using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashGiftDiary.Web.Services
{
    public class PasswordService
    {
        private readonly byte[] salt = {50,60,70 };
        private string _password;
        public PasswordService(string password)
        {
            _password = password;
        }
        public string Hash()
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: _password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
