using System.Collections.Generic;

namespace ProviderApps.Core.Schemas.MedicalEdits
{
    public class ClaimEditResponse
    {
        /// <summary>
        ///  Whether the service call is successful or not 
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Validation comments (Errors and Warnings) on the request 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// unique ID for the response (reference ID). 
        /// Use this ID to get the service response anytime using “getClaimsEditsResult” method
        /// </summary>
        public string ResponseId { get; set; }

        /// <summary>
        /// List of claim edits for all submitted claims. 
        /// </summary>
        public List<ClaimEditResult> ClaimEdits { get; set; }
    }

    public class ClaimEditResult
    {
        public int EditId { get; set; }

        public string ActivityId { get; set; }

        public string ActivityCode { get; set; }

        public string OppositeCode { get; set; }

        public int EditRank { get; set; }

        public string EditCode { get; set; }

        public string EditComment { get; set; }

        public string CreatedDate { get; set; }

        public EditType EditType { get; set; }

        public string EditSource { get; set; }

        public string ClaimId { get; set; }

        public string Value1 { get; set; }
        public string Value1Type { get; set; }

        public string Value2 { get; set; }
        public string Value2Type { get; set; }
        public string Value2SubType { get; set; }

        public string GetActivityCodeValue()
        {
            if (Value1Type == "ACTIVITY")
                return Value1;
            if (Value2Type == "ACTIVITY" && Value2SubType == "ACTIVITY_CODE")
                return Value2;
            return null;
        }
    }

    public class EditType
    {
        public int ServiceId { get; set; }

        public string ServiceCode { get; set; }

        public string Description { get; set; }
    }
}
