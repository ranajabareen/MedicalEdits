using System.Collections.Generic;
using ProviderApps.Core.Models;

namespace ProviderApps.Core.Interfaces
{
    public interface ICachedList
    {
        List<Insurance> Payers { get; set; }
        List<InsurancePlan> Plans { get; set; }
        List<ActivityType> ActivityTypes { get; set; }
    }
}
