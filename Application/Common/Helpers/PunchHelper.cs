using Domain.Entities;
using Domain.Enums;

namespace Application.Common.Helpers
{
    internal class PunchHelper
    {
        private readonly IDataContext dataContext;

        public PunchHelper(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public Punch GetCurrentPunchByAccountId(int accountId)
        {
            var result = dataContext.Punches
                .Where(p => p.AccountId == accountId)
                .OrderByDescending(p => p.TimeStamp)
                .FirstOrDefault();
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public double CalculateBreakTime(int accountId)
        {
            var startBreak = dataContext.Punches
                .Where(p => p.AccountId == accountId && p.Type == PunchType.BreakStart)
                .OrderByDescending(p => p.TimeStamp)
                .FirstOrDefault();

            var breakEnded = dataContext.Punches
                .Where(p => p.AccountId == accountId && p.Type == PunchType.BreakEnd)
                .OrderByDescending(p => p.TimeStamp)
                .FirstOrDefault();

            if (startBreak == null || breakEnded == null)
            {
                return 0;
            }

            double result = (breakEnded.TimeStamp - startBreak.TimeStamp).TotalHours;
            return result;
        }
    }
}