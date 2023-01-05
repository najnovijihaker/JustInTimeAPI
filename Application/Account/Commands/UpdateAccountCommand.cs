using Application.Account.Dtos;
using Application.Common;
using Application.Project.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Account.Commands
{
    public class UpdateAccountCommand : AccountDto, IRequest<string>
    {
    }

    public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, string>
    {
        private readonly IDataContext dataContext;

        public UpdateAccountCommandHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<string> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await dataContext.Accounts.FirstOrDefaultAsync(a => a.Id == request.Id);
            if (account == null)
            {
                return "Account not found";
            }

            if (request.FirstName != account.FirstName)
            {
                account.FirstName = request.FirstName;
            }
            if (request.LastName != account.LastName)
            {
                account.LastName = request.LastName;
            }
            if (request.UserName != account.Username)
            {
                account.Username = request.UserName;
            }
            if (request.Email != account.Email)
            {
                account.Email = request.Email;
            }
            if (request.Role != account.role)
            {
                account.role = request.Role;
            }

            await dataContext.SaveChangesAsync(cancellationToken);

            return "Successful";
        }
    }
}