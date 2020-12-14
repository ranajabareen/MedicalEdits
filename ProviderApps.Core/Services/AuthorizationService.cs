using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProviderApps.Core.Classes;
using ProviderApps.Core.Extensions;
using ProviderApps.Core.Interfaces;
using ProviderApps.Core.Interfaces.Services;
using ProviderApps.Core.Schemas.MedicalEdits;
using ProviderApps.Core.ViewModels.MedicalEdits;

namespace ProviderApps.Core.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICacheService _cacheService;
        private readonly IMedicalEditsService _medicalEditsService;

        public AuthorizationService(IHttpContextAccessor httpContextAccessor, ICacheService cacheService, IMedicalEditsService medicalEditsService)
        {
            _medicalEditsService = medicalEditsService;
            _httpContextAccessor = httpContextAccessor;
            _cacheService = cacheService;
        }

        public async Task<ProcessResult<MedicalEditsResult>> GetMedicalEditsAsync(MedicalEditsViewModel requestParameter)
        {
            var processResult = new ProcessResult<MedicalEditsResult>()
            {
                Succeeded = false,
                StatusCode = HttpStatusCode.BadRequest
            };

            var prepareDataResult = await PrepareCdsParameterAsync(requestParameter).ConfigureAwait(false);
            if (!prepareDataResult.Succeeded)
            {
                processResult.AddRangeModelError(prepareDataResult.GetModelErrors());
                processResult.StatusCode = prepareDataResult.StatusCode;
                processResult.Succeeded = prepareDataResult.Succeeded;
                return await Task.FromResult(processResult).ConfigureAwait(false);
            }

            var result = await _medicalEditsService.GetClaimsEditsAsync(requestParameter).ConfigureAwait(false);

            if (result.Success)
            {
                processResult.DataResult = result;
                processResult.Succeeded = true;
                processResult.StatusCode = HttpStatusCode.OK;

                return await Task.FromResult(processResult).ConfigureAwait(false);
            }

            return await Task.FromResult(processResult).ConfigureAwait(false);
        }

        private List<EditCommentParameter> ExtractEditCommentParameters(ClaimEditResult claimEditResult)
        {
            var list = new List<EditCommentParameter>();
            var value1 = claimEditResult.Value1;
            var value2 = claimEditResult.Value2;
            var value1Type = claimEditResult.Value1Type;
            var value2Type = claimEditResult.Value2Type;

            //Extract from value1
            if (!string.IsNullOrEmpty(value1))
            {
                //Multiple values

                var value1Splitted = value1.Split(',');
                foreach (var code in value1Splitted)
                {
                    list.Add(new EditCommentParameter
                    {
                        Value = code,
                        Type = value1Type
                    });
                }

            }

            //Extract from value2
            if (!string.IsNullOrEmpty(value2))
            {
                // Extract from value1
                var value2Splitted = value2.Split(',');
                list.AddRange(value2Splitted.Select(code => new EditCommentParameter { Value = code, Type = value2Type }));
            }
            return list;
        }

        private async Task<ProcessResult> PrepareCdsParameterAsync(MedicalEditsViewModel requestParameter)
        {
            var processResult = new ProcessResult()
            {
                Succeeded = false,
                StatusCode = HttpStatusCode.BadRequest
            };


            if (requestParameter == null)
            {
                processResult.AddModelError("", "CdsRequest can't be null!");
                return await Task.FromResult(processResult).ConfigureAwait(false);

            }
            if (requestParameter.ActivityList == null || requestParameter.ActivityList.Count == 0)
            {
                processResult.AddModelError("", "One activity at least is required");
                return await Task.FromResult(processResult).ConfigureAwait(false);

            }

            if (requestParameter.DiagnosisList == null || requestParameter.DiagnosisList.Count == 0)
            {
                processResult.AddModelError("", "One diagnosis at least is required");
                return await Task.FromResult(processResult).ConfigureAwait(false);
            }

            if (requestParameter.InsurancePlanId.HasValue)
            {
                var insurancePlan = _cacheService.CachedLists.Plans.FirstOrDefault(c => c.Id == requestParameter.InsurancePlanId);
                if (insurancePlan != null)
                {
                    requestParameter.PayerId = _cacheService.CachedLists.Payers.FirstOrDefault(c => c.Id == insurancePlan.PayerId).Code ?? null;
                }

            }

            requestParameter.ProviderId = _httpContextAccessor.HttpContext.User.GetCurrentFacilityCode();

            processResult.Succeeded = true;
            processResult.StatusCode = HttpStatusCode.OK;

            return await Task.FromResult(processResult).ConfigureAwait(false);
        }
    }
}
