using Application.Common;
using Application.Project.Dtos;
using Application.TimeKeep.Dtos.Response;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;

namespace Application.TimeKeep.Queries
{
    public class ClockedAccountsQuery : IRequest<PunchesQueryDto>
    {
    }

    public class ClockedAccountsQueryHandler : IRequestHandler<ClockedAccountsQuery, PunchesQueryDto>
    {
        private readonly IDataContext dataContext;

        public ClockedAccountsQueryHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<PunchesQueryDto> Handle(ClockedAccountsQuery request, CancellationToken cancellationToken)
        {
            var currentPunches = await dataContext.Punches
            .Where(p => p.Type == PunchType.In)
            .GroupBy(p => p.AccountId)
            .Select(g => g.OrderByDescending(p => p.TimeStamp).FirstOrDefault()).ToListAsync();

            if (currentPunches == null)
            {
                throw new Exception("No Punches found");
            }

            return new PunchesQueryDto(currentPunches);
        }
    }
}