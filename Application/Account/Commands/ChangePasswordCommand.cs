using Application.Account.Dtos.Request;
using Application.Common;
using Application.Common.Helpers;
using Application.Project.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Application.Account.Commands
{
    public class ChangePasswordCommand : ResetPasswordDto, IRequest<ResponseDto>
    {
    }

    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ResponseDto>
    {
        private readonly IDataContext dataContext;

        public ChangePasswordCommandHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<ResponseDto> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var account = await dataContext.Accounts.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (account == null)
            {
                throw new Exception("Account does not exist");
            }

            CreatePassowrdHash(request.NewPassword, out byte[] hash, out byte[] salt);

            account.PasswordHash = hash;
            account.PasswordSalt = salt;

            await dataContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto("Password Changed");
        }

        private void CreatePassowrdHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}