using Application.Account.Dtos;
using Application.Account.Dtos.Response;
using Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.User.Queries
{
    public class AccountsQuery : IRequest<List<AccountDto>>
    {
    }

    public class AccountsQueryHandler : List, IRequestHandler<AccountsQuery, List<AccountDto>>
    {
        private IDataContext dataContext;

        public AccountsQueryHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<List<AccountDto>> Handle(AccountsQuery request, CancellationToken cancellationToken)
        {
            var accounts = new List<AccountDto>();

            var eAccounts = await dataContext.Accounts.ToListAsync(cancellationToken);

            //if (eAccounts == null)
            //{
            //    return new List(404, "No accounts available");
            //}

            foreach (var eAccount in eAccounts)
            {
                AccountDto account = new();

                account.Id = eAccount.Id;
                account.FirstName = eAccount.FirstName;
                account.LastName = eAccount.LastName;
                account.UserName = eAccount.Username;
                account.Email = eAccount.Email;
                account.Role = eAccount.role;

                accounts.Add(account);
            }

            return accounts;
        }
    }
}