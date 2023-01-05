using Application.Account.Dtos.Request;
using Application.Common;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Application.Account.Commands
{
    public class ResetPasswordCommand : ResetPasswordDto, IRequest<string>
    {
    }

    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, string>
    {
        private readonly IDataContext dataContext;
        private readonly IConfiguration configuration;

        public ResetPasswordCommandHandler(IDataContext dataContext, IConfiguration configuration)
        {
            this.dataContext = dataContext;
            this.configuration = configuration;
        }

        public async Task<string?> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var account = dataContext.Accounts.FirstOrDefault(a => a.Email == request.Email);
            if (account == null) return null;

            account.PasswordSalt = null;
            account.PasswordHash = null;

            CreatePassowrdHash(request.ConfirmNewPassword, out byte[] hash, out byte[] salt);
            account.PasswordSalt = salt;
            account.PasswordHash = hash;

            return "Sucessful";
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