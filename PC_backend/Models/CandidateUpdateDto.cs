using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PC_backend.Models
{
	public class CandidateUpdateDto
	{
		// Assuming the candidate's ID is needed to identify which candidate to update
		[Required]
		public int CandidateId { get; set; }

		// Include the same properties as in CandidateCreateDto
		public string? FirstName { get; set; }
		public string? MiddleName { get; set; }
		public string? LastName { get; set; }
		public string? Gender { get; set; }
		public DateTime? BirthDate { get; set; }
		public string? NativeLanguage { get; set; }
		public string? Email { get; set; }
		public string? LandlineNumber { get; set; }
		public string? MobileNumber { get; set; }

		// Lists for addresses and photo IDs with identifiers
		public List<CandidateAddressUpdateDto>? Addresses { get; set; }
		public List<CandidatePhotoIDUpdateDto>? PhotoIDs { get; set; }
	}

	public class CandidatePhotoIDUpdateDto
	{
		// Include an identifier for the photo ID
		public int PhotoIdId { get; set; }

		public string? PhotoIdtype { get; set; }
		public string? PhotoIdnumber { get; set; }
		public DateTime? PhotoIdissueDate { get; set; }
	}

	public class CandidateAddressUpdateDto
	{
		// Include an identifier for the address
		public int AddressId { get; set; }

		public string? Address { get; set; }
		public string? AddressLine2 { get; set; }
		public string? CountryOfResidence { get; set; }
		public string? StateTerritoryProvince { get; set; }
		public string? TownCity { get; set; }
		public string? PostalCode { get; set; }
	}
}
