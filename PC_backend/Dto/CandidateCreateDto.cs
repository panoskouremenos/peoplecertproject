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
