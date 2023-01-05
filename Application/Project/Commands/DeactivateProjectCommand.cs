using Application.Common;
using Application.Project.Dtos;
using MediatR;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Project.Commands
{
    public class DeactivateProjectCommand : ProjectDto, IRequest<ResponseDto>
    {
    }

    public class DeactivateProjectCommandHandler : IRequestHandler<DeactivateProjectCommand, ResponseDto>
    {
        private readonly IDataContext dataContext;

        public DeactivateProjectCommandHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<ResponseDto> Handle(DeactivateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await dataContext.Projects.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (project != null)
            {
                project.IsActive = false;
                await dataContext.SaveChangesAsync(cancellationToken);
                return new ResponseDto("Project deactivated!");
            }

            return new ResponseDto("No project found");
        }
    }
}