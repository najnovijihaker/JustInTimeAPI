using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EProject = Domain.Entities.Project;

namespace Application.Account.Dtos.Response
{
    public class MyProjectsDto
    {
        public List<EProject> Projects = new List<EProject>();

        public MyProjectsDto(List<EProject> projects)
        {
            Projects = projects;
        }
    }
}