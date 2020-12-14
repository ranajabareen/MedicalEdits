using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ProviderApps.Core.Classes;
using ProviderApps.Core.Interfaces;
using ProviderApps.Core.Models;
using ProviderApps.Core.Schemas.MedicalEdits;
using ProviderApps.Core.Schemas.MedicalEdits.CustomExceptions;
using ProviderApps.Core.ViewModels.MedicalEdits;

namespace ProviderApps.MedicalEditsAPI
{
    public class MedicalEditsService : IMedicalEditsService
    {
        private readonly IMedicalEditsClient _medicalEditsClient;
        private readonly ISettingsService _settingsService;
        private readonly IRepository _repository;
        private readonly ICacheService _cacheService;
        private MedicalEditsSetting medicalEditsSetting;

        public MedicalEditsService(IMedicalEditsClient medicalEditsClient, IRepository repository, ISettingsService settingsService, ICacheService cacheService)
        {
            _medicalEditsClient = medicalEditsClient;
            _settingsService = settingsService;
            _cacheService = cacheService;
            _repository = repository;
            medicalEditsSetting = _settingsService.GetMedicalEditsSetting();
        }

        /// <summary>
        /// Used to get Medical Edits 
        /// </summary>
        /// <param name="medicalEditsRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<MedicalEditsResult> GetClaimsEditsAsync(MedicalEditsViewModel medicalEditsRequest,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = new MedicalEditsResult();
            try
            {
                //check if medicalEditsService is enabled 
                if (!medicalEditsSetting.IsEnabled)
                {
                    result.Success = false;
                    result.Message = "Medical Edits service not enabled!";
                    return result;

                }

                //set cancellation time for medicalEdits Service
                // if cancellationToken parameter equals default CancellationToken, then set the cancel time from medialEditsSettings
                if (cancellationToken == default(CancellationToken))
                {
                    var cts = new CancellationTokenSource();
                    cts.CancelAfter(medicalEditsSetting.CancelTime.GetValueOrDefault());
                    cancellationToken = cts.Token;

                }

                //mapping medicalEditsViewModel to medicalEditsRequest
                var medicalEditsRequestData = ToMedicalEditsRequestModel(medicalEditsRequest);
                if (medicalEditsRequestData == null)
                {
                    result.Success = false;
                    result.Message = "An error occurred while mapping data to MedicalEdits Request";
                    return result;
                }

                // call GetClaimEdits api and check the returned result
                var medicalEditsResult = await _medicalEditsClient.GetClaimsEditsAsync(medicalEditsRequestData, cancellationToken);
                if (medicalEditsResult != null)
                {
                    result.ResponseId = medicalEditsResult.ResponseId;
                    result.Message = medicalEditsResult.Message;
                    result.Success = medicalEditsResult.Success;

                    // mapping claimEdits to  MedicalEditsResponse object
                    if (medicalEditsResult.Success && medicalEditsResult.ClaimEdits != null)
                    {
                        var medicalEditsClaimActivityList =
                            medicalEditsRequestData.claims.FirstOrDefault().claimSubmissionActivityList;

                        var ClaimEdits = medicalEditsResult.ClaimEdits.Select(x => new MedicalEditsResponse
                        {
                            EditCode = x.EditCode,
                            EditComment = x.EditComment,
                            EditType = x.EditType.Description,
                            ActivityId = x.ActivityId,
                            ActivityCode = x.GetActivityCodeValue(), // get ActivityCode value
                            ActivityType = _cacheService.CachedLists?.ActivityTypes?.Find(a =>
                                a.Code == medicalEditsClaimActivityList
                                    .FirstOrDefault(act => act.activityCode == x.GetActivityCodeValue())
                                    ?.activityType)?.Name,
                            EditSource = x.EditSource,
                            // Set IsBlockingEdit = true ,when Drug or Generic quantity has exceeded the maximum limit that can be given for this age
                            IsBlockingEdit = x.EditCode == "DRU_DOS_01",
                            EditCommentParameters = ExtractEditCommentParameters(x)
                        }).ToList();

                        // edits returned to current cliam data
                        result.CurrentEdits = ClaimEdits.Where(c => c.EditSource == "CURRENT").ToList();
                       
                        //edits returned claim history data
                        result.HistoryEdits = ClaimEdits.Where(c => c.EditSource == "HISTORY").ToList();
                    }
                }
            }
            catch (MedicalEditsAPIException e)
            {
                result.Message = e.MedicalEditsExceptionMessage;
                result.Success = false;
            }
            catch (OperationCanceledException e)
            {
                result.Message = e.Message;
                result.Success = false;
            }
            catch (Exception e)
            {
                //log the exception
                result.Message = "MedicalEdits request failed.";
                result.Success = false;
            }

            return result;
        }

        /// <summary>
        /// Generate list of edit result from (value1,value2,valuetype1,valuetype2).
        /// </summary>
        /// <param name="claimEditResult"></param>
        /// <returns></returns>
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
                foreach (var code in value2Splitted)
                {
                    list.Add(new EditCommentParameter
                    {
                        Value = code,
                        Type = value2Type
                    });
                }

            }

            return list;
        }

        private MedicalEditsRequestModel ToMedicalEditsRequestModel(MedicalEditsViewModel request)
        {
            try
            {
                //set medical Service setting data
                var medicalEditsRequest = new MedicalEditsRequestModel
                {
                    password = medicalEditsSetting.Password,
                    username = medicalEditsSetting.UserName,
                    services = string.Join(',', medicalEditsSetting.Services),
                    isStrictCheck = medicalEditsSetting.IsStrictCheck

                };

                // prepare claim data
                var claim = new Claim
                {
                    claimID = request.ClaimId,
                    providerId = request.ProviderId,
                    payerId = request.PayerId
                };

                // set claim activities
                claim.claimSubmissionActivityList = request.ActivityList?.Select(activity => new ClaimSubmissionActivity
                {
                    activityCode = activity.ActivityCode,
                    activityId = activity.Id.ToString(),
                    activitySource = activity.ActivitySource,
                    activityStart = activity.StartDate.ToLocalTime().ToString("dd/MM/yyyy"),
                    activityType = activity.ActivityType,
                    clinician = activity.Clinician,
                    orderingClinician = activity.OrderingClinician,
                    duration = activity.Duration,
                    quantity = activity.Quantity,
                    unit = activity.Unit,
                    claimSubmissionObservationList = activity.ObservationList != null
                            ? activity.ObservationList.Select(obs => new ClaimSubmissionObservation
                            {
                                observationCode = obs.Code,
                                observationValue = obs.Value,
                                observationValueType = obs.ValueType,
                                observationType = obs.Type,
                            }).ToList()
                            : null

                }).ToList();

                // add ActiveDrugs as history for the patient
                if (request.ActiveDrugs?.Count > 0)
                {
                    var i = 1;
                    claim.claimSubmissionActivityList.AddRange(request.ActiveDrugs.Select(activity => new ClaimSubmissionActivity
                    {
                        activityCode = activity.ActivityCode,
                        activityId = $"ActiveDrug-{i++}",
                        activitySource = "HISTORY",
                        activityStart = activity.StartDate.ToLocalTime().ToString("dd/MM/yyyy"),
                        activityType = activity.ActivityType,
                        clinician = activity.Clinician,
                        orderingClinician = activity.OrderingClinician,
                        duration = activity.Duration,
                        quantity = activity.Quantity,
                        unit = activity.Unit,
                        claimSubmissionObservationList = activity.ObservationList != null
                                ? activity.ObservationList.Select(obs => new ClaimSubmissionObservation
                                {
                                    observationCode = obs.Code,
                                    observationValue = obs.Value,
                                    observationValueType = obs.ValueType,
                                    observationType = obs.Type,
                                }).ToList()
                                : null
                    }));
                }

                // set claim diagnosis
                claim.claimSubmissionDiagnosisList = request.DiagnosisList != null
                    ? request.DiagnosisList.Select(x => new ClaimSubmissionDiagnosis()
                    {
                        diagnosisCode = x.Code,
                        diagnosisSource = x.Source,
                        diagnosisType = x.Type
                    }).ToList()
                    : null;

                // add ChronicDiseases as dignosis history for the patient
                if (request.ChronicDiseases?.Count > 0)
                {
                    var filteredChronicDiseases = request.ChronicDiseases.Where(a => !
                            claim.claimSubmissionDiagnosisList.Select(c => c.diagnosisCode).Contains(a.Code))?.Select(c => new ClaimSubmissionDiagnosis
                            {
                                diagnosisCode = c.Code,
                                diagnosisType = "Secondary", // set the type of dignosis as Secondary for ChronicDiseases
                                diagnosisSource = "HISTORY",
                            })
                        .ToList();

                    if (filteredChronicDiseases?.Count > 0)
                    {
                        claim.claimSubmissionDiagnosisList.AddRange(filteredChronicDiseases);
                    }
                }

                // set ClaimSubmissionPerson
                var patient = _repository.Find<Patient>(
                    p => p.Id == request.PatientId && !p.IsDeleted && p.DateOfBirth > DateTimeOffset.MinValue);

                if (patient != null)
                {
                    claim.claimSubmissionPerson = new ClaimSubmissionPerson
                    {
                        dateOfBirth = patient.DateOfBirth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),                       
                        nationalId = patient.NationalId,
                        patientId = patient.Code,
                        weight = request.Weight,
                        height = request.height

                    };
                }

                // set ClaimSubmissionAllergy
                if (request.AllergiesList != null && request.AllergiesList.Count != 0)
                {
                    claim.claimSubmissionAllergyList = request.AllergiesList.Select(p => new ClaimSubmissionAllergy()
                    {
                        allergyCode = p.Code,
                        allergyId = p.Code

                    }).ToList();
                }

                medicalEditsRequest.claims = new List<Claim>();
                medicalEditsRequest.claims.Add(claim);

                return medicalEditsRequest;
            }
            catch
            {
                return null;
            }
        }
    }
}
