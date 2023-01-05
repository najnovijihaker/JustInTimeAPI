using Application.Account.Dtos.Response;
using Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TimeKeep.Queries
{
    public class RecentLogsQuery : IRequest<List<AccountStatisticsResponseDto>>
    {
    }

    public class RecentLogsQueryHandler : IRequestHandler<RecentLogsQuery, List<AccountStatisticsResponseDto>>
    {
        private readonly IDataContext dataContext;

        public RecentLogsQueryHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<List<AccountStatisticsResponseDto>> Handle(RecentLogsQuery request, CancellationToken cancellationToken)
        {
            var response = new List<AccountStatisticsResponseDto>();
            var timeKeeps = await dataContext.TimeKeep
            .OrderByDescending(x => x.Id)
            .Take(10)
            .ToListAsync();

            foreach (var keep in timeKeeps)
            {
                var e = new AccountStatisticsResponseDto();
                e.LogDate = keep.Time;
                e.AccountId = keep.AccountId;
                e.HoursWorked = keep.HoursWorked;
                e.ProjectId = keep.ProjectId;

                response.Add(e);
            }

            return response;
        }
    }
}