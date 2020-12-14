using System.Collections.Generic;

namespace ProviderApps.Core.Schemas.MedicalEdits
{
    public class Claim
    {
        public string claimID { get; set; }
        public List<ClaimSubmissionActivity> claimSubmissionActivityList { get; set; }
        public List<ClaimSubmissionDiagnosis> claimSubmissionDiagnosisList { get; set; }
        public ClaimSubmissionEncounter claimSubmissionEncounter { get; set; }
        public ClaimSubmissionPerson claimSubmissionPerson { get; set; }
        public List<ClaimSubmissionAllergy> claimSubmissionAllergyList { get; set; }
        public string payerId { get; set; }
        public string providerId { get; set; }
    }
}
