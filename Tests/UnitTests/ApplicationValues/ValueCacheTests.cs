using System;
using System.Configuration;
using ValorMorgan.Common.ApplicationValues;
using ValorMorgan.Common.Configuration;
using ValorMorgan.Common.UnitTests.Utilities;
using NUnit.Framework;

namespace ValorMorgan.Common.UnitTests.ApplicationValues
{
    [TestFixture]
    public class ValueCacheTests
    {
        #region GetValue
        
        #region POSITIVE
        [Test]
        [Category("Positive"), Category("CI")]
        public void GetValue_Returns_Not_Null_And_Not_Empty()
        {
            IValueCache cache = new ValueCache();
            cache.RegisterValue(Utilities.SettingNames.TEST_SETTING_NAME);
            Assert.That(cache.GetValue(Utilities.SettingNames.TEST_SETTING_NAME), Is.Not.Null.And.Not.Empty);
        }

        [Test]
        [Category("Positive"), Category("CI")]
        public void GetValue_From_Template_Type_Returns_Not_Null_Or_Empty()
        {
            IValueCache cache = new ValueCache();
            cache.RegisterValue(Utilities.SettingNames.TEST_SETTING_NAME);
            Assert.That(cache.GetValue<string>(Utilities.SettingNames.TEST_SETTING_NAME), Is.Not.Null.And.Not.Empty);
        }
        
        [Test]
        [Category("Positive"), Category("CI")]
        public void GetValue_Returns_Correct_Type()
        {
            TypeParameterTest(Utilities.SettingNames.TEST_SETTING_NAME, typeof(string));
            TypeParameterTest(Utilities.SettingNames.TEST_CHARACTER_NAME, typeof(char));
            TypeParameterTest(Utilities.SettingNames.TEST_BOOLEAN_NAME, typeof(bool));
            TypeParameterTest(Utilities.SettingNames.TEST_INTEGER_NAME, typeof(byte));
            TypeParameterTest(Utilities.SettingNames.TEST_INTEGER_NAME, typeof(sbyte));
            TypeParameterTest(Utilities.SettingNames.TEST_INTEGER_NAME, typeof(decimal));
            TypeParameterTest(Utilities.SettingNames.TEST_INTEGER_NAME, typeof(double));
            TypeParameterTest(Utilities.SettingNames.TEST_INTEGER_NAME, typeof(float));
            TypeParameterTest(Utilities.SettingNames.TEST_INTEGER_NAME, typeof(int));
            TypeParameterTest(Utilities.SettingNames.TEST_INTEGER_NAME, typeof(uint));
            TypeParameterTest(Utilities.SettingNames.TEST_INTEGER_NAME, typeof(long));
            TypeParameterTest(Utilities.SettingNames.TEST_INTEGER_NAME, typeof(ulong));
            TypeParameterTest(Utilities.SettingNames.TEST_INTEGER_NAME, typeof(short));
            TypeParameterTest(Utilities.SettingNames.TEST_INTEGER_NAME, typeof(ushort));

            GenericTypeTest<string>(Utilities.SettingNames.TEST_SETTING_NAME);
            GenericTypeTest<char>(Utilities.SettingNames.TEST_CHARACTER_NAME);
            GenericTypeTest<bool>(Utilities.SettingNames.TEST_BOOLEAN_NAME);
            GenericTypeTest<byte>(Utilities.SettingNames.TEST_INTEGER_NAME);
            GenericTypeTest<sbyte>(Utilities.SettingNames.TEST_INTEGER_NAME);
            GenericTypeTest<decimal>(Utilities.SettingNames.TEST_INTEGER_NAME);
            GenericTypeTest<double>(Utilities.SettingNames.TEST_INTEGER_NAME);
            GenericTypeTest<float>(Utilities.SettingNames.TEST_INTEGER_NAME);
            GenericTypeTest<int>(Utilities.SettingNames.TEST_INTEGER_NAME);
            GenericTypeTest<uint>(Utilities.SettingNames.TEST_INTEGER_NAME);
            GenericTypeTest<long>(Utilities.SettingNames.TEST_INTEGER_NAME);
            GenericTypeTest<ulong>(Utilities.SettingNames.TEST_INTEGER_NAME);
            GenericTypeTest<short>(Utilities.SettingNames.TEST_INTEGER_NAME);
            GenericTypeTest<ushort>(Utilities.SettingNames.TEST_INTEGER_NAME);

            void TypeParameterTest(string setting, Type type)
            {
                try
                {
                    IValueCache cache = new ValueCache();
                    cache.RegisterValue(setting, type);
                    Assert.That(cache.GetValue(setting), Is.TypeOf(type), $"Failed for {type.Name}");
                }
                catch (Exception ex)
                {
                    throw new ApplicationException($"Failed for {type.Name}", ex);
                }
            }

            void GenericTypeTest<T>(string setting)
            {
                try
                {
                    IValueCache cache = new ValueCache();
                    cache.RegisterValue(setting, typeof(T));
                    Assert.That(cache.GetValue<T>(setting), Is.TypeOf<T>(), $"Failed for {typeof(T).Name}");
                }
                catch (Exception ex)
                {
                    throw new ApplicationException($"Failed for {typeof(T).Name}", ex);
                }
            }
        }

