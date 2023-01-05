using Application.Account.Dtos;
using Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Account.Commands
{
    public class DeleteAccountCommand : AccountDto, IRequest<string>
    {
    }

    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, string>
    {
        private readonly IDataContext dataContext;

        public DeleteAccountCommandHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<string> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await dataContext.Accounts.FirstOrDefaultAsync(a => a.Username == request.UserName);
            if (account == null) return "Account not found";

            dataContext.Accounts.Remove(account);
            await dataContext.SaveChangesAsync(cancellationToken);

            return "Account records erased";
        }
    }
}