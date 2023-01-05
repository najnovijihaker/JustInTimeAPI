using Application.Common;
using Application.Project.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Project.Commands
{
    public class DeleteProjectCommand : ProjectDto, IRequest<string>
    {
    }

    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, string>
    {
        private readonly IDataContext dataContext;

        public DeleteProjectCommandHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<string> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await dataContext.Projects.FirstOrDefaultAsync(a => a.Name == request.Name);
            if (project == null) return "Project not found";

            dataContext.Projects.Remove(project);
            await dataContext.SaveChangesAsync(cancellationToken);

            return "Project records erased";
        }
    }
}