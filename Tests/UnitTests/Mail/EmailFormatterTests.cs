using System.Collections.Generic;
using ValorMorgan.Common.Mail.Helpers;
using NUnit.Framework;

namespace ValorMorgan.Common.UnitTests.Mail
{
    [TestFixture]
    class EmailFormatterTests
    {
        private readonly IEnumerable<string> _recipients = new[]
            {
                "jmorgan@devqa.farm",
                "dscore@devqa.farm",
                "jfolkens@devqa.farm"
            };
        
        [Test]
        [Category("Positive"), Category("CI")]
        public void ConvertRecipientCollectionToString_Not_Null_And_Not_Empty()
        {
            Assert.That(EmailFormatter.ConvertRecipientCollectionToString(_recipients), Is.Not.Null.And.Not.Empty);
        }

        [Test]
        [Category("Positive"), Category("CI")]
        public void ConvertRecipientCollectionToString_Has_Proper_Divider()
        {
            string recipientsAsString = EmailFormatter.ConvertRecipientCollectionToString(_recipients);
            char testDivider = ';';
            Assert.IsTrue(recipientsAsString.Contains(EmailerConstants.EMAIL_FORMAT_DIVIDER_CHARACTER.ToString()));
            Assert.IsTrue(EmailFormatter.ConvertRecipientCollectionToString(_recipients, testDivider).Contains(testDivider.ToString()));
        }
    }
}
