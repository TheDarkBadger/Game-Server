using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Security
{
    public static class Passwords
    {
        /// <summary>
        /// Size of salt.
        /// </summary>
        private const int SaltSize = 16;

        /// <summary>
        /// Size of hash.
        /// </summary>
        private const int HashSize = 20;


        private const int iterations = 100000;
        /// <summary>
        /// Creates a hash from a password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="iterations">Number of iterations.</param>
        /// <returns>The hash.</returns>
        public static string Hash(string password, out string salt)
        {
            // Create salt
            byte[] bsalt;
            new RNGCryptoServiceProvider().GetBytes(bsalt = new byte[SaltSize]);

            // Create hash
            var pbkdf2 = new Rfc2898DeriveBytes(password, bsalt, iterations);
            var hash = pbkdf2.GetBytes(HashSize);

            // Combine salt and hash
            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(bsalt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            // Convert to base64
            var base64Hash = Convert.ToBase64String(hashBytes);
            salt = Encoding.Default.GetString(bsalt);
            // Format hash with extra information
            return string.Format("$TDBHASH$V1${0}${1}", iterations, base64Hash);
        }

        /// <summary>
        /// Checks if hash is supported.
        /// </summary>
        /// <param name="hashString">The hash.</param>
        /// <returns>Is supported?</returns>
        public static bool IsHashSupported(string hashString)
        {
            return hashString.Contains("$TDBHASH$V1$");
        }

        /// <summary>
        /// Verifies a password against a hash.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="hashedPassword">The hash.</param>
        /// <returns>Could be verified?</returns>
        public static bool Verify(string password, string hashedPassword)
        {
            // Check hash
            if (!IsHashSupported(hashedPassword))
            {
                throw new NotSupportedException("The hashtype is not supported");
            }

            // Extract iteration and Base64 string
            var splittedHashString = hashedPassword.Replace("$TDBHASH$V1$", "").Split('$');
            var iterations = int.Parse(splittedHashString[0]);
            var base64Hash = splittedHashString[1];

            // Get hash bytes
            var hashBytes = Convert.FromBase64String(base64Hash);

            // Get salt
            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Create hash with given salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            // Get result
            for (var i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }

        private static Regex symbolsRegex = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

        public static bool IsPasswordValid(string toCheck)
        {
            bool validLength = false;
            bool validLowerCase = false;
            bool validUpperCase = false;
            bool validSpecialCharacter = false;
            bool validDecimal = false;
            bool validWhitespace = true;

            if (toCheck.Length >= 8 && toCheck.Length <= 32)
                validLength = true;
            char[] array = toCheck.ToCharArray();
            foreach (char c in array)
            {
                if (char.IsLower(c))
                    validLowerCase = true;
                if (char.IsUpper(c))
                    validUpperCase = true;
                if (char.IsDigit(c))
                    validDecimal = true;
                if (char.IsWhiteSpace(c))
                    validDecimal = false;

            }
            validDecimal = symbolsRegex.IsMatch(toCheck.ToString());
            return validLength && validLowerCase && validUpperCase && validSpecialCharacter && validDecimal && validWhitespace;
        }
    }
}
