using Application.Common;
using Application.Common.Helpers;
using Application.Project.Dtos;
using Application.TimeKeep.Dtos;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TimeKeep.Commands
{
    public class StartBreakCommand : PunchDto, IRequest<ResponseDto>
    {
    }

    public class StartBreakCommandHandler : IRequestHandler<StartBreakCommand, ResponseDto>
    {
        private readonly IDataContext dataContext;

        public StartBreakCommandHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<ResponseDto> Handle(StartBreakCommand request, CancellationToken cancellationToken)
        {
            var punchHelper = new PunchHelper(dataContext);

            var currentPunch = punchHelper.GetCurrentPunch(request.AccountId);

            if (currentPunch == null || currentPunch.Type == PunchType.Out)
            {
                return new ResponseDto("Unable to break if user is not punched in");
            }
            else if (currentPunch.Type == PunchType.BreakStart)
            {
                return new ResponseDto("User already on break");
            }

            var punch = new Punch(request.AccountId, request.ProjectId, DateTime.Now, PunchType.BreakStart);
            await dataContext.Punches.AddAsync(punch);
            await dataContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto("Break started");
        }
    }
}