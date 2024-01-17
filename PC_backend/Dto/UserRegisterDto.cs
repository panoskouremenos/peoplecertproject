namespace PC_backend.Dto
{
    public class UserRegisterDto
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }

    public class UserLoginDto
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
	public class User
	{
		public int UserId { get; set; }
		public int RoleId { get; set; }

		public Role Role { get; set; }
	}

	public class Role
	{
		public int RoleId { get; set; }
		public string RoleName { get; set; }
	}
}

