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
            return View(new Contact());
        }

        [HttpPost]
        public async Task<IActionResult> Index(Contact contact)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Message = "Error al enviar, verifique los campos.";
                    ViewBag.MessageType = "error";

                    return View(contact);
                }

                var existingContact = await _contactRepository.ValidateContactAsync(contact);

                if (existingContact != null)
                {
                    ViewBag.Message = "Error al enviar. El número de celular o email ya existe.";
                    ViewBag.MessageType = "error";

                    return View(contact);
                }

                await _contactRepository.AddContactAsync(contact);

                ViewBag.Message = "El contacto se ha agregado con éxito.";
                ViewBag.MessageType = "success";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Contacts()
        {
            try
            {
                var contacts = await _contactRepository.GetContactsAsync();

                return View(contacts);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteContact(int Contactid)
        {
            try
            {
                await _contactRepository.DeleteContactAsync(Contactid);
                TempData["Message"] = "Contacto borrado.";

                return RedirectToAction("Contacts");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAllContacts()
        {
            try
            {
                await _contactRepository.DeleteContactsAsync();
                TempData["Message"] = "Todos los contactos han sido eliminados.";

                return RedirectToAction("Contacts");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditContact(int id)
        {
            try
            {
                var contact = await _contactRepository.GetContactAsync(id);

                if (contact == null)
                {
                    return NotFound();
                }

                return View(contact);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditContact(int id, Contact contact)
        {
            try
            {
                await _contactRepository.UpdateContactAsync(id, contact);

                return RedirectToAction("Contacts");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
