using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Account.Dtos.Request
{
    public class SendReportRequestDto
    {
        public int SenderId { get; set; }
        public int RecieverId { get; set; }
    }
}