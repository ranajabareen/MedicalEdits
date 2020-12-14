namespace ProviderApps.Core.Models
{
    public class Insurance : BaseModelIntActiveFromToDataList
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public double PatientShare { get; set; }
        public string Description { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string InsuranceLogoGuid { get; set; }
    }
}
