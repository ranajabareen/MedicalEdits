using ProviderApps.Core.ViewModels.MedicalEdits;
using System.Threading;
using System.Threading.Tasks;

namespace ProviderApps.Core.Interfaces
{
    public interface IMedicalEditsService
    {
        /// <summary>
        ///  Used to Get Medical Edits 
        /// </summary>
        /// <param name="medicalEditsRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<MedicalEditsResult> GetClaimsEditsAsync(MedicalEditsViewModel medicalEditsRequest, CancellationToken cancellationToken = default(CancellationToken));
    }
}
