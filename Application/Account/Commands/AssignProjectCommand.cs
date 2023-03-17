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

        public async Task<ResponseDto> Handle(AssignProjectCommand request, CancellationToken cancellatoinToken)
        {
            var account = await dataContext.Accounts.FirstOrDefaultAsync(x => x.Id == request.AccountId);
            var project = await dataContext.Projects.FirstOrDefaultAsync(x => x.Id == request.ProjectId || x.Name == request.ProjectName);

            if (account == null)
            {
                return new ResponseDto("Account not found");
            }

            if (project == null)
            {
                return new ResponseDto("Project not found");
            }

            // check if user is already added to project
            var control = await dataContext.AccountProjects.AnyAsync(x => x.ProjectId == project.Id && x.AccountId == account.Id);

            if (control == true)
            {
                return new ResponseDto("User already added!");
            }

            var accountProjects = new AccountProjects
            {
                AccountId = account.Id,
                ProjectId = project.Id
            };

            await dataContext.AccountProjects.AddAsync(accountProjects);
            await dataContext.SaveChangesAsync(cancellatoinToken);

            return new ResponseDto("Successful");
        }
    }
}