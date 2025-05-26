using Microsoft.AspNetCore.Identity;

public class PasswordService
{
    private readonly PasswordHasher<string> _hasher = new();

    public string HashPassword(string userId, string password) =>
        _hasher.HashPassword(userId, password);

    public bool VerifyPassword(string userId, string hashedPassword, string inputPassword)
    {
        var result = _hasher.VerifyHashedPassword(userId, hashedPassword, inputPassword);
        return result == PasswordVerificationResult.Success;
    }
}