        [Test]
        [Category("Positive"), Category("CI")]
        public void GetValue_Supports_Custom_Type_Casting()
        {
            IValueCache cache = new ValueCache();
            cache.RegisterValue(Utilities.SettingNames.TEST_SETTING_NAME, typeof(TestCustomClass), true,
                setting => new TestCustomClass
                {
                    TestStringProperty = setting
                }
            );
            Assert.That(cache.GetValue<TestCustomClass>(Utilities.SettingNames.TEST_SETTING_NAME)?.TestStringProperty, Is.Not.Null.And.EqualTo(ApplicationSettings.AsString(Utilities.SettingNames.TEST_SETTING_NAME)));
        }
        #endregion

        #region NEGATIVE
        [Test]
        [Category("Negative"), Category("CI")]
        public void GetValue_Throws_When_Setting_Not_Registered()
        {
            IValueCache cache = new ValueCache();
            Assert.That(() => cache.GetValue(Utilities.SettingNames.TEST_SETTING_NAME), Throws.Exception);
        }

        [Test]
        [Category("Negative"), Category("CI")]
        public void GetValue_From_Template_Type_Throws_When_Not_Registered()
        {
            IValueCache cache = new ValueCache();
            Assert.That(() => cache.GetValue<string>(Utilities.SettingNames.TEST_SETTING_NAME), Throws.Exception);
        }

        [Test]
        [Category("Negative"), Category("CI")]
        public void GetValue_Throws_ConfigurationErrorsException_When_Setting_Not_Found_And_Required()
        {
            IValueCache cache = new ValueCache();
            cache.RegisterValue(Utilities.SettingNames.SETTING_THAT_NOT_EXISTS, typeof(string));
            Assert.That(() => cache.GetValue(Utilities.SettingNames.SETTING_THAT_NOT_EXISTS), Throws.TypeOf<ConfigurationErrorsException>());
        }
        
        [Test]
        [Category("Negative"), Category("CI")]
        public void GetValue_Throws_Exception_When_Custom_Type_Cast_Not_Provided()
        {
            IValueCache cache = new ValueCache();
            cache.RegisterValue(Utilities.SettingNames.TEST_SETTING_NAME, typeof(TestCustomClass), true, null);
            Assert.That(() => cache.GetValue(Utilities.SettingNames.TEST_SETTING_NAME), Throws.Exception);
        }
        #endregion

        #endregion

        #region RegisterValue

        #region POSITIVE
        [Test]
        [Category("Positive"), Category("CI")]
        public void RegisterValue_With_One_Param()
        {
            IValueCache cache = new ValueCache();
            Assert.That(() => cache.RegisterValue(Utilities.SettingNames.TEST_SETTING_NAME), Throws.Nothing, "Failed on one param.");
        }

        [Test]
        [Category("Positive"), Category("CI")]
        public void RegisterValue_With_Two_Params()
        {
            IValueCache cache = new ValueCache();
            Assert.That(() => cache.RegisterValue(Utilities.SettingNames.TEST_SETTING_NAME, typeof(string)), Throws.Nothing, "Failed on two params.");
        }
        
        [Test]
        [Category("Positive"), Category("CI")]
        public void RegisterValue_With_Four_Params()
        {
            IValueCache cache = new ValueCache();
            Assert.That(() => cache.RegisterValue(Utilities.SettingNames.TEST_SETTING_NAME, typeof(string), true, string.Empty), Throws.Nothing, "Failed on four params.");
        }

        [Test]
        [Category("Positive"), Category("CI")]
        public void RegisterValue_With_Five_Params()
        {
            IValueCache cache = new ValueCache();
            Assert.That(() => cache.RegisterValue(Utilities.SettingNames.TEST_SETTING_NAME, typeof(string), true, string.Empty, null), Throws.Nothing, "Failed on four params.");
        }

        [Test]
        [Category("Positive"), Category("CI")]
        public void RegisterValue_Cannot_Register_Duplicate()
        {
            IValueCache cache = new ValueCache();

            // First one shouldn't fail. Second one should.
            Assert.That(() => cache.RegisterValue(Utilities.SettingNames.TEST_SETTING_NAME), Throws.Nothing);
            Assert.That(() => cache.RegisterValue(Utilities.SettingNames.TEST_SETTING_NAME), Throws.Exception);
        }

