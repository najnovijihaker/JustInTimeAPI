using Application.Common;
using Application.Project.Dtos;
using Application.TimeKeep.Dtos;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TimeKeep.Commands
{
    public class PunchInCommand : PunchDto, IRequest<ResponseDto>
    {
    }

    public class PunchInCommandHandler : IRequestHandler<PunchInCommand, ResponseDto>
    {
        private readonly IDataContext dataContext;

        public PunchInCommandHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<ResponseDto> Handle(PunchInCommand request, CancellationToken cancellationToken)
        {
            // Check if employee is already punched in
            var currentPunch = GetCurrentPunch(request.AccountId);

            if (currentPunch != null && currentPunch.Type == PunchType.In)
            {
                return new ResponseDto("Employee is already punched in.");
            }
            var punch = new Punch(request.AccountId, request.ProjectId, DateTime.Now, PunchType.In);

            await dataContext.Punches.AddAsync(punch, cancellationToken);
            await dataContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto("Punched In!");
        }

        private Punch GetCurrentPunch(int employeeId)
        {
            return dataContext.Punches
                .Where(p => p.AccountId == employeeId)
                .OrderByDescending(p => p.TimeStamp)
                .FirstOrDefault();
        }
    }
}