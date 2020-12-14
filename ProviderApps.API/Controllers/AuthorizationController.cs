using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProviderApps.Core.Interfaces.Services;
using ProviderApps.Core.ViewModels.MedicalEdits;
using ProviderApps.WebFramework;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace ProviderApps.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthorizationController : BaseController
    {
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPost("CheckMedicalEdits")]
        public async Task<IActionResult> CheckMedicalEdits([FromBody] MedicalEditsViewModel requestParameter)
        {
            if (ModelState.IsValid)
            {
                var result = await _authorizationService.GetMedicalEditsAsync(requestParameter);
                return await GetResponse(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Validate CDS API
        /// </summary>
        /// <param name="requestParameter"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     {
        /// 
        ///         "activityList":[   
        ///             {
        ///                "ActivityCode":"R1.0",
        ///                "ActivityType":"10",
        ///                "id":1,
        ///                "quantity":12,
        ///                "duration":"3",
        ///                "OrderingClinician":"",
        ///                "Clinician":"",
        ///                "observationList":[ ],
        ///                "Unit":"DOSE"
        ///             }   
        ///         ],
        ///         
        ///         "diagnosisList":[  
        ///             {
        ///                "code":"R93.0",
        ///                "TypeId":1
        ///             }
        ///         ],
        ///         "allergiesList":[
        ///             {
        ///                "Code": "96"
        ///             }
        ///         
        ///         ], 
        ///         "ChronicDiseases":[
        ///             {
        ///               "code":"R94.0"           
        ///             }
        ///         ], 
        ///         "ActiveDrugs":[
        ///            
        ///            {
        ///              "ActivityCode":"R1.0",
        ///              "ActivityType":"10",
        ///              "quantity":12,
        ///              "duration":"3",
        ///              "OrderingClinician":"",
        ///              "Clinician":"",
        ///              "Unit":"DOSE"
        ///            }
        ///         ], 
        ///         "InsurancePlanId":849,
        ///         "PatientId":63,
        ///         "weight": 56,
        ///         "height": 160,
        ///         "ClaimId": "DHA-F-0045687-INS111-20190826233657-504"
        ///         
        ///         
        ///     }
        ///         
        /// </remarks>
        ///
    }
}
