using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using BgmCollaboration.Installation.ActionLibrary;

namespace OnBase.TaxReturnUploader.CustomAction
{
    public class UpdateSettingsAction
    {
        #region VARIABLES
        private static Configuration _config;
        #endregion

        #region PROPERTIES
        private static string AppConfigPath => Path.Combine(_config.BinTarget, _config.ConfigName);
        #endregion

        public static void UpdateGlobalSettingsPath(RootActionBase sender, Configuration config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config), "Unable to locate application settings file location without the configuration object provided.");

            _config = config;
            XDocument appConfig = XDocument.Load(AppConfigPath);
            XmlNamespaceManager namespaceManager = ConstructEnterpriseLibraryNamespaceManager();

            // NOTE: Add/Remove records to match instances of Global Settings (i.e. DataConfiguration)
            UpdateGlobalSettingPathAttribute(appConfig, namespaceManager, "GlobalSettings", config.GlobalSettings2File);

            UpdateGlobalSettingKeyPathAttribute(appConfig, namespaceManager, "File Key Algorithm Storage Provider", config.SettingsKeyFile);

            SaveAppConfig(AppConfigPath, appConfig);
        }

        public static void UpdateSettings(RootActionBase sender, Configuration config)
        {
            if (sender == null)
                throw new ArgumentNullException(nameof(sender), "Unable to update application settings without a Sender object provided.");
            if (config == null)
                throw new ArgumentNullException(nameof(config), "Unable to locate application settings file location without the configuration object provided.");

            _config = config;
            XDocument appConfig = XDocument.Load(AppConfigPath);

            IDictionary<string, string> settings = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> setting in _config.AppSettings)
                settings.Add(setting.Key, sender.GetContextAction(setting.Value).NewValue);

            foreach (KeyValuePair<string, string> setting in settings)
                AddOrUpdateSetting(appConfig, setting);

            SaveAppConfig(AppConfigPath, appConfig);
        }

        #region PRIVATE
        private static void AddOrUpdateSetting(XDocument appConfig, KeyValuePair<string, string> setting)
        {
            if (appConfig == null)
                throw new ArgumentNullException(nameof(appConfig), "Cannot add or update a setting for a NULL App Config.");
            if (string.IsNullOrWhiteSpace(setting.Key))
                throw new ArgumentNullException(nameof(setting), "The setting key was not provided.");

            string rootPath = "configuration/appSettings";

            XElement node = appConfig.XPathSelectElement($"{rootPath}/add[@key='{setting.Key}']");

            if (node == null)
            {
                node = appConfig.XPathSelectElement(rootPath);
                if (node == null)
                    throw new NullReferenceException($"Failed to find the appSettings location of the config file \n \"{appConfig}\"");

                XElement newChildElement = new XElement("add");
                newChildElement.Add(new XAttribute("key", setting.Key));
                newChildElement.Add(new XAttribute("value", setting.Value));
                node.Add(newChildElement);
            }
            else
                node.SetAttributeValue("value", setting.Value);
        }

        private static void AddOrUpdateSetting(XDocument appConfig, string key, string value)
        {
            KeyValuePair<string, string> setting = new KeyValuePair<string, string>(key, value);
            AddOrUpdateSetting(appConfig, setting);
        }

        private static XmlNamespaceManager ConstructEnterpriseLibraryNamespaceManager()
        {
            NameTable nameTable = new NameTable();

            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(nameTable);
            string enterpriseLibraryNamespace = "http://www.microsoft.com/practices/enterpriselibrary/08-31-2004/configuration";
            namespaceManager.AddNamespace("el", enterpriseLibraryNamespace);

            return namespaceManager;
        }

        private static void SaveAppConfig(string appConfigPath, XDocument appConfig)
        {
            FileSecurity accessControl = new FileSecurity(appConfigPath, AccessControlSections.Owner);
            File.SetAccessControl(appConfigPath, accessControl);
            appConfig.Save(appConfigPath);
        }

        private static void UpdateGlobalSettingKeyPathAttribute(XDocument appConfig, XmlNamespaceManager namespaceManager, string settingKeyName, string newPath)
        {
            if (appConfig == null)
                throw new ArgumentNullException(nameof(appConfig), "Cannot update the global setting key path for a NULL App Config.");
            if (namespaceManager == null)
                throw new ArgumentNullException(nameof(namespaceManager), "Cannot update the global setting key path without the namespace manager.");
            if (string.IsNullOrWhiteSpace(settingKeyName))
                throw new ArgumentNullException(nameof(settingKeyName), "The setting key name was not provided.");
            if (string.IsNullOrWhiteSpace(newPath))
                throw new ArgumentNullException(nameof(newPath), "The new path was not provided.");

            XElement node = appConfig.XPathSelectElement($"configuration/el:enterpriselibrary.configurationSettings/el:keyAlgorithmStorageProvider[@name='{settingKeyName}']", namespaceManager);
            if (node == null)
                throw new NullReferenceException($"Failed to locate the setting key name in the app config. \n Setting Key Name: \"{settingKeyName}\" \n \"{appConfig}\"");

            node.SetAttributeValue("path", newPath);
        }

        private static void UpdateGlobalSettingPathAttribute(XDocument appConfig, XmlNamespaceManager namespaceManager, string configSection, string newPath)
        {
            if (appConfig == null)
                throw new ArgumentNullException(nameof(appConfig), "Cannot update the global setting path for a NULL App Config.");
            if (namespaceManager == null)
                throw new ArgumentNullException(nameof(namespaceManager), "Cannot update the global setting path without the namespace manager.");
            if (string.IsNullOrWhiteSpace(configSection))
                throw new ArgumentNullException(nameof(configSection), "The config section was not provided.");
            if (string.IsNullOrWhiteSpace(newPath))
                throw new ArgumentNullException(nameof(newPath), "The new path was not provided.");

            XElement node = appConfig.XPathSelectElement($"configuration/el:enterpriselibrary.configurationSettings/el:configurationSections/el:configurationSection[@name='{configSection}']/el:storageProvider", namespaceManager);
            if (node == null)
                throw new NullReferenceException($"Failed to locate the configuration section in the app config. \n Config Section: \"{configSection}\" \n \"{appConfig}\"");

            node.SetAttributeValue("path", newPath);
        }
        #endregion
    }
}