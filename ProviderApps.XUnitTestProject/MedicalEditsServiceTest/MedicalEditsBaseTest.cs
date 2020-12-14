using System;
using System.Net.Http;
using ProviderApps.Core.Interfaces;
using ProviderApps.MedicalEditsAPI;
using ProviderApps.XUnitTestProject.MockServices;

namespace ProviderApps.XUnitTestProject.MedicalEditsServiceTest
{
    public class MedicalEditsBaseTest
    {
        protected IMedicalEditsClient _medicalEditsClient;
        protected IMedicalEditsService _medicalEditsService;

        public MedicalEditsBaseTest()
        {
            var mockRepository = new MockRepository().MockMedicalEditsFindPatient();
            var mockSetting = new MockSettingService().MockGetMedicalEditsServiceSetting();
            var mockCachSevice = new MockCacheService().MockActivityTypesCacheList();
            var medicalEditsSetting = mockSetting.Object.GetMedicalEditsSetting();
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(medicalEditsSetting.Url)
            };
            _medicalEditsClient = new MedicalEditsClient(httpClient);
            _medicalEditsService = new MedicalEditsService(_medicalEditsClient, mockRepository.Object, mockSetting.Object, mockCachSevice.Object);

        }
    }
}
