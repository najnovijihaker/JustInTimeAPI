using Application.Account.Dtos;
using Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Account.Queries
{
    public class AccountQuery : IRequest<AccountDto>
    {
        public string Username { get; set; }

        public AccountQuery(string username)
        {
            this.Username = username;
        }
    }

    public class AccountQueryHandler : AccountDto, IRequestHandler<AccountQuery, AccountDto>
    {
        private readonly IDataContext dataContext;
        private int AccountId { get; set; }

        public AccountQueryHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<AccountDto> Handle(AccountQuery request, CancellationToken cancellationToken)
        {
            var selectedAccount = await dataContext.Accounts.FirstOrDefaultAsync(a => a.Username == request.Username);

            if (selectedAccount == null)
            {
                return null;
            }

            AccountId = selectedAccount.Id;

            var weeklyHours = GetWeeklyHours(AccountId);

            var monthlyHours = GetMonthlyHours(AccountId);

            var accountDto = new AccountDto(selectedAccount.Id, selectedAccount.FirstName, selectedAccount.LastName, selectedAccount.Username,
                selectedAccount.Email, selectedAccount.role, weeklyHours, monthlyHours, selectedAccount.IsLocked, selectedAccount.LockedMessage);

            return accountDto;
        }

        private double GetWeeklyHours(int accountId)
        {
            var startOfWeek = GetStartOfWeek();
            var endOfWeek = startOfWeek.AddDays(7);

            var result = GetHoursWorked(startOfWeek, endOfWeek, accountId);

            return result;
        }

        public double GetMonthlyHours(int accountId)
        {
            var startOfMonth = GetStartOfMonth();
            var endOfMonth = GetEndOfMonth();

            var result = GetHoursWorked(startOfMonth, endOfMonth, accountId);

            return result;
        }

        private double GetHoursWorked(DateTime startDate, DateTime endDate, int accountId)
        {
            var timeEntries = dataContext.TimeKeep.Where(x => x.Time >= startDate && x.Time < endDate && x.AccountId == accountId);
            var result = timeEntries.Sum(x => x.HoursWorked);

            return result;
        }

        private DateTime GetStartOfWeek()
        {
            // Get the current date and time
            var currentDate = DateTime.Now;

            // Get the number of days until the next Sunday
            var daysUntilSunday = DayOfWeek.Sunday - currentDate.DayOfWeek;

            // If it is already Sunday, set the number of days to 0
            if (daysUntilSunday < 0)
            {
                daysUntilSunday = 0;
            }

            // Calculate the start of the week by subtracting the number of days until Sunday from the current date
            return currentDate.AddDays(-1 * (int)currentDate.DayOfWeek);
        }

        private DateTime GetStartOfMonth()
        {
            // Get the current date and time
            var currentDate = DateTime.Now;

            // Calculate the start of the month by setting the day to 1
            return new DateTime(currentDate.Year, currentDate.Month, 1);
        }

        private DateTime GetEndOfMonth()
        {
            // Get the current date and time
            var currentDate = DateTime.Now;

            // Calculate the end of the month by setting the day to the last day of the month
            return new DateTime(currentDate.Year, currentDate.Month, DateTime.DaysInMonth(currentDate.Year, currentDate.Month));
        }
    }
}