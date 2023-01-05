using Application.Account.Dtos;
using Application.Common;
using Application.Project.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Account.Commands
{
    public class UnlockAccountCommand : AccountDto, IRequest<ResponseDto>
    {
    }

    public class UnlockAccountCommandHandler : IRequestHandler<UnlockAccountCommand, ResponseDto>
    {
        private readonly IDataContext dataContext;

        public UnlockAccountCommandHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<ResponseDto> Handle(UnlockAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await dataContext.Accounts.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (account == null)
            {
                return new ResponseDto("Account not found");
            }

            account.IsLocked = false;
            await dataContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto("Account Unlocked");
        }
    }
}