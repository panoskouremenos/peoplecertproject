using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PC_backend.Dto
{
    public class CandidateUpdateDto
    {
        [Required]
        public int CandidateId { get; set; }

        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public bool? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? NativeLanguage { get; set; }
        public string? Email { get; set; }
        public string? LandlineNumber { get; set; }
        public string? MobileNumber { get; set; }

        public List<CandidateAddressUpdateDto>? Addresses { get; set; }
        public List<CandidatePhotoIDUpdateDto>? PhotoIDs { get; set; }
    }

    public class CandidatePhotoIDUpdateDto
    {
        public int PhotoIdId { get; set; }
        public int? PhotoIdtype { get; set; }
        public string? PhotoIdnumber { get; set; }
        public DateTime? PhotoIdissueDate { get; set; }
    }

    public class CandidateAddressUpdateDto
    {
        public int AddressId { get; set; }
        public string? Address { get; set; }
        public string? AddressLine2 { get; set; }
        public string? CountryOfResidence { get; set; }
        public string? StateTerritoryProvince { get; set; }
        public string? TownCity { get; set; }
        public string? PostalCode { get; set; }
    }
}