        [Test]
        [Category("Positive"), Category("CI")]
        public void RegisterValue_Supports_CustomType()
        {
            Assert.That(() => new ValueCache().RegisterValue(Utilities.SettingNames.TEST_SETTING_NAME, typeof(TestCustomClass)), Throws.Nothing);
        }

        [Test]
        [Category("Positive"), Category("CI")]
        public void RegisterValue_DefaultValue_Enforced_To_Correct_Type()
        {
            // string, required
            Assert.That(() =>
                    new ValueCache().RegisterValue(Utilities.SettingNames.TEST_SETTING_NAME, typeof(string)),
                Throws.Nothing,
                "Failed for string, required"
            );

            // string, not required, null
            Assert.That(() =>
                    new ValueCache().RegisterValue(Utilities.SettingNames.TEST_SETTING_NAME, typeof(string), false, null),
                Throws.Nothing,
                "Failed for string, not required, default null"
            );

            // string, not required, value
            Assert.That(() =>
                    new ValueCache().RegisterValue(Utilities.SettingNames.TEST_SETTING_NAME, typeof(string), false, "hi"),
                Throws.Nothing,
                "Failed for string, not required, default value"
            );

            // int, required
            Assert.That(() =>
                    new ValueCache().RegisterValue(Utilities.SettingNames.TEST_SETTING_NAME, typeof(int)),
                Throws.Nothing,
                "Failed for int, requried"
            );

            // int, not required, null
            Assert.That(() =>
                    new ValueCache().RegisterValue(Utilities.SettingNames.TEST_SETTING_NAME, typeof(int), false, null),
                Throws.Exception,
                "Failed for int, not required, default null"
            );

            // int, not required, value
            Assert.That(() =>
                    new ValueCache().RegisterValue(Utilities.SettingNames.TEST_SETTING_NAME, typeof(int), false, 10),
                Throws.Nothing,
                "Failed for int, not required, default value"
            );

            // Custom Type, required
            Assert.That(() =>
                    new ValueCache().RegisterValue(Utilities.SettingNames.TEST_SETTING_NAME, typeof(TestCustomClass)),
                Throws.Nothing,
                "Failed for custom type, required, default null"
            );

            // Custom Type, not required, empty string
            Assert.That(() =>
                    new ValueCache().RegisterValue(Utilities.SettingNames.TEST_SETTING_NAME, typeof(TestCustomClass), false, string.Empty),
                Throws.Exception,
                "Failed for custom type, required, default string empty."
            );

            // Custom Type, not required, new Custom Type
            Assert.That(() =>
                    new ValueCache().RegisterValue(Utilities.SettingNames.TEST_SETTING_NAME, typeof(TestCustomClass), false, new TestCustomClass()),
                Throws.Nothing,
                "Failed for custom type, required, default string empty."
            );

            // Custom Type, not required, new Custom Type, delegate provided
            Assert.That(() =>
                    new ValueCache().RegisterValue(Utilities.SettingNames.TEST_SETTING_NAME, typeof(TestCustomClass), false, new TestCustomClass(), val => new TestCustomClass { TestStringProperty = val }),
                Throws.Nothing,
                "Failed for custom type, required, default string empty."
            );
        }
        #endregion

        #region NEGATIVE
        [Test]
        [Category("Negative"), Category("CI")]
        public void RegisterValue_Throws_ArgumentNullException_When_SettingName_Null()
        {
            Assert.That(() => new ValueCache().RegisterValue(null), Throws.ArgumentNullException);
        }

        [Test]
        [Category("Negative"), Category("CI")]
        public void RegisterValue_Throws_ArgumentNullException_When_ValueType_Null()
        {
            Assert.That(() => new ValueCache().RegisterValue(Utilities.SettingNames.TEST_SETTING_NAME, null), Throws.ArgumentNullException);
        }
        
        [Test]
        [Category("Negative"), Category("CI")]
        public void RegisterValue_Throws_Exception_When_DefaultType_Not_Matches_Setting_Type()
        {
            Assert.That(() => new ValueCache().RegisterValue(Utilities.SettingNames.TEST_SETTING_NAME, typeof(string), false, 10), Throws.Exception);
        }

        [Test]
        [Category("Negative"), Category("CI")]
        public void RegisterValue_Throws_InvalidOperationException_When_Registering_Setting_Already_Registered()
        {
            IValueCache cache = new ValueCache();
            cache.RegisterValue(Utilities.SettingNames.TEST_SETTING_NAME);
            Assert.That(() => cache.RegisterValue(Utilities.SettingNames.TEST_SETTING_NAME), Throws.InvalidOperationException);
        }
        #endregion

        #endregion
    }
}