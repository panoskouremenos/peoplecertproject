using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PC_backend.Dto
{
    public class CandidateCreateDto
    {
        [Required]
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        [Required]
        public string? LastName { get; set; }
        public bool? Gender { get; set; }
        [Required]
        public string? NativeLanguage { get; set; }
        [Required]
        public DateTime? BirthDate { get; set; }
        [Required]
        public string? Email { get; set; }

        public string? LandlineNumber { get; set; }
        [Required]
        public string? MobileNumber { get; set; }

        public List<CandidateAddressCreateDto>? Addresses { get; set; }
        public List<CandidatePhotoIDDto>? PhotoIDs { get; set; }
    }

    public class CandidatePhotoIDDto
    {
        [Required]
        public int? PhotoIdtype { get; set; }
        [Required]
        public string? PhotoIdnumber { get; set; }
        [Required]
        public DateTime? PhotoIdissueDate { get; set; }
    }

    public class CandidateAddressCreateDto
    {
        [Required]
        public string? Address { get; set; }
        //[Required]
        public string? AddressLine2 { get; set; }
        [Required]
        public string? CountryOfResidence { get; set; }
        [Required]
        public string? StateTerritoryProvince { get; set; }
        [Required]
        public string? TownCity { get; set; }
        [Required]
        public string? PostalCode { get; set; }
    }

}
