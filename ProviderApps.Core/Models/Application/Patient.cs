using System;
using System.Collections.Generic;

namespace ProviderApps.Core.Models
{
    public class Patient : BaseModelIntActive, IBaseModelIsDeleted 
    {
        public Patient()
        {
            Insurances = new List<PatientInsurance>();
        }

        /// <summary>
        /// Represents the Profile Id.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// So far, it is not used in the View Model.
        /// </summary>
        public string Address { get; set; }

        public string ContactNumber { get; set; }

        public string EmergencyContactNumber { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }

        /// <summary>
        /// So far, it is not used in the View Model.
        /// </summary>
        public string Email { get; set; }

        public int? GenderId { get; set; }

        public string Name { get; set; }

        public string NationalId { get; set; }

        public string Picture { get; set; }

        public int? NoNationalReasonId { get; set; }

        public int? BloodGroupId { get; set; }

        /// <summary>
        /// Check why we have counter table and why this filed not mapped to counter table.
        /// </summary>
        public int? NationalityId { get; set; }

        public decimal? Weight { get; set; }

        public int? ResidencyTypeId { get; set; }

        public int? DocumentTypeId { get; set; }

        // Single, Married (default: none)
        public int? MaritalStatusId { get; set; }
        // No, Yes (default: null)
        public bool? Pregnant { get; set; }
        public string Notes { get; set; }
        public bool IsDeleted { get; set; }

        public string PreferredLanguage { get; set; }

        public string EHealthID { get; set; }

        public int? OrganizationId { get; set; }

        public int? AddressId { get; set; }

        public ICollection<PatientInsurance> Insurances { get; set; }
    }
}