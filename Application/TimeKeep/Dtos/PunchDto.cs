using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TimeKeep.Dtos
{
    public class PunchDto
    {
        public int AccountId { get; set; }
        public int ProjectId { get; set; }
        public DateTime PunchIn { get; set; }
        public DateTime PunchOut { get; set; }
        public PunchType Type { get; set; }
    }
}