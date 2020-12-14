**Medical Edits Service API**

This is a .NET Core console application for implement Medical Edits Service for
Provider Application, this service can be used to check medical edits for
prescriptions.

**Technologies**

-   .NET Core 3.1

**Getting Started**

1.  Add reference to MedicalEdits project.

2.  Register medicalEdits service in startup by calling ‘AddMedicalEditsService’
    method.

3.  Using ‘GetClaimsEdits’ method in MedicalEditsService to get the medical
    edits.

**How to use**

1.  Register Medical Edits service in startup.

    services.AddMedicalEditsService();

2.  Use MedicalEditsService to call GetClaimEdits API.

-   Pass Medical Edit Request object (type of MedicalEditsViewModel)

-   Pass cancellationToken (optional).

var result = await
\_medicalEditsService.GetClaimsEdits(medicalEditsServiceRequest);

1.  The success field returned in medical edits result indicates Whether the
    service call is successful or not.

2.  The ClaimEdits field contains List of claim edits for all submitted claims.
