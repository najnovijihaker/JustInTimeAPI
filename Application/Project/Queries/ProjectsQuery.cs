using Application.Account.Dtos.Response;
using Application.Common;
using Application.Project.Dtos;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using EProject = Domain.Entities.Project;

namespace Application.Project.Queries
{
    public class ProjectsQuery : IRequest<List<ProjectDto>>
    {
        public bool Active { get; set; }

        public ProjectsQuery()
        {
            Active = true;
        }

        public ProjectsQuery(bool isActive)
        {
            Active = isActive;
        }
    }

    public class ProjectsQueryHandler : List, IRequestHandler<ProjectsQuery, List<ProjectDto>>
    {
        private IDataContext dataContext;

        public ProjectsQueryHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<List<ProjectDto>> Handle(ProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = new List<ProjectDto>();
            var eProjects = new List<EProject>();
            if (request.Active)
            {
                eProjects = await dataContext.Projects.Where(p => p.IsActive).ToListAsync(cancellationToken);
            }
            else
            {
                eProjects = await dataContext.Projects.Where(p => p.IsActive == false).ToListAsync(cancellationToken);
            }

            if (eProjects.Count == 0)
            {
                return new List<ProjectDto>();
            }

            foreach (var eProject in eProjects)
            {
                var projectDto = new ProjectDto(eProject.Id, eProject.TeamId, eProject.Name, eProject.Description, eProject.ClientName, eProject.ClientId, eProject.StartDate,
                eProject.EstimatedDeliveryDate, eProject.EstimatedWorkingHours, eProject.HoursInvested, eProject.BillRate, eProject.IsActive);

                projects.Add(projectDto);
            }

            return projects;
        }
    }
}