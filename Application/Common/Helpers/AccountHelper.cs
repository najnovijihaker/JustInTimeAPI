using Application.Account.Commands;
using Application.Account.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Helpers
{
    internal class AccountHelper
    {
        private readonly IDataContext dataContext;

        public AccountHelper(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public AccountHelper()
        {
        }

        public bool ExistsByDto(AccountDto request)
        {
            if (dataContext.Accounts.Any(u => u.Id == request.Id || dataContext.Accounts.Any(u => u.Email == request.Email)))
            {
                return true;
            }
            return false;
        }

        public bool ExistsById(int id)
        {
            if (dataContext.Accounts.Any(u => u.Id == id))
            {
                return true;
            }
            return false;
        }

        public bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
        {
            using (var hmac = new HMACSHA512(salt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(hash);
            }
        }

        // Must use this instance so the HMACSHA512 key stays the same
        public void CreatePassowrdHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}