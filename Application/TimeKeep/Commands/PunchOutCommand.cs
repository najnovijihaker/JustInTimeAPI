using Application.Account.Dtos;
using Application.Common;
using Application.Common.Helpers;
using Application.Project.Dtos;
using Application.TimeKeep.Dtos;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETimeKeep = Domain.Entities.TimeKeeping;

namespace Application.TimeKeep.Commands
{
    public class PunchOutCommand : PunchDto, IRequest<ResponseDto>
    {
    }

    public class PunchOutCommandHandler : IRequestHandler<PunchOutCommand, ResponseDto>
    {
        private readonly IDataContext dataContext;

        public PunchOutCommandHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<ResponseDto> Handle(PunchOutCommand request, CancellationToken cancellationToken)
        {
            AccountHelper helper = new AccountHelper(dataContext);
            if (!helper.ExistsById(request.AccountId))
            {
                return new ResponseDto("Account not found!");
            }
            // Check if employee is already punched in
            var currentPunch = GetCurrentPunch(request.AccountId);
            if (currentPunch == null)
            {
                return new ResponseDto("Unexpected error");
            }

            if (currentPunch != null && currentPunch.Type == PunchType.Out)
            {
                return new ResponseDto("Employee is already punched out.");
            }
            var punch = new Punch(request.AccountId, currentPunch.ProjectId, DateTime.Now, PunchType.Out);

            await dataContext.Punches.AddAsync(punch, cancellationToken);
            var timeWorked = 0.0;
            // calculate and log hours
            if (currentPunch != null)
            {
                var punchHelper = new PunchHelper(dataContext);
                var deductions = punchHelper.CalculateBreakTime(request.AccountId);

                timeWorked = ((DateTime.Now - currentPunch.TimeStamp).TotalHours) - deductions;

                ETimeKeep timeKeep = new()
                {
                    AccountId = request.AccountId,
                    Time = DateTime.Now,
                    HoursWorked = timeWorked,
                    ProjectId = currentPunch.ProjectId
                };

                await dataContext.TimeKeep.AddAsync(timeKeep);
            }

            await dataContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto($"Punched Out and {timeWorked} hours logged");
        }

        private Punch GetCurrentPunch(int employeeId)
        {
            var punches = dataContext.Punches
                .Where(p => p.AccountId == employeeId)
                .OrderByDescending(p => p.TimeStamp)
                .FirstOrDefault();

            return punches;
        }
    }
}