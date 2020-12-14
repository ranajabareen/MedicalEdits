using System.Threading;
using System.Threading.Tasks;
using ProviderApps.Core.ViewModels.MedicalEdits;
using ProviderApps.XUnitTestProject.MedicalEditsServiceTest.TestData;
using Xunit;

namespace ProviderApps.XUnitTestProject.MedicalEditsServiceTest
{
    public class MedicalEditsServiceTest : MedicalEditsBaseTest
    {
        private MedicalEditsViewModel medicalEditsServiceRequest;
        public MedicalEditsServiceTest()
        {
            medicalEditsServiceRequest = MedicalEditsDataProvider.GetMedicalEditsServiceRequest();
        }

        /// <summary>
        /// 1.Test GetClaimsEdits API in success case
        /// 2.By using valid Data and Check the success field returned from API response
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetClaimEditRequestSuccess()
        {
            //1. Set Cancellation Token
            var cts = new CancellationTokenSource();
            cts.CancelAfter(10000);
            // 2. Call GetClaimsEdit API
            var result = await _medicalEditsService.GetClaimsEditsAsync(medicalEditsServiceRequest,cts.Token);
            // 3. Check that success field is true 
            Assert.True(result.Success);
            Assert.True(result.CurrentEdits != null);
        }

        /// <summary>
        /// 1. Test GetClaimsEdits API in invalid data case
        /// 2. By using Invalid data
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetClaimEditRequestBadRequest()
        {
            //1. Set empty value to diagnosisType in diagnosisList
            medicalEditsServiceRequest.DiagnosisList
                .ForEach(d => d.Type = "");

            // 2. Call GetClaimsEdit API
            var result = await _medicalEditsService.GetClaimsEditsAsync(medicalEditsServiceRequest);

            // 3. Check that success field is false
            Assert.False(result.Success);
            Assert.Contains("Validation failed", result.Message);

        }

        /// <summary>
        /// 1.Test GetClaimsEdits API in cancellation Case
        /// 2.By set value to Cancellation token field 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetClaimEditRequestCancellationSuccess()
        {
            //1. Set Cancellation Token
            var cts = new CancellationTokenSource();
            cts.CancelAfter(100);
            // 2. Call GetClaimsEdit API
            var result = await _medicalEditsService.GetClaimsEditsAsync(medicalEditsServiceRequest, cts.Token);
            // 3. Check that success field is true 
            Assert.False(result.Success);
            Assert.Contains("canceled", result.Message);
        }
    }
}
