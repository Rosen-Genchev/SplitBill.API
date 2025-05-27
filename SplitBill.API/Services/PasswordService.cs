using BCrypt.Net;

namespace SplitBill.API.Services
{
    public class PasswordService
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            Console.WriteLine($"[DEBUG] hashedPassword: {hashedPassword}");


            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
