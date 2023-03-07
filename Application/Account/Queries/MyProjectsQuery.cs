using Application.Account.Dtos.Response;
using Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using EProject = Domain.Entities.Project;

namespace Application.Account.Queries
{
    public class MyProjectsQuery : IRequest<MyProjectsDto>
    {
        public int accountId { get; set; }

        public MyProjectsQuery(int accountId)
        {
            this.accountId = accountId;
        }
    }

    public class MyProjectsQueryHandler : IRequestHandler<MyProjectsQuery, MyProjectsDto>
    {
        private readonly IDataContext dataContext;

        public MyProjectsQueryHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<MyProjectsDto> Handle(MyProjectsQuery request, CancellationToken cancellationToken)
        {
            var myProjects = await dataContext.AccountProjects
                .Where(x => x.AccountId == request.accountId)
                .Join(dataContext.Projects,
                 accountProject => accountProject.ProjectId,
                 project => project.Id,
                (accountProject, project) => project)
                .ToListAsync(cancellationToken);

            return new MyProjectsDto(myProjects);

            // get all of the accountProjects record for user
            //var accountProjects = await dataContext.AccountProjects.Where(x => x.AccountId == request.accountId).ToListAsync(cancellationToken);
            //var projects = new List<EProject>();

            //foreach (var record in accountProjects)
            //{
            //    projects.Add(await dataContext.Projects.FirstOrDefaultAsync(x => x.Id == record.ProjectId));
            //}

            //return new MyProjectsDto(projects);
        }
    }
}