using Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Account.Queries
{
    public class AccountTotalHoursQuery : IRequest<double>
    {
        public int ProjectId { get; set; }
        public int AccountId { get; set; }

        public AccountTotalHoursQuery(int accountId, int projectId)
        {
            this.AccountId = accountId;
            this.ProjectId = projectId;
        }
    }

    public class AccountHoursQueryHandler : IRequestHandler<AccountTotalHoursQuery, double>
    {
        private readonly IDataContext dataContext;

        public AccountHoursQueryHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<double> Handle(AccountTotalHoursQuery request, CancellationToken cancellationToken)
        {
            var timeKeep = await dataContext.TimeKeep.Where(t => t.ProjectId == request.ProjectId && t.AccountId == request.AccountId).ToListAsync(cancellationToken);

            var workedHours = timeKeep.Sum(t => t.HoursWorked);

            return workedHours;
        }
    }
}