using System.Collections.Generic;

namespace ProviderApps.Core.ViewModels.MedicalEdits
{
    public class MedicalEditsViewModel
    {
        public List<MedicalEditsActivityViewModel> ActivityList { get; set; }

        public List<MedicalEditsDiagnosisViewModel> DiagnosisList { get; set; }

        public List<MedicalEditsAllergiesViewModel> AllergiesList { get; set; }

        public List<MedicalEditsActivityViewModel> ActiveDrugs { get; set; }

        public List<MedicalEditsDiagnosisViewModel> ChronicDiseases { get; set; }

        public string PayerId { get; set; }

        public string ClaimId { get; set; } = "claim1";

        public string ProviderId { get; set; }

        public int? InsurancePlanId { get; set; }

        public int PatientId { get; set; }

        public decimal? Weight { get; set; }

        public decimal? height { get; set; }
    }
}
