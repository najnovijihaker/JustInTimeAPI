using Application.Common;
using Application.Project.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Project.Commands
{
    public class DeleteProjectCommand : ProjectDto, IRequest<ResponseDto>
    {
    }

    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, ResponseDto>
    {
        private readonly IDataContext dataContext;

        public DeleteProjectCommandHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<ResponseDto> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await dataContext.Projects.FirstOrDefaultAsync(a => a.Id == request.Id);
            if (project == null)
            {
                return new ResponseDto("No project found");
            }

            dataContext.Projects.Remove(project);
            await dataContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto("Project records erased");
        }
    }
}