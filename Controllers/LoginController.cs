using ContactManagement.DataAccess;
using ContactManagement.Models;
using ContactManagement.Models.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagement.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;

        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult CreateAccount()
        {
            return View(new User());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Message = "Error al enviar, verifique los campos.";
                    ViewBag.MessageType = "error";

                    return View(user);
                }

                var existingUser = await _userRepository.ValidateUserAsync(user);

                if (existingUser != null)
                {
                    ViewBag.Message = "Error al enviar. Ya existe un usuario con ese email.";
                    ViewBag.MessageType = "error";

                    return View(user);
                }

                user = UserDataProcessor.UserFormat(user);
                await _userRepository.AddUserAsync(user);

                ViewBag.Message = "Cuenta creada con éxito.";
                ViewBag.MessageType = "success";

                return RedirectToAction("CreateAccount");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        public async Task<IActionResult> Login()
        {
            try
            {

            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
