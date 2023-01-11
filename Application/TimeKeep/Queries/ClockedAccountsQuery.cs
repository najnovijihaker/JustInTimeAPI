using Application.Common;
using Application.TimeKeep.Dtos.Response;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            .ToListAsync();

            if (currentPunches == null)
            {
                throw new Exception("No Punches Currently");
            }

            return new PunchesQueryDto(currentPunches);
        }
    }
}