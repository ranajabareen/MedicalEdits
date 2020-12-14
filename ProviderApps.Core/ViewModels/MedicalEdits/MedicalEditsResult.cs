using System.Collections.Generic;

namespace ProviderApps.Core.ViewModels.MedicalEdits
{
    public class MedicalEditsResult
    {
        public MedicalEditsResult()
        {
            CurrentEdits = new List<MedicalEditsResponse>();
            HistoryEdits = new List<MedicalEditsResponse>();
        }
        /// <summary>
        /// Unique ID for the MedicalEdits response
        /// </summary>
        public string ResponseId { get; set; }

        /// <summary>
        /// Holds list of edits related to original claim data ( dataSource of edit "CURRENT")
        /// </summary>
        public List<MedicalEditsResponse> CurrentEdits { get; set; }

        /// <summary>
        /// Holds list of edits related to person's history (either activity or diagnosis) ( dataSource of edit "HISTORY")
        /// </summary>
        public List<MedicalEditsResponse> HistoryEdits { get; set; }

        public bool Success { get; set; }

        /// <summary>
        /// Holds the Error Message returned from MedicalEdits Service
        /// </summary>
        public string Message { get; set; }
    }
}
