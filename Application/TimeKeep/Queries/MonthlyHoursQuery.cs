using Application.Common;
using Application.TimeKeep.Dtos.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TimeKeep.Queries
{
    public class MonthlyHoursQuery : IRequest<MonthlyHoursResponseDto>
    {
    }

    public class MonthlyHoursQueryHandler : IRequestHandler<MonthlyHoursQuery, MonthlyHoursResponseDto>
    {
        private readonly IDataContext dataContext;

        public MonthlyHoursQueryHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<MonthlyHoursResponseDto> Handle(MonthlyHoursQuery request, CancellationToken cancellationToken)
        {
            var timeKeep = await dataContext.TimeKeep.ToListAsync(cancellationToken);
            if (timeKeep == null)
            {
                return new MonthlyHoursResponseDto(0);
            }

            double hours = 0;

            int currentMonth = DateTime.Now.Month;

            foreach (var keep in timeKeep)
            {
                if (keep.Time.Month == currentMonth)
                {
                    hours += keep.HoursWorked;
                }
            }

            return new MonthlyHoursResponseDto(hours);
        }
    }
}