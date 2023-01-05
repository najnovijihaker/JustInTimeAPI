using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TimeKeep.Dtos.Response
{
    public class MonthlyHoursResponseDto
    {
        public double Hours { get; set; }

        public MonthlyHoursResponseDto(double hours)
        {
            Hours = hours;
        }
    }
}