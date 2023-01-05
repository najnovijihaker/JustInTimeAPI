using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Project.Dtos
{
    public class TimeKeepRequestDto
    {
        public DateTime Time { get; set; } = DateTime.Now;
        public int ProjectId { get; set; }
        public int AccountId { get; set; }
        public double HoursWorked { get; set; }
    }
}