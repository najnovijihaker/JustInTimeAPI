using Application.Account.Dtos.Request;
using Application.Common;
using Application.Project.Dtos;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Account.Commands
{
    public class AssignProjectCommand : AssignProjectDto, IRequest<ResponseDto>
    {
    }

    public class AssingProjectCommandHandler : IRequestHandler<AssignProjectCommand, ResponseDto>
    {
        private readonly IDataContext dataContext;

        public AssingProjectCommandHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<ResponseDto> Handle(AssignProjectCommand request, CancellationToken cancellation)
        {
            var account = dataContext.Accounts.FirstOrDefaultAsync(x => x.Id == request.AccountId);
            var project = dataContext.Projects.FirstOrDefault(x => x.Id == request.ProjectId);

            if (account == null)
            {
                return new ResponseDto("Account not found");
            }

            if (project == null)
            {
                return new ResponseDto("Project not found");
            }

            var accountProjects = new AccountProjects();

            accountProjects.AccountId = request.AccountId;
            accountProjects.ProjectId = request.ProjectId;

            await dataContext.AccountProjects.AddAsync(accountProjects);
            await dataContext.SaveChangesAsync(cancellation);

            return new ResponseDto("Successful");
        }
    }
}