using Application.Account.Dtos.Requests;
using Application.Common;
using Application.Common.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

//using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using EAccount = Domain.Entities.Account;

namespace Application.Account.Commands
{
    public class AuthenticateCommand : AuthRequestDto, IRequest<AuthResponseDto>
    {
    }

    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, AuthResponseDto>
    {
        private readonly IDataContext dataContext;
        private readonly IConfiguration configuration;

        public AuthenticateCommandHandler(IDataContext dataContext, IConfiguration configuration)
        {
            this.dataContext = dataContext;
            this.configuration = configuration;
        }

        public async Task<AuthResponseDto> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            var Account = await (dataContext.Accounts.AsNoTracking()
                            .Where(u => u.Username == request.UserName)
                            .FirstOrDefaultAsync(cancellationToken));

            if (Account == null)
            {
                return new AuthResponseDto(404, "User not found");
            }

            if (!VerifyPasswordHash(request.Password, Account.PasswordHash, Account.PasswordSalt))
            {
                return new AuthResponseDto(401, "Unauthorized");
            }
            else if (!Account.IsActive)
            {
                return new AuthResponseDto(401, "Account deactivated");
            }

            return new AuthResponseDto(200, "Authorized", CreateJWT(Account));
        }

        private bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
        {
            using (var hmac = new HMACSHA512(salt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(hash);
            }
        }

        private string CreateJWT(EAccount account)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, account.role),
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.Email, account.Email)
            });

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = credentials
            };

            var token = jwtHandler.CreateToken(tokenDescriptor);

            return jwtHandler.WriteToken(token);
        }
    }
}