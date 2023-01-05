using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Punch
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int ProjectId { get; set; }

        public DateTime TimeStamp { get; set; }
        public PunchType Type { get; set; }

        public Punch(int accountId, int projectId, DateTime timeStamp, PunchType type)
        {
            AccountId = accountId;
            ProjectId = projectId;
            TimeStamp = timeStamp;
            Type = type;
        }
    }
}