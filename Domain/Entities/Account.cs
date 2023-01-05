namespace Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = false;
        public bool IsFlagged { get; set; } = false;
        public bool IsLocked { get; set; } = false; //lock only if flag is risen, user can login and export but cannot enter into TimeKeep
        public string LockedMessage { get; set; } = string.Empty;
        public string role { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string ActionToken { get; set; } = string.Empty;
        public DateTime? ActionTokenExp { get; set; }
    }
}