namespace ProviderApps.WebFramework.Models
{
    /// <summary>
    /// Represents the UI Message to be shown to user.
    /// </summary>
    public class UIMessage
    {
        /// <summary>
        /// The field name where the message is related to (ex: validation message on a specific field).
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The message body.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The message type.
        /// </summary>
        public UIMessageTypes Type { get; set; }

        /// <summary>
        /// The Suggestion Message
        /// </summary>
        public string SuggestionMessage { get; set; }
    }
}
