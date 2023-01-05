using Application.Account.Dtos;
using Application.Account.Dtos.Response;
using Application.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Account.Queries
{
    public class AccountStatisticsQuery : IRequest<List<AccountStatisticsResponseDto>>
    {
        public int AccountId { get; set; }

        public AccountStatisticsQuery(int accountId)
        {
            AccountId = accountId;
        }
    }

    public class AccountStatisticsQueryHandler : List, IRequestHandler<AccountStatisticsQuery, List<AccountStatisticsResponseDto>>
    {
        private readonly IDataContext dataContext;

        public AccountStatisticsQueryHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<List<AccountStatisticsResponseDto>> Handle(AccountStatisticsQuery request, CancellationToken cancellationToken)
        {
            var statistics = new List<AccountStatisticsResponseDto>();
            var account = await dataContext.Accounts.FirstOrDefaultAsync(a => a.Id == request.AccountId);
            var projects = await dataContext.Projects.ToListAsync();

            if (account == null)
            {
                //return new List<AccountStatisticsResponseDto>();
                return null;
            }

            var accountProjects = await dataContext.TimeKeep
           .Where(x => x.AccountId == request.AccountId)
           .ToListAsync();

            //var lastProjectId = -1;

            foreach (var log in accountProjects)
            {
                var response = new AccountStatisticsResponseDto();

                var project = projects.FirstOrDefault(p => p.Id == log.ProjectId); 
                if (project != null)
                {
                    response.AccountId = account.Id;
                    response.Username = account.Username;
                    response.ProjectId = log.ProjectId;
                    response.HoursWorked = log.HoursWorked;
                    response.LogDate = log.Time;
                    var projectName = project.Name;
                    response.ProjectName = projectName;

                    statistics.Add(response);
                }
            }

            return statistics;
        }
    }
}