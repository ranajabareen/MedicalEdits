namespace ProviderApps.Core.Schemas.MedicalEdits
{
    public class ClaimSubmissionObservation
    {
        public string observationCode { get; set; }
        public string observationType { get; set; }
        public string observationValue { get; set; }
        public string observationValueType { get; set; }
    }
}
