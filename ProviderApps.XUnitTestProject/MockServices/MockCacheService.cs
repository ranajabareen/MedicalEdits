using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using ProviderApps.Core.Interfaces;
using ProviderApps.Core.Models;
using ProviderApps.XUnitTestProject.MedicalEditsServiceTest.TestData;

namespace ProviderApps.XUnitTestProject.MockServices
{
    public class MockCacheService : Mock<ICacheService>
    {
        public MockCacheService MockActivityTypesCacheList()
        {

            var activitiesTypes = MedicalEditsDataProvider.GetActivityTypes();
          
            Setup(x => x.CachedLists.ActivityTypes)
                .Returns(activitiesTypes);

            return this;
        }
    }
}
