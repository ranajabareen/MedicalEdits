using System.Collections.Generic;

namespace ProviderApps.Core.ViewModels.MedicalEdits
{
    public class MedicalEditsResponse
    {
        public string EditCode { get; set; }

        public string EditComment { get; set; }

        public string EditType { get; set; }

        public string Description { get; set; }

        public string ActivityId { get; set; }

        public string ActivityCode { get; set; }

        public string ActivityType { get; set; }

        public string EditSource { get; set; }

        public bool IsBlockingEdit { get; set; }

        public List<EditCommentParameter> EditCommentParameters { get; set; }
    }
}
