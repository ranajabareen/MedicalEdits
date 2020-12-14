using System.Threading.Tasks;
using ProviderApps.Core.Classes;
using ProviderApps.Core.ViewModels.MedicalEdits;

namespace ProviderApps.Core.Interfaces.Services
{
    public interface IAuthorizationService
    {
        Task<ProcessResult<MedicalEditsResult>> GetMedicalEditsAsync(MedicalEditsViewModel requestParameter);
    }
}