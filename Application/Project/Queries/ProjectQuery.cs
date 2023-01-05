using Application.Common;
using Application.Project.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Project.Queries
{
    public class ProjectQuery : IRequest<ProjectDto>
    {
        public int Id { get; set; }

        public ProjectQuery(int Id)
        {
            this.Id = Id;
        }
    }

    public class ProjectQueryHandler : IRequestHandler<ProjectQuery, ProjectDto>
    {
        private readonly IDataContext dataContext;

        public ProjectQueryHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<ProjectDto> Handle(ProjectQuery request, CancellationToken cancellationToken)
        {
            var project = await dataContext.Projects.FirstOrDefaultAsync(p => p.Id == request.Id);
            if (project == null) return null;

            var projectDto = new ProjectDto(project.Id, project.TeamId, project.Name, project.Description, project.ClientName, project.ClientId, project.StartDate,
                project.EstimatedDeliveryDate, project.EstimatedWorkingHours, project.HoursInvested, project.BillRate, project.IsActive);

            return projectDto;
        }
    }
}