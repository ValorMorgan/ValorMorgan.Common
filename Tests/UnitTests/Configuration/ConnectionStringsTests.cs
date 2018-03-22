using ValorMorgan.Common.Configuration;
using ValorMorgan.Common.UnitTests.Utilities;
using NUnit.Framework;

namespace ValorMorgan.Common.UnitTests.Configuration
{
    [TestFixture]
    class ConnectionStringsTests
    {
        #region POSITIVE
        [Test]
        [Category("Positive"), Category("CI")]
        public void ConnectStrings_Not_Null()
        {
            Assert.That(ConnectionStrings.ConnectStrings, Is.Not.Null);
        }

        [Test]
        [Category("Positive"), Category("CI")]
        public void HasConnectionString_Validates_String_Exists()
        {
            Assert.IsTrue(ConnectionStrings.HasConnectionString(ConnectionStringNames.TEST_CONNECTION_STRING_NAME));
            Assert.IsFalse(ConnectionStrings.HasConnectionString(ConnectionStringNames.CONNECTION_STRING_THAT_NOT_EXISTS));
        }

        [Test]
        [Category("Positive"), Category("CI")]
        public void GetConnectionString_Not_Null_And_Not_Empty()
        {
            Assert.That(ConnectionStrings.GetConnectionString(ConnectionStringNames.TEST_CONNECTION_STRING_NAME), Is.Not.Null.And.Not.Empty);
        }
        #endregion

        #region NEGATIVE
        [Test]
        [Category("Negative"), Category("CI")]
        public void GetConnectionString_Throws_ArgumentNullException_When_Parameter_Is_Null_Or_WhiteSpace()
        {
            Assert.That(() => ConnectionStrings.GetConnectionString(null), Throws.ArgumentNullException);
            Assert.That(() => ConnectionStrings.GetConnectionString(" "), Throws.ArgumentNullException);
        }
        
        [Test]
        [Category("Negative"), Category("CI")]
        public void ThrowExceptionIfConnectionStringNotFound_Throws_Exception_If_String_Not_Found()
        {
            Assert.That(() =>
                ConnectionStrings.ThrowExceptionIfConnectionStringNotFound(ConnectionStringNames.TEST_CONNECTION_STRING_NAME),
                Throws.Nothing);

            Assert.That(() =>
                ConnectionStrings.ThrowExceptionIfConnectionStringNotFound(ConnectionStringNames.CONNECTION_STRING_THAT_NOT_EXISTS),
                Throws.Exception);
        }
        #endregion
    }
}
