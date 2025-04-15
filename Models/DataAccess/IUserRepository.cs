using ContactManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagement.DataAccess
{
    public interface IUserRepository
    {
        Task<IActionResult> AddUserAsync(User user);
        Task<IActionResult> UpdateUserAsync(int id, User user);
        Task<IActionResult> DeleteUserAsync(int id);
        Task<User?> ValidateUserAsync(User user);
    }
}
