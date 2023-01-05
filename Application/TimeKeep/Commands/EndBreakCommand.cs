using Application.Common;
using Application.Common.Helpers;
using Application.Project.Dtos;
using Application.TimeKeep.Dtos;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TimeKeep.Commands
{
    public class EndBreakCommand : PunchDto, IRequest<ResponseDto>
    {
    }

    public class EndBreakCommandHandler : IRequestHandler<EndBreakCommand, ResponseDto>
    {
        private readonly IDataContext dataContext;

        public EndBreakCommandHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<ResponseDto> Handle(EndBreakCommand request, CancellationToken cancellationToken)
        {
            var punchHelper = new PunchHelper(dataContext);

            var currentPunch = punchHelper.GetCurrentPunch(request.AccountId);

            if (currentPunch == null || currentPunch.Type == PunchType.Out)
            {
                return new ResponseDto("Unable to end break if user is not punched in");
            }
            else if (currentPunch.Type == PunchType.BreakEnd)
            {
                return new ResponseDto("User already ended break");
            }

            var punch = new Punch(request.AccountId, currentPunch.ProjectId, DateTime.Now, PunchType.BreakEnd);
            await dataContext.Punches.AddAsync(punch);
            await dataContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto("Break ended");
        }
    }
}