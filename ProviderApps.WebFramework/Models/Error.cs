namespace ProviderApps.WebFramework.Models
{
    public class Error
    {
        /// <summary> Gets or sets the additional reference. the Id or the code for the object</summary>
        /// <value> The additional reference.</value>
        public object AdditionalReference { get; set; }

        /// <summary> Gets or sets the error text.</summary>
        /// <value> The error text.</value>
        public string ErrorText { get; set; }

        /// <summary> Gets or sets the Suggestion Message.</summary>
        /// <value> The Suggestion Message.</value>
        public string SuggestionMessage { get; set; }

        /// <summary>
        ///     Gets or sets the object name.
        /// </summary>
        public string ObjectName { get; set; }

        /// <summary>
        ///     Gets or sets the object type.
        /// </summary>
        public string ObjectType { get; set; }

        /// <summary> Gets or sets the rule text.</summary>
        /// <value> The rule text.</value>
        public string RuleText { get; set; }

        /// <summary>
        ///     Gets or sets the transaction.
        /// </summary>
        public string Transaction { get; set; }

        /// <summary> Gets or sets the type.</summary>
        /// <value> The type.</value>
        public string Type { get; set; }
    }
}
