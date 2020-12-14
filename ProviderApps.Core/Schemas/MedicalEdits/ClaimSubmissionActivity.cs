using System.Collections.Generic;

namespace ProviderApps.Core.Schemas.MedicalEdits
{
    public class ClaimSubmissionActivity
    {
        public string activityCode { get; set; }
        public string activityId { get; set; }
        public string activitySource { get; set; }
        public string activityStart { get; set; }
        public string activityType { get; set; }
        public string duration { get; set; }
        public string quantity { get; set; }
        public List<ClaimSubmissionObservation> claimSubmissionObservationList { get; set; }
        public string clinician { get; set; }
        public string orderingClinician { get; set; }
        public string priorAuthorizationId { get; set; }
        public string unit { get; set; }
    }
}
