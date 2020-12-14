namespace ProviderApps.Core.Models
{
    public class PatientInsurance : BaseModelIntActiveFromTo, IBaseModelIsDeleted
    {
        public string MemberCardId { get; set; }
        public int PatientId { get; set; }
        public int InsurancePlanId { get; set; }
        public bool IsDeleted { get; set; }

        public InsurancePlan InsurancePlan { get; set; }
        public Patient Patient { get; set; }
    }
}
