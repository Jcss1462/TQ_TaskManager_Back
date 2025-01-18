using System.Security.Cryptography;
using System.Text;

namespace TQ_TaskManager_Back.Services;

public class SecurityService : ISecurityService
{
    public string HashPassword(string password)
    {
        // Utiliza un algoritmo de hashing seguro, por ejemplo, PBKDF2 o BCrypt
        using (var sha256 = SHA256.Create())
        {
            var passwordWithSalt = password;
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(passwordWithSalt));
            return Convert.ToBase64String(hashBytes);
        }
    }

}

public interface ISecurityService
{
    string HashPassword(string password);
}