using System.Security.Cryptography;
using System.Text;

namespace WebApi.Helpers;

public class PasswordHelper
{
     private const int KeySize = 32;
     private const int IterationsCount = 1000;

     // ReSharper disable once MemberCanBePrivate.Global
     /// <summary>
     /// Makes hash string of given password
     /// </summary>
     /// <param name="password"></param>
     /// <returns></returns>
     public static string Encrypt(string password)
     {
          if (EnvironmentHelper.IsDevelopment)
               return password;

          using var algorithm = new Rfc2898DeriveBytes(
               password: password,
               salt: Encoding.UTF8.GetBytes(password),
               iterations: IterationsCount,
               hashAlgorithm: HashAlgorithmName.SHA256);
          return Convert.ToBase64String(algorithm.GetBytes(KeySize));
     }


     public static bool Verify(string passwordHash, string password)
     {
          if (EnvironmentHelper.IsDevelopment)
               return passwordHash == password;

          return Encrypt(password).SequenceEqual(passwordHash);
     }

     public static int GenerateRandom6DigitNumber()
     {
          return Random.Shared.Next(100_000, 999_999);
     }

     public static string GenerateRandom6LengthPassword()
     {
          string ch = "abcdefghijklmnopqrstuvwxyz0123456789";
          StringBuilder pwd = new StringBuilder();
          for (int i = 0; i < 6; i++)
          {
               pwd.Append(ch[Random.Shared.Next(0, 35)]);
          }

          return pwd.ToString();
          // return Random.Shared.Next(100_000, 999_999);
     }
}