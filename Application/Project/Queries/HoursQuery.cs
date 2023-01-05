using Application.Common;
using Application.TimeKeep.Dtos;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Account.Queries
{
    public class HoursQuery : IRequest<double>
    {
        public int projectId { get; set; }

        public HoursQuery(int projectId)
        {
            this.projectId = projectId;
        }
    }

    public class HoursQueryHandler : IRequestHandler<HoursQuery, double>
    {
        private readonly IDataContext dataContext;

        public HoursQueryHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<double> Handle(HoursQuery request, CancellationToken cancellationToken)
        {
            var hours = await dataContext.TimeKeep
            .Where(x => x.ProjectId == request.projectId)
            .ToListAsync();

            var workingHours = 0.0;

            foreach (var hour in hours)
            {
                workingHours += hour.HoursWorked;
            }

            return workingHours;
        }
    }
}