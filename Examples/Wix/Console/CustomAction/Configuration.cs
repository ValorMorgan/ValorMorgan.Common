using System;
using System.Collections.Generic;
using System.Xml;

namespace OnBase.TaxReturnUploader.CustomAction
{
    public class Configuration
    {
        public Configuration()
        {
        }

        public Configuration(string customActionTarget, string binTarget, string configDir, string target)
        {
            CustomActionTarget = customActionTarget;
            BinTarget = binTarget;
            ConfigDir = configDir;
            Target = target;

            if (string.IsNullOrWhiteSpace(CustomActionTarget))
                throw new ArgumentNullException(nameof(CustomActionTarget), "CUSTOMACTIONTARGET is missing from the CustomActionData collection.");
            if (string.IsNullOrWhiteSpace(BinTarget))
                throw new ArgumentNullException(nameof(BinTarget), "BINTARGET is missing from the CustomActionData collection.");
            if (string.IsNullOrWhiteSpace(ConfigDir))
                throw new ArgumentNullException(nameof(ConfigDir), "CONFIGDIR is missing from the CustomActionData collection.");
            if (string.IsNullOrWhiteSpace(Target))
                throw new ArgumentNullException(nameof(Target), "Target is missing from the CustomActionData collection.");

            RetrieveFromGlobalSettings();
        }

        public Configuration(Microsoft.Deployment.WindowsInstaller.CustomActionData cad)
            : this(cad["CUSTOMACTIONTARGET"], cad["BINTARGET"], cad["CONFIGDIR"], cad["Target"])
        {
            ShortVersion = cad["ProductVersion"];
        }

        private void RetrieveFromGlobalSettings()
        {
            string globalSettings2FilePath = System.IO.Path.Combine(ConfigDir, "globalsettings2.config");

            XmlDocument globalXmlDocument = new XmlDocument();
            globalXmlDocument.Load(globalSettings2FilePath);

            AssociationNumber = globalXmlDocument.SelectSingleNode("GlobalSettings/xmlSerializerSection/GlobalSettings/AssociationNumber")?.InnerText;
        }

        public string AssociationNumber { get; set; }
        public string ShortVersion { get; set; }
        public string Version => $"20{ShortVersion}";
        public string ProductName => "OnBase.TaxReturnUploader";
        public string ExecutableName => $"{ProductName}.exe";
        public string ConfigName => $"{ExecutableName}.config";

        public string GlobalSettings2File => System.IO.Path.Combine(ConfigDir, "GlobalSettings2.config");
        public string SettingsKeyFile => System.IO.Path.Combine(ConfigDir, "settings.key");

        public string Target { get; set; }
        public string ConfigDir { get; set; }
        public string CustomActionTarget { get; set; }
        public string BinTarget { get; set; }
        public string XmlInstructionsName => $"{Version}.ConfigUpdate.xml";

        public IDictionary<string, string> AppSettings = new Dictionary<string, string>
        {
            {"FilenameDivider", "Filename Divider"},
            {"CifPosition", "Cif Position"},
            {"NamePosition", "Name Position"},
            {"TaxYearPosition", "Tax Year Position"},
            {"SuppliedBy", "Supplied By"},
            {"RootDirectory", "Root Directory"},
            {"ErrorDirectory", "Error Directory"},
            {"EmailFrom", "Email From"},
            {"ErrorEmailTo", "Error Email To"},
            {"FatalEmailTo", "Fatal Email To"},
            {"SendSuccessEmail", "Send Success Email"},
            {"EmailSuccessSubject", "Email Success Subject"},
            {"EmailErrorSubject", "Email Error Subject"},
            {"OnBaseBranchKeyword", "OnBase Branch Keyword"},
            {"OnBaseCifKeyword", "OnBase Cif Keyword"},
            {"OnBaseFullNameKeyword", "OnBase Full Name Keyword"},
            {"OnBaseSuppliedByKeyword", "OnBase Supplied By Keyword"},
            {"OnBaseTaxYearKeyword", "OnBase Tax Year Keyword"},
            {"OnBaseDocumentType", "OnBase Document Type"},
            {"OnBaseFileType", "OnBase File Type"},
            {"OnBaseDocumentLink", "OnBase Document Link"},
            {"OnBaseServerUrl", "OnBase Server Url"},
            {"OnBaseDataSource", "OnBase Data Source"},
            {"OnBaseConnectionTimeout", "OnBase Connection Timeout"},
            {"VerboseLogging", "Verbose Logging"},
            {"EventLogSource", "Event Log Source"}
        };

        public string EventLogLog => "OnBase Log";
    }
}