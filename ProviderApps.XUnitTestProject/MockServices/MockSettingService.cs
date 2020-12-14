using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using ProviderApps.Core.Classes;
using ProviderApps.Core.Interfaces;
using ProviderApps.Core.Services;
using ProviderApps.XUnitTestProject.MedicalEditsServiceTest.TestData;

namespace ProviderApps.XUnitTestProject.MockServices
{
    public class MockSettingService : Mock<ISettingsService>
    {

        public MockSettingService MockGetMedicalEditsServiceSetting()
        {
            var medicalEditsServiceSetting = MedicalEditsDataProvider.GetMedicalEditsServiceSetting();
            Setup(x => x.GetMedicalEditsSetting()).Returns(medicalEditsServiceSetting);

            return this;
        }


    }
}
