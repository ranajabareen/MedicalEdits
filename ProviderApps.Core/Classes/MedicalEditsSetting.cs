using System.Collections.Generic;

namespace ProviderApps.Core.Classes
{
    public class MedicalEditsSetting
    {
        public bool IsEnabled { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
        public int? CancelTime { get; set; } = 4000;
        public string IsStrictCheck { get; set; }
        public List<string> Services { get; set; }
    }
}
