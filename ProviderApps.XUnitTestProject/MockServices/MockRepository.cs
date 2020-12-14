using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using ProviderApps.Core.Interfaces;
using ProviderApps.Core.Models;
using ProviderApps.XUnitTestProject.MedicalEditsServiceTest.TestData;

namespace ProviderApps.XUnitTestProject.MockServices
{
    public class MockRepository :Mock<IRepository>
    {
        public MockRepository MockMedicalEditsFindPatient()
        {
            var data = MedicalEditsDataProvider.GetPatient();
            Setup(x => x.Find<Patient>(null,null))
                .Returns(data);

            return this;
        }
    }
}
