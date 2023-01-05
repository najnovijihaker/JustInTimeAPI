using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Account.Dtos.Response
{
    public class AccountStatisticsResponseDto
    {
        public string ProjectName { get; set; } = string.Empty;
        public int ProjectId { get; set; }
        public string Username { get; set; } = string.Empty;
        public int AccountId { get; set; }
        public double HoursWorked { get; set; }
        public DateTime LogDate { get; set; }
    }
}