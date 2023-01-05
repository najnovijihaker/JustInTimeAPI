using Application.Common.FraudEngine.Responses;
using Application.Project.Dtos;
using Microsoft.EntityFrameworkCore;
using EAccount = Domain.Entities.Account;
using EProject = Domain.Entities.Project;

namespace Application.Common.FraudEngine
{
    internal class FraudEngine
    {
        // default sensitivty configuration

        private readonly double Treshold = 50; // if treshold is exceeded by points return true
        private readonly double HoursTolerance = 2.5; // The amount a request can differ from average before points are added
        private readonly double PercentageTolerance = 35; // The % amount a request can differ from average before points are added
        private readonly double RequestTimeDifferance = 5; // Seconds wthin a request cannot be made from a different request without adding points
        private readonly double SoftLimitWorkingHours = 9; // A soft limit for a high amount of working hours
        private readonly double HardLimitWorkingHours = 12; // A hard limit for a high amount of working hours
        private readonly double SoftDateLimit = 3; // A soft date limit for inputting hours (adds 15 points)
        private readonly double HardDateLimit = 5; // A hard date limit for inputting hours

        private double Points = 0;

        private readonly IDataContext dataContext;

        public FraudEngine(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public FraudEngine(IDataContext dataContext, FraudEngineConfig config)
        {
            this.dataContext = dataContext;

            Treshold = config.Treshold;
            HoursTolerance = config.HoursTolerance;
            PercentageTolerance = config.PercentageTolerance;
            RequestTimeDifferance = config.RequestTimeDifferance;
            SoftLimitWorkingHours = config.SoftLimitWorkingHours;
            HardLimitWorkingHours = config.HardLimitWorkingHours;
        }

        public async Task<FraudCheckResponse> RequestFraudCheck(EAccount account, EProject project, TimeKeepRequestDto request)
        {
            #region Request size

            // request is large
            if (request.HoursWorked > SoftLimitWorkingHours && request.HoursWorked < HardLimitWorkingHours)
            {
                Points += 35;
            }

            // request is too large
            if (request.HoursWorked > HardLimitWorkingHours)
            {
                return new FraudCheckResponse(true, "Request was too large");
            }

            #endregion Request size

            #region hours checking

            var hours = await dataContext.TimeKeep
            .Where(x => x.AccountId == account.Id)
            .ToListAsync();

            // allow for adjustment period before calculating averages
            if (hours.Count >= 5)
            {
                var average = hours.Average(x => x.HoursWorked);

                // if the request differes more than tolerance hours from the average hours
                if (Math.Abs(average - request.HoursWorked) > HoursTolerance)
                {
                    Points += 25;
                }

                // if the request is more than tolerance in % larger than average
                if ((request.HoursWorked / average) / average > PercentageTolerance)
                {
                    Points += 30;
                }
            }

            #endregion hours checking

            #region time checking

            // check if multiple requests within a too low timespan
            //var lastRequest = await dataContext.TimeKeep.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            //if (lastRequest != null)
            //{
            //    var timeSpan = request.Time.Subtract(lastRequest.Time);

            //    if (timeSpan.TotalSeconds <= RequestTimeDifferance)
            //    {
            //        return new FraudCheckResponse(true, "Too many requests");
            //    }
            //}

            //if (request.Time > DateTime.Now.AddMinutes(1))
            //{
            //    return true;
            //}

            //if ((DateTime.Now - request.Time).TotalDays <= SoftDateLimit)
            //{
            //    Points += 15;
            //}

            //if ((DateTime.Now - request.Time).TotalDays <= HardDateLimit)
            //{
            //    return true;
            //}

            #endregion time checking

            if (Points > Treshold)
            {
                return new FraudCheckResponse(true, "");
            }

            return new FraudCheckResponse(false, "");
        }
    }
}