using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PC_backend.Dto
{
	public class ChangePasswordDto
	{
		public string? NewPassword { get; set; }
	}

	public class ChangeUsernameDto
	{
		public string? NewUsername { get; set; }
	}
}