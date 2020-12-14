using System.Security.Claims;
using System.Security.Principal;
using ProviderApps.Core.Constants;

namespace ProviderApps.Core.Extensions
{
    public static class IdentityExtensions
    {
        /// <summary>
        /// Get default facility 
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string GetCurrentFacilityCode(this IPrincipal principal)
        {

            if (principal == null)
                return null;
            var claim = (principal as ClaimsPrincipal)?.FindFirstValue(ApplicationClaimTypes.CurrentFacilityCode);
            if (!string.IsNullOrWhiteSpace(claim))
                return claim;
            return null;
        }
    }
}
