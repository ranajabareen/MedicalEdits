using System.Collections.Generic;
using ProviderApps.Core.Models;
using ProviderApps.Core.ViewModels.MedicalEdits;
using ProviderApps.Core.Schemas.MedicalEdits;
using System;
using ProviderApps.Core.Classes;

namespace ProviderApps.XUnitTestProject.MedicalEditsServiceTest.TestData
{
    public static class MedicalEditsDataProvider
    {
        public static MedicalEditsViewModel GetMedicalEditsServiceRequest()
        {
            var medicalEditsRequestParameter = new MedicalEditsViewModel
            {
                ActivityList = new List<MedicalEditsActivityViewModel>
                {
                    new MedicalEditsActivityViewModel
                    {
                        Id= 1,
                        Quantity= "6",
                        Duration="3",
                       ObservationList = new List<MedicalEditsObservationViewModel>(),
                        ActivityCode= "2040-106618-1001",
                        ActivityType= "5",
                        Unit="GRANULAR",
                    }
                },

                DiagnosisList = new List<MedicalEditsDiagnosisViewModel>
                {
                    new MedicalEditsDiagnosisViewModel
                    {
                        Code = "R25.0",
                        TypeId = 1,
                        Type = "Principal"
                    }
                },
                InsurancePlanId = 875,
                ClaimId = "DHA-F-0045687-INS111-202002101627117-313"
            };

            return medicalEditsRequestParameter;
        }

        public static MedicalEditsRequestModel GetMedicalEditsClientRequest()
        {
            var medicalEditsRequestModel = new MedicalEditsRequestModel
            {
                 username= "",
                 password = "",
                 services = "1",
                 isStrictCheck = "0",
                 claims = new List<Claim>
                 {
                     new Claim
                     {
                         claimID = "DHA-F-0045687-TPA333-201910152304421-143",
                         claimSubmissionActivityList = new List<ClaimSubmissionActivity>
                         {
                             new ClaimSubmissionActivity
                             {
                                 activityCode = "0043-197703-0511",
                                 activityId = "1",
                                activitySource = "CURRENT",
                                activityStart = "15/10/2019",
                                activityType = "5",
                                duration = "10",
                                quantity = "5"
                             }
                         },
                         claimSubmissionDiagnosisList = new List<ClaimSubmissionDiagnosis>
                         {
                             new ClaimSubmissionDiagnosis
                             {
                                 diagnosisCode = "R25.0",
                                 diagnosisSource = "CURRENT",
                                 diagnosisType = "Principal"
                             }
                         },
                         claimSubmissionPerson = new ClaimSubmissionPerson
                         {
                             dateOfBirth = "22/01/2018",
                             gender = "M",
                             height = 80,
                             nationalId = "2291987021",
                             patientId = "39",
                             weight = 20
                         },
                         payerId = "INS004",
                         providerId = "DHA-F-0045687"

                     }
                 }
            };

            return medicalEditsRequestModel;
        }

        public static Patient GetPatient()
        {
            var patient = new Patient
            {
              
                DateOfBirth =  new DateTimeOffset(2018,1,22,0,0,0,TimeSpan.Zero),
                NationalId = "2291987021",
                Code = "39",
            };
            return patient;
        }

        public static List<ActivityType> GetActivityTypes()
        {
            var activityTypes = new List<ActivityType>();
            activityTypes.AddRange(new List<ActivityType>
            {
                new ActivityType { Code = "5",  Name = "Drug"},
                new ActivityType { Code = "10",  Name = "Scientific Drug"},

            });

            return activityTypes;
        }

        public static MedicalEditsSetting GetMedicalEditsServiceSetting()
        {
            var medicalEditsServiceSetting = new MedicalEditsSetting
            {
                UserName = "",
                Password = "",
                Services = new List<string> { "1" },
                IsStrictCheck = "0",
                Url = "",
                IsEnabled = true,
                CancelTime = 4000
            };
            return medicalEditsServiceSetting;
        }
    }
}
