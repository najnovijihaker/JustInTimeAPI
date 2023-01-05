using Application.Account.Dtos.Requests;
using Application.Common;
using Application.Common.Dtos;
using MediatR;
using System.Security.Cryptography;
using System.Text;
using EAccount = Domain.Entities.Account;

namespace Application.Account.Commands
{
    public class CreateAccountCommand : CreateAccountRequestDto, IRequest<AuthResponseDto>
    {
    }

    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AuthResponseDto>
    {
        private readonly IDataContext dataContext;

        public CreateAccountCommandHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<AuthResponseDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var emailer = new Emailer();
            if (Exists(request))
            {
                return new AuthResponseDto(409, "Account already registered!");
            }

            if (IsDeactivated(request))
            {
                return new AuthResponseDto(409, "Account is deactivated!");
            }

            EAccount eAccount = new EAccount();

            eAccount.FirstName = request.FirstName;
            eAccount.LastName = request.LastName;
            eAccount.Username = request.UserName;
            eAccount.Email = request.Email;

            CreatePassowrdHash(request.Password, out byte[] hash, out byte[] salt);
            eAccount.PasswordHash = hash;
            eAccount.PasswordSalt = salt;

            if (request.StudentAccount == true)
            {
                eAccount.role = "Student";
            }
            else
            {
                eAccount.role = "Developer";
            }

            eAccount.IsActive = false;

            await dataContext.AddEntityToGraph(eAccount);
            await dataContext.SaveChangesAsync();
            emailer.SendVerification("tokenplaceholder", eAccount);
            return new AuthResponseDto(200, "Successful");
        }

        private void CreatePassowrdHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public bool Exists(CreateAccountCommand request)
        {
            if (dataContext.Accounts.Any(u => u.Username == request.UserName || dataContext.Accounts.Any(u => u.Email == request.Email)))
            {
                return true;
            }
            return false;
        }

        public bool IsDeactivated(CreateAccountCommand request)
        {
            var userForCheck = dataContext.Accounts.FirstOrDefault(u => u.Username == request.UserName || u.Email == request.Email);
            if (userForCheck == null)
            {
                return false;
            }
            else if (!userForCheck.IsActive)
            {
                return true;
            }
            return false;
        }
    }
}