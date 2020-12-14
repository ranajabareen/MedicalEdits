using ProviderApps.Core.Schemas.MedicalEdits;
using System.Threading;
using System.Threading.Tasks;

namespace ProviderApps.MedicalEditsAPI
{
    public interface IMedicalEditsClient
    {
        /// <summary>
        /// Medical Service API to Get Medical Edits 
        /// </summary>
        /// <param name="medicalEditsRequest"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<ClaimEditResponse> GetClaimsEditsAsync(MedicalEditsRequestModel medicalEditsRequest, CancellationToken token = default(CancellationToken));
    }
}
