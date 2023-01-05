using Application.Account.Dtos;
using Application.Common;
using Application.Project.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Account.Commands
{
    public class LockAccountCommand : AccountDto, IRequest<ResponseDto>
    {
    }

    public class LockAccountCommandHandler : IRequestHandler<LockAccountCommand, ResponseDto>
    {
        private readonly IDataContext dataContext;

        public LockAccountCommandHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<ResponseDto> Handle(LockAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await dataContext.Accounts.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (account == null)
            {
                return new ResponseDto("Account not found");
            }

            account.IsLocked = true;
            await dataContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto("Account Locked!");
        }
    }
}