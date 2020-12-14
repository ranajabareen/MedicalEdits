using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ProviderApps.WebFramework.Models
{
    /// <summary>
    /// Represents the return wrapped response (envelope) to front end.
    /// </summary>
    public class ResponseWrapper
    {
        /// <summary>
        /// The return data (view model) of the response.
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// The messages to be shown to user (ex: validation, or status messages). 
        /// </summary>
        public ICollection<UIMessage> Messages { get; set; }

        /// <summary>
        /// Pagination details in case of pagination result.
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public string Pagination { get; set; }

        /// <summary>
        /// Pagination raw JSON string, used to not duplicate json serialize.
        /// </summary>
        [JsonProperty("Pagination", NullValueHandling = NullValueHandling.Ignore)]
        private JRaw PaginationJson
        {
            get => !string.IsNullOrWhiteSpace(Pagination) ? new JRaw(Pagination) : null;
            set => Pagination = value.ToString();
        }

        /// <summary>
        /// Errors for development environment only.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<UIMessage> Errors { get; set; }

        public ResponseWrapper()
        {
            Messages = new List<UIMessage>();
        }
    }
}
