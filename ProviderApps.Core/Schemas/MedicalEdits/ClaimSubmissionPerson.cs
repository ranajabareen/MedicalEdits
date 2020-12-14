namespace ProviderApps.Core.Schemas.MedicalEdits
{
    public class ClaimSubmissionPerson
    {
        public string dateOfBirth { get; set; }
        public string gender { get; set; }
        public decimal? height { get; set; }
        public string memberId { get; set; }
        public string nationalId { get; set; }
        public string patientId { get; set; }
        public decimal? weight { get; set; }
    }
}
