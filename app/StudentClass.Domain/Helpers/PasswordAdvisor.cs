using StudentClass.Domain.Enums;
using System.Text;
using System.Text.RegularExpressions;

namespace StudentClass.Domain.Helpers
{
    public class PasswordAdvisor
    {
        public PasswordScore CheckStrength(string? password)
        {
            int score = 0;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            if (password.Length < 1)
                return PasswordScore.Blank;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (password.Length < 4)
                return PasswordScore.VeryWeak;

            if (password.Length >= 8)
                score++;
            if (password.Length >= 12)
                score++;
            if (Regex.Match(password, @"[0-9]", RegexOptions.ECMAScript).Success)
                score++;
            if (Regex.Match(password, @"[a-z]", RegexOptions.ECMAScript).Success &&
              Regex.Match(password, @"[A-Z]", RegexOptions.ECMAScript).Success)
                score++;
            if (Regex.Match(password, @".[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]", RegexOptions.ECMAScript).Success)
                score++;

            return (PasswordScore)score;
        }

        public string Hash(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            var hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
