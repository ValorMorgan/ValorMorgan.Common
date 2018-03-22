using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ValorMorgan.Common.Mail.Helpers
{
    /// <summary>
    /// Formats provided data for emails.
    /// </summary>
    public static class EmailFormatter
    {
        /// <summary>
        /// Converts the provided collection of recipients to a string seperated by the
        /// constant format divider character.
        /// </summary>
        /// <param name="recipients">The collection of recipients to convert to a string.</param>
        /// <returns>A string seperated by the constant format divider character.</returns>
        public static string ConvertRecipientCollectionToString(IEnumerable<string> recipients) =>
            ConvertRecipientCollectionToString(recipients, EmailerConstants.EMAIL_FORMAT_DIVIDER_CHARACTER);

        /// <summary>
        /// Converts the provided collection of recipients to a string seperated by the
        /// provided dividing character.
        /// </summary>
        /// <param name="recipients">The collection of recipients to convert to a string.</param>
        /// <param name="dividingCharacter">The character to use to divide the recipients.</param>
        /// <returns>A string seperated by the provided dividing character.</returns>
        public static string ConvertRecipientCollectionToString(IEnumerable<string> recipients, char dividingCharacter)
        {
            IEnumerable<string> recipientsEnumerated = recipients as IList<string> ?? recipients?.ToList() ?? new List<string>();
            
            if (!recipientsEnumerated.Any())
            {
                Debug.WriteLine("No recipients provided to generate a recipient string from. Returning NULL.");
                return null;
            }

            string recipientString = "";
            foreach (string recipient in recipientsEnumerated.ToList())
            {
                // If empty or just composed of the divider character, skip
                if (string.IsNullOrWhiteSpace(recipient) || recipient.Trim() == dividingCharacter.ToString())
                    continue;

                if (recipient.Trim().EndsWith(dividingCharacter.ToString()))
                    recipientString += $" {recipient.Trim()}";
                else
                    recipientString += $" {recipient.Trim()}{dividingCharacter}";
                recipientString = recipientString.Trim();
            }

            if (!string.IsNullOrEmpty(recipientString))
                return recipientString;

            Debug.WriteLine("The provided list of recipients did not have any valid information to use. Returning NULL.");
            return null;
        }
    }
}