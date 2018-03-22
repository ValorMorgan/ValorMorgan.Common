using System;
using ValorMorgan.Common.Error.Helpers;
using NUnit.Framework;

namespace ValorMorgan.Common.UnitTests.Error
{
    [TestFixture]
    class ExceptionFormatterTests
    {
        // Message should end in "." for testing purposes
        private const string TEST_MESSAGE = "Test.";
        private static string TEST_DOUBLE_MESSAGE => $"{TEST_MESSAGE} {TEST_MESSAGE}";
        private static Exception TEST_EXCEPTION => new Exception(TEST_MESSAGE);
        private static Exception TEST_DOUBLE_EXCEPTION => new Exception(TEST_MESSAGE, TEST_EXCEPTION);

        [Test]
        [Category("Positive"), Category("CI")]
        public void Divider_Not_Null_And_Not_Empty()
        {
            Assert.That(ExceptionFormatter.Divider, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        [Category("Positive"), Category("CI")]
        public void GetExceptionMessagesOnly_Returns_Just_The_Message()
        {
            Assert.That(ExceptionFormatter.GetExceptionMessagesOnly(TEST_EXCEPTION), Is.EqualTo(TEST_MESSAGE));
            Assert.That(ExceptionFormatter.GetExceptionMessagesOnly(TEST_DOUBLE_EXCEPTION), Is.EqualTo(TEST_DOUBLE_MESSAGE));
        }

        [Test]
        [Category("Positive"), Category("CI")]
        public void GetExceptionMessagesOnly_Null_Parameter_Returns_Default_Message()
        {
            Assert.That(ExceptionFormatter.GetExceptionMessagesOnly(null), Is.EqualTo(LoggerConstants.DEFAULT_LOG_MESSAGE));
        }
        
        [Test]
        [Category("Positive"), Category("CI")]
        public void GetFormatedException_Not_Null_And_Not_Empty()
        {
            Assert.That(ExceptionFormatter.GetFormatedException(TEST_EXCEPTION), Is.Not.Null.And.Not.Empty);
            Assert.That(ExceptionFormatter.GetFormatedException(TEST_DOUBLE_EXCEPTION), Is.Not.Null.And.Not.Empty);
        }

        [Test]
        [Category("Positive"), Category("CI")]
        public void GetFormatedException_Null_Parameter_Returns_Default_Message()
        {
            Assert.That(ExceptionFormatter.GetFormatedException(null), Is.EqualTo(LoggerConstants.DEFAULT_LOG_MESSAGE));
        }
    }
}
