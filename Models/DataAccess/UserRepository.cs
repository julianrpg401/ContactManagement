using ContactManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactManagement.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AddUserAsync(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { Message = ex.Message });
            }
        }

        public Task<IActionResult> DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> DeleteUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> UpdateUserAsync(int id, User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> ValidateUserAsync(User user)
        {
            try
            {
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == user.Email);

                return existingUser;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
