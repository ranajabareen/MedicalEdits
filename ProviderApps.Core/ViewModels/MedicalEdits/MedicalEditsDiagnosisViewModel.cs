namespace ProviderApps.Core.ViewModels.MedicalEdits
{
    public class MedicalEditsDiagnosisViewModel
    {
        public string Code { get; set; }

        public string Source { get; set; } = "CURRENT";

        public string Type { get; set; }

        public int TypeId { get; set; }
    }
}
