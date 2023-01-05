using Application.Account.Dtos;
using Application.Common;
using Application.Project.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Account.Commands
{
    public class ManuallyActivateCommand : AccountDto, IRequest<ResponseDto>
    {
    }

    public class ManuallyVerifyCommandHandler : IRequestHandler<ManuallyActivateCommand, ResponseDto>
    {
        private readonly IDataContext dataContext;

        public ManuallyVerifyCommandHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<ResponseDto> Handle(ManuallyActivateCommand request, CancellationToken cancellationToken)
        {
            var account = await dataContext.Accounts.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
            if (account == null)
            {
                return new ResponseDto("No account found!");
            }

            account.IsActive = true;
            await dataContext.SaveChangesAsync(cancellationToken);
            return new ResponseDto("Account Manually Activated");
        }
    }
}