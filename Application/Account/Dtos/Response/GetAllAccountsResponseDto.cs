using Application.Account.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Account.Dtos.Response
{
    public class List
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<AccountDto> Users { get; set; } = new List<AccountDto>();

        public List()
        {
        }

        public List(int StautsCode, string Message, List<AccountDto> Users)
        {
            StatusCode = StatusCode;
            this.Message = Message;
            this.Users = Users;
        }

        public List(int StatusCode, string Message)
        {
            this.StatusCode = StatusCode;
            this.Message = Message;
        }
    }
}