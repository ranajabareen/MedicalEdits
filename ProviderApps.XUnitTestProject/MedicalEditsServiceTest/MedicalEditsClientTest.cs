using ProviderApps.Core.Schemas.MedicalEdits;
using ProviderApps.XUnitTestProject.MedicalEditsServiceTest.TestData;
using System.Threading.Tasks;
using Xunit;

namespace ProviderApps.XUnitTestProject.MedicalEditsServiceTest
{
    public class MedicalEditsClientTest : MedicalEditsBaseTest
    {
        private MedicalEditsRequestModel _medicalEditsClientRequest;
        public MedicalEditsClientTest()
        {
            _medicalEditsClientRequest = MedicalEditsDataProvider.GetMedicalEditsClientRequest();
        }

        /// <summary>
        /// 1.Test GetClaimsEdits API in success case
        /// 2.By using valid Data and Check the success field returned from API response
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetClaimEditRequestSuccess()
        {
            // 1. Call GetClaimsEdit API
            var result = await _medicalEditsClient.GetClaimsEditsAsync(_medicalEditsClientRequest);
            // 2. Check that success field is true 
            Assert.True(result.Success);
            Assert.True(result.ClaimEdits != null);
        }

        /// <summary>
        /// 1. Test GetClaimsEdits API in not authorized case
        /// 2. By using Invalid UserName
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetClaimEditRequestNotAuthorized()
        {
            //1. Set Invalid UserName 
            _medicalEditsClientRequest.username = "InvalidUserName";

            // 2. Call GetClaimsEdit API
            var result = await _medicalEditsClient.GetClaimsEditsAsync(_medicalEditsClientRequest);

            // 3. Check that success field is false
            Assert.False(result.Success);
            Assert.Contains("Authentication failed", result.Message);
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
            _medicalEditsClientRequest.claims
                .ForEach(a => a.claimSubmissionDiagnosisList.ForEach(
                    d => d.diagnosisType = ""));

            // 2. Call GetClaimsEdit API
            var result = await _medicalEditsClient.GetClaimsEditsAsync(_medicalEditsClientRequest);

            // 3. Check that success field is false
            Assert.False(result.Success);
            Assert.Contains("Validation failed", result.Message);

        }
    }
}
