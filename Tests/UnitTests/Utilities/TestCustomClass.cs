namespace ValorMorgan.Common.UnitTests.Utilities
{
    class TestCustomClass
    {
        #region VARIABLES
        public const string DEFAULT_VALUE = "Default";
        #endregion

        #region PROPERTIES
        public int TestIntProperty { get; set; }

        public string TestStringProperty { get; set; }

        public string TestStringPropertyWithDefault { get; set; } = DEFAULT_VALUE;
        #endregion
    }
}