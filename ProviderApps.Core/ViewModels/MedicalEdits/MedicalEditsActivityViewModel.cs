using System;
using System.Collections.Generic;

namespace ProviderApps.Core.ViewModels.MedicalEdits
{
    public class MedicalEditsActivityViewModel
    {
        public int Id { get; set; }
        public string ActivityCode { get; set; }

        public int ActivityId { get; set; }

        public string ActivitySource { get; set; } = "CURRENT";

        public DateTimeOffset StartDate { get; set; } 

        public string ActivityType { get; set; }

        public string Quantity { get; set; }

        public string Duration { get; set; }

        public string Unit { get; set; }

        public string Clinician { get; set; }
        public int? ClinicianId { get; set; }

        public int? OrderingClinicianId { get; set; }

        public string OrderingClinician { get; set; }

        public List<MedicalEditsObservationViewModel> ObservationList { get; set; }
    }
}
