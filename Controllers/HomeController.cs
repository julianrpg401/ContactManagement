using System.Diagnostics;
using ContactManagement.DataAccess;
using ContactManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContactRepository _contactRepository;

        public HomeController(ILogger<HomeController> logger, IContactRepository contactRepository)
        {
            _logger = logger;
            _contactRepository = contactRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<Contact>> GetContacts()
        {
            return await _contactRepository.GetContactsAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Contact contact)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Message = "Error al enviar, verifique los campos.";
                    return View(contact);
                }

                var existingContact = await _contactRepository.ValidateContactAsync(contact);

                if (existingContact != null)
                {
                    ViewBag.Message = "Error al enviar. El número de celular o email ya existe.";
                    return View(contact);
                }

                await _contactRepository.AddContactAsync(contact);
                ViewBag.Message = "El contacto se ha agregado con éxito.";

                return View();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { Message = ex.Message });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
