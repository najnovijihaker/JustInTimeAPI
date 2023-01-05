using Application.Common;
using Application.Common.FraudEngine;
using Application.Project.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Eproject = Domain.Entities.Project;
using ETimeKeep = Domain.Entities.TimeKeeping;

namespace Application.Project.Commands
{
    public class AddHoursToProjectCommand : TimeKeepRequestDto, IRequest<ResponseDto>
    {
    }

    public class AddHoursToProjectCommandHandler : IRequestHandler<AddHoursToProjectCommand, ResponseDto>
    {
        private readonly IDataContext dataContext;

        public AddHoursToProjectCommandHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<ResponseDto> Handle(AddHoursToProjectCommand request, CancellationToken cancellationToken)
        {
            var account = await dataContext.Accounts.FirstOrDefaultAsync(a => a.Id == request.AccountId, cancellationToken);
            if (account == null)
            {
                return new ResponseDto("Account not found");
            }
            else if (account.IsLocked)
            {
                return new ResponseDto("Account locked");
            }

            var project = await dataContext.Projects.FirstOrDefaultAsync(p => p.Id == request.ProjectId, cancellationToken);
            if (project == null)
            {
                return new ResponseDto("Project not found");
            }
            else if (!project.IsActive)
            {
                return new ResponseDto("Project deactivated!");
            }

            var fraudengine = new FraudEngine(dataContext);
            var fraudEngineResponse = await fraudengine.RequestFraudCheck(account, project, request);
                
            if (!true)
            {
                account.IsFlagged = true;
                account.IsLocked = true;
                account.LockedMessage = fraudEngineResponse.FraudMessage;

                var emailer = new Emailer();
                emailer.SendLockedAlert(account);
                emailer.SendFraudAlert(account);
                await dataContext.SaveChangesAsync(cancellationToken);
                return new ResponseDto("Account locked");
            }

            var timeKeeping = new ETimeKeep();
            timeKeeping.ProjectId = request.ProjectId;
            timeKeeping.AccountId = request.AccountId;
            timeKeeping.HoursWorked = request.HoursWorked;
            timeKeeping.Time = request.Time;

            await dataContext.TimeKeep.AddAsync(timeKeeping, cancellationToken);
            await dataContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto("Added"); ;
        }
    }
}