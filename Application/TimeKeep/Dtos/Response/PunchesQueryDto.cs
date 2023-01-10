using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TimeKeep.Dtos.Response
{
    public class PunchesQueryDto
    {
        public List<Punch> Punches { get; set; } = new List<Punch>();

        public PunchesQueryDto(List<Punch> punches)
        {
            Punches = punches;
        }
    }
}