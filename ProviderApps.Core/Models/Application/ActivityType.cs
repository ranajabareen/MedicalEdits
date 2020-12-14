namespace ProviderApps.Core.Models
{
    public class ActivityType : BaseModelIntActiveFromToDataList
    {
        public string Name { get; set; }
        public string Code { get; set; }

        /// <summary>
        /// Optional : if the lookup has enum so we map using this key.
        /// </summary>
        public string SchemaEnumValue { get; set; }
    }
}
