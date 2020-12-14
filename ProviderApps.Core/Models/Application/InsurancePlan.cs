namespace ProviderApps.Core.Models
{
    public class InsurancePlan : BaseModelIntActiveFromTo
    {
        public InsurancePlan()
        {
          
        }

        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public double? DiscountRate { get; set; }
        public double? MaxPatientShare { get; set; }
        public double? PatientShare { get; set; }
        public string CardPhotoGuid { get; set; }
        public int? SyncedListRecordId { get; set; }
        public double? DeductibleAmount { get; set; }
        public int PayerId { get; set; }
        public int ReceiverId { get; set; }
        public int? OrganizationId { get; set; }
        public int? DataListId { get; set; }
        public int? SyncedListId { get; set; }
        public Insurance Payer { get; set; }
        public Insurance Receiver { get; set; }
    }
}
