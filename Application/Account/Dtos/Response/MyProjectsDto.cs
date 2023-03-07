using Domain.Entities;
using System.Collections.Generic;
using EProject = Domain.Entities.Project;

namespace Application.Account.Dtos.Response
{
    public class MyProjectsDto
    {
        public List<EProject> Projects { get; set; }

        public MyProjectsDto(List<EProject> projects)
        {
            Projects = projects;
        }
    }
}