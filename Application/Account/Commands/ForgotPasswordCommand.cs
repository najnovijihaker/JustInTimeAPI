using Application.Account.Dtos.Request;
using Application.Common;
using Application.Project.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Application.Account.Commands
{
    public class ForgotPasswordCommand : ResetPasswordDto, IRequest<ResponseDto>
    {
    }

    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, ResponseDto>
    {
        private readonly IDataContext dataContext;
        private readonly IConfiguration configuration;

        public ForgotPasswordCommandHandler(IDataContext dataContext, IConfiguration configuration)
        {
            this.dataContext = dataContext;
            this.configuration = configuration;
        }

        public async Task<ResponseDto> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var account = await dataContext.Accounts.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
            if (account == null)
            {
                throw new Exception("Account not found");
            }

            var eHelper = new Emailer();

            account.PasswordHash = null;
            account.PasswordSalt = null;

            var newPassword = GenerateRandomPassowrd();

            CreatePassowrdHash(newPassword, out byte[] hash, out byte[] salt);

            account.PasswordHash = hash;
            account.PasswordSalt = salt;

            await dataContext.SaveChangesAsync(cancellationToken);

            //eHelper.SendNewPassowrd(newPassword, account);

            Console.WriteLine(newPassword);

            return new ResponseDto("Password Reset email sent");
        }

        // returns a randomly generated 8 digit password
        private string GenerateRandomPassowrd()
        {
            const int length = 8;
            var random = new RNGCryptoServiceProvider();
            var randomBytes = new byte[length];
            random.GetBytes(randomBytes);
            var characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+-=,.<>/?;:'\"[]{}\\|`~";
            var password = new string(randomBytes.Select(x => characters[x % characters.Length]).ToArray());
            return password;
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