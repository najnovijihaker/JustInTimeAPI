using Application.Common;
using Application.Project.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Project.Commands
{
    public class ActivateProjectCommand : ProjectDto, IRequest<ResponseDto>
    {
    }

    public class ActivateProjectCommandHandler : IRequestHandler<ActivateProjectCommand, ResponseDto>
    {
        private readonly IDataContext dataContext;

        public ActivateProjectCommandHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<ResponseDto> Handle(ActivateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await dataContext.Projects.FirstOrDefaultAsync(p => p.Id == request.Id);

            if (project != null)
            {
                project.IsActive = true;
                await dataContext.SaveChangesAsync(cancellationToken);
                return new ResponseDto("Project activated!");
            }

            return new ResponseDto("Project not found!");
        }
    }
}