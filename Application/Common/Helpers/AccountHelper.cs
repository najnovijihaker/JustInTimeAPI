using Application.Account.Commands;
using Application.Account.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Helpers
{
    internal class AccountHelper
    {
        private readonly IDataContext dataContext;

        public AccountHelper(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public bool ExistsByDto(AccountDto request)
        {
            if (dataContext.Accounts.Any(u => u.Id == request.Id || dataContext.Accounts.Any(u => u.Email == request.Email)))
            {
                return true;
            }
            return false;
        }

        public bool ExistsById(int id)
        {
            if (dataContext.Accounts.Any(u => u.Id == id))
            {
                return true;
            }
            return false;
        }
    }
}