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
using System.Reflection;
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
            // var statistics = new List<AccountStatisticsResponseDto>();
            // var account = await dataContext.Accounts.FirstOrDefaultAsync(a => a.Id == request.AccountId, cancellationToken);
            // var projects = await dataContext.Projects.ToListAsync(cancellationToken);

            // if (account == null)
            // {
            //     return null;
            // }

            // var accountProjects = await dataContext.TimeKeep
            //.Where(x => x.AccountId == request.AccountId)
            //.ToListAsync(cancellationToken);

            // foreach (var log in accountProjects)
            // {
            //     var response = new AccountStatisticsResponseDto();

            //     var project = projects.FirstOrDefault(p => p.Id == log.ProjectId);
            //     if (project != null)
            //     {
            //         response.AccountId = account.Id;
            //         response.Username = account.Username;
            //         response.ProjectId = log.ProjectId;
            //         response.HoursWorked = log.HoursWorked;
            //         response.LogDate = log.Time;
            //         var projectName = project.Name;
            //         response.ProjectName = projectName;

            //         statistics.Add(response);
            //     }
            // }

            // return statistics;

            // get Account
            var account = await dataContext.Accounts.FirstOrDefaultAsync(x => x.Id == request.AccountId, cancellationToken);
            if (account == null)
            {
                // return empty object
                return new List<AccountStatisticsResponseDto>();
            }
            // get account projects
            var accountProjects = await dataContext.AccountProjects.Where(x => x.AccountId == request.AccountId).ToListAsync(cancellationToken);
            if (!accountProjects.Any())
            {
                // return empty object
                return new List<AccountStatisticsResponseDto>();
            }

            var response = new List<AccountStatisticsResponseDto>();

            // mapping
            foreach (var project in accountProjects)
            {
                var responseDto = new AccountStatisticsResponseDto();

                response.Add(responseDto);
            }

            return response;
        }
    }
}