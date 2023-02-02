namespace Application.Account.Dtos
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public double WeeklyHours { get; set; }
        public double MonthlyHours { get; set; }
        public bool IsLocked { get; set; }
        public bool IsActivated { get; set; }
        public string LockedMessage { get; set; } = string.Empty;
        public bool IsPunchedIn { get; set; } = false;
        public bool IsOnBreak { get; set; }

        public AccountDto()
        {
        }

        public AccountDto(int id, string firstName, string lastName, string userName, string email, string role, double weeklyHours,
            double monthlyHours, bool isLocked, string lockedMessage, bool IsActivated, bool isPunchedIn, bool isOnBreak)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = email;
            Role = role;
            WeeklyHours = weeklyHours;
            MonthlyHours = monthlyHours;
            IsLocked = isLocked;
            LockedMessage = lockedMessage;
            this.IsActivated = IsActivated;
            IsPunchedIn = isPunchedIn;
            IsOnBreak = isOnBreak;
        }
    }
}