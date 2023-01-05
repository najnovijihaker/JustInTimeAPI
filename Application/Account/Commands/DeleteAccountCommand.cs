using Application.Account.Dtos;
using Application.Common;
using Application.Project.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Account.Commands
{
    public class DeleteAccountCommand : AccountDto, IRequest<ResponseDto>
    {
    }

    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, ResponseDto>
    {
        private readonly IDataContext dataContext;

        public DeleteAccountCommandHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<ResponseDto> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await dataContext.Accounts.FirstOrDefaultAsync(a => a.Username == request.UserName);
            if (account == null)
            {
                return new ResponseDto("Account not found");
            }

            dataContext.Accounts.Remove(account);
            await dataContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto("Account records erased");
        }
    }
}