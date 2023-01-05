using Application.Common;
using Application.Project.Dtos;
using MediatR;
using EProject = Domain.Entities.Project;

namespace Application.Projec.Commands
{
    public class CreateProjectCommand : CreateProjectRequestDto, IRequest<ResponseDto>
    {
    }

    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ResponseDto>
    {
        private readonly IDataContext dataContext;

        public CreateProjectCommandHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<ResponseDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            if (Exists(request))
            {
                return new ResponseDto("Project already exists");
            }

            EProject eProject = new EProject();
            //eProject.TeamId = request.TeamId;
            eProject.Name = request.Name;
            eProject.Description = request.Description;
            eProject.ClientName = request.ClientName;
            //eProject.ClientId = request.ClientId;
            //eProject.StartDate = request.StartDate;
            //eProject.EstimatedDeliveryDate = request.EstimatedDeliveryDate;
            eProject.EstimatedWorkingHours = request.EstimatedWorkingHours;
            eProject.BillRate = request.BillRate;
            //eProject.HoursInvested = request.HoursInvested;
            eProject.IsActive = true;

            await dataContext.AddEntityToGraph(eProject);
            await dataContext.SaveChangesAsync(cancellationToken);
            return new ResponseDto("Successful");
        }

        public bool Exists(CreateProjectCommand request)
        {
            if (dataContext.Projects.Any(p => p.Name == request.Name))
            {
                return true;
            }
            return false;
        }

        //public bool IsDeactivated(CreateProjectCommand request)
        //{
        //    var userForCheck = dataContext.Accounts.FirstOrDefault(u => u.Username == request.UserName || u.Email == request.Email);
        //    if (userForCheck == null)
        //    {
        //        return false;
        //    }
        //    else if (!userForCheck.IsActive)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
    }
}