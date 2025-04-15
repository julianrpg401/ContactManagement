using ContactManagement.Models.Services;

namespace ContactManagement.Models.Utilities
{
    public class UserDataProcessor
    {
        public static User UserFormat(User user)
        {
            try
            {
                User userFormatted = new User()
                {
                    UserName = user.UserName.ToUpper(),
                    Email = user.Email.ToUpper(),
                    PasswordHash = EncryptService.GetSHA256(user.PasswordHash)
                };

                return userFormatted;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
