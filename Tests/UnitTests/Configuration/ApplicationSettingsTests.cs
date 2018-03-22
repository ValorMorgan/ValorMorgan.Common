using ValorMorgan.Common.Configuration;
using ValorMorgan.Common.UnitTests.Utilities;
using NUnit.Framework;

namespace ValorMorgan.Common.UnitTests.Configuration
{
    [TestFixture]
    class ApplicationSettingsTests
    {
        #region POSITIVE
        [Test]
        [Category("Positive"), Category("CI")]
        public void AllKeys_Not_Null_And_Not_Empty()
        {
            Assert.That(ApplicationSettings.AllKeys, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        [Category("Positive"), Category("CI")]
        public void AppSettings_Not_Null()
        {
            Assert.That(ApplicationSettings.AppSettings, Is.Not.Null);
        }

        [Test]
        [Category("Positive"), Category("CI")]
        public void HasKey_Vaidates_Key_Existence()
        {
            Assert.IsTrue(ApplicationSettings.HasKey(SettingNames.TEST_SETTING_NAME));
            Assert.IsFalse(ApplicationSettings.HasKey(SettingNames.SETTING_THAT_NOT_EXISTS));
        }

        [Test]
        [Category("Positive"), Category("CI")]
        public void HasValue_Vaidates_Value_Existence()
        {
            Assert.IsTrue(ApplicationSettings.HasValue(SettingNames.TEST_SETTING_NAME));
            Assert.IsFalse(ApplicationSettings.HasValue(SettingNames.SETTING_THAT_NOT_EXISTS));
        }

        [Test]
        [Category("Positive"), Category("CI")]
        public void As_Type_Properly_Converts()
        {
            Assert.That(ApplicationSettings.AsString(SettingNames.TEST_SETTING_NAME), Is.TypeOf<string>(), "Failed AsString");
            Assert.That(ApplicationSettings.As<string>(SettingNames.TEST_SETTING_NAME), Is.TypeOf<string>(), "Failed As<string>");
            Assert.That(ApplicationSettings.AsChar(SettingNames.TEST_CHARACTER_NAME), Is.TypeOf<char>(), "Failed AsChar");
            Assert.That(ApplicationSettings.AsBool(SettingNames.TEST_BOOLEAN_NAME), Is.TypeOf<bool>(), "Failed AsBool");
            Assert.That(ApplicationSettings.AsInt(SettingNames.TEST_INTEGER_NAME), Is.TypeOf<int>(), "Failed AsInt");
            Assert.That(ApplicationSettings.AsByte(SettingNames.TEST_INTEGER_NAME), Is.TypeOf<byte>(), "Failed AsByte");
            Assert.That(ApplicationSettings.AsSByte(SettingNames.TEST_INTEGER_NAME), Is.TypeOf<sbyte>(), "Failed AsSByte");
            Assert.That(ApplicationSettings.AsDecimal(SettingNames.TEST_INTEGER_NAME), Is.TypeOf<decimal>(), "Failed AsDecimal");
            Assert.That(ApplicationSettings.AsDouble(SettingNames.TEST_INTEGER_NAME), Is.TypeOf<double>(), "Failed AsDouble");
            Assert.That(ApplicationSettings.AsFloat(SettingNames.TEST_INTEGER_NAME), Is.TypeOf<float>(), "Failed AsFLoat");
            Assert.That(ApplicationSettings.AsUInt(SettingNames.TEST_INTEGER_NAME), Is.TypeOf<uint>(), "Failed AsUInt");
            Assert.That(ApplicationSettings.AsLong(SettingNames.TEST_INTEGER_NAME), Is.TypeOf<long>(), "Failed AsLong");
            Assert.That(ApplicationSettings.AsULong(SettingNames.TEST_INTEGER_NAME), Is.TypeOf<ulong>(), "Failed AsULong");
            Assert.That(ApplicationSettings.AsShort(SettingNames.TEST_INTEGER_NAME), Is.TypeOf<short>(), "Failed AsShort");
            Assert.That(ApplicationSettings.AsUShort(SettingNames.TEST_INTEGER_NAME), Is.TypeOf<ushort>(), "Failed AsUShort");
        }
        #endregion

        #region NEGATIVE
        [Test]
        [Category("Negative"), Category("CI")]
        public void HasKey_Throws_ArgumentNullException_If_Key_Is_Null_Or_WhiteSpace()
        {
            Assert.That(() => ApplicationSettings.HasKey(null), Throws.ArgumentNullException);
            Assert.That(() => ApplicationSettings.HasKey(" "), Throws.ArgumentNullException);
        }

        [Test]
        [Category("Negative"), Category("CI")]
        public void HasValue_Throws_ArgumentNullException_If_Key_Is_Null_Or_WhiteSpace()
        {
            Assert.That(() => ApplicationSettings.HasValue(null), Throws.ArgumentNullException);
            Assert.That(() => ApplicationSettings.HasValue(" "), Throws.ArgumentNullException);
        }

        [Test]
        [Category("Negative"), Category("CI")]
        public void ThrowExceptionIfKeyNotFound_Throws_Exception_If_Key_Not_Found()
        {
            Assert.That(() => ApplicationSettings.ThrowExceptionIfKeyNotFound(SettingNames.TEST_SETTING_NAME), Throws.Nothing);
            Assert.That(() => ApplicationSettings.ThrowExceptionIfKeyNotFound(SettingNames.SETTING_THAT_NOT_EXISTS), Throws.Exception);
        }
        #endregion
    }
}
