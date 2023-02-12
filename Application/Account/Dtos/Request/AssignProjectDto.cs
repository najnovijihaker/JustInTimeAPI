using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Account.Dtos.Request
{
    public class AssignProjectDto
    {
        public int AccountId { get; set; }
        public int ProjectId { get; set; }
    }
}