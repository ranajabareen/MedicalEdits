using System.Collections.Generic;

namespace ProviderApps.Core.Schemas.MedicalEdits
{
    public class MedicalEditsRequestModel
    {
        public string username { get; set; } // username from medicalEditsSetting
        public string password { get; set; } // password from medicalEditsSetting
        public string services { get; set; } // services from medicalEditsSetting
        public string isStrictCheck { get; set; } //isStrictCheck from medicalEditsSetting
        public List<Claim> claims { get; set; } // holds the claim data to check
    }
}
