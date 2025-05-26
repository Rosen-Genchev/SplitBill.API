using SplitBill.API.Dtos;
using SplitBill.API.Entities;

namespace SplitBill.API.Services
{
    public interface IUserService
    {
        Task<User?> AuthenticateAsync(string username, string password);
        Task<User> RegisterAsync(UserRegisterDto dto);
    }
}
