using Application.Common;
using Application.Project.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Project.Commands
{
    public class UpdateProjectCommand : ProjectDto, IRequest<string>
    {
    }

    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, string>
    {
        private readonly IDataContext dataContext;

        public UpdateProjectCommandHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<string> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await dataContext.Projects.FirstOrDefaultAsync(p => p.Name == request.Name);
            if (project == null)
            {
                return "Project not found";
            }

            if (request.TeamId != project.TeamId)
            {
                project.TeamId = request.TeamId;
            }
            if (request.Name != project.Name)
            {
                project.Name = request.Name;
            }
            if (request.Description != project.Description)
            {
                project.Description = request.Description;
            }
            if (request.ClientName != project.ClientName)
            {
                project.ClientName = request.ClientName;
            }
            if (request.ClientId != project.ClientId)
            {
                project.ClientId = request.ClientId;
            }
            if (request.StartDate != project.EstimatedDeliveryDate)
            {
                project.StartDate = request.StartDate;
            }
            if (request.EstimatedDeliveryDate != project.EstimatedDeliveryDate)
            {
                project.StartDate = request.StartDate;
            }
            if (request.EstimatedWorkingHours != project.EstimatedWorkingHours)
            {
                project.EstimatedWorkingHours = request.EstimatedWorkingHours;
            }
            if (request.HoursInvested != project.HoursInvested)
            {
                project.HoursInvested = request.HoursInvested;
            }
            if (request.BillRate != project.BillRate)
            {
                project.BillRate = request.BillRate;
            }
            if (request.IsActive != project.IsActive)
            {
                project.IsActive = request.IsActive;
            }

            await dataContext.SaveChangesAsync(cancellationToken);

            return "Successful";
        }
    }
}