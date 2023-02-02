using Application.Account.Dtos;
using Application.Common;
using Application.Project.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Account.Commands
{
    public class UpdateAccountCommand : AccountDto, IRequest<ResponseDto>
    {
    }

    public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, ResponseDto>
    {
        private readonly IDataContext dataContext;

        public UpdateAccountCommandHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<ResponseDto> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await dataContext.Accounts.FirstOrDefaultAsync(a => a.Id == request.Id);
            if (account == null)
            {
                throw new Exception("Account not found");
                //return new ResponseDto("Accuont NA");
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

            await dataContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto("Successful");
        }
    }
}