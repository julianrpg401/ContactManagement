using ContactManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagement.DataAccess
{
    public interface IContactRepository
    {
        Task<IActionResult> AddContactAsync(Contact contact);
        Task<IActionResult> GetContactAsync(int id);
        Task<List<Contact>> GetContactsAsync();
        Task<IActionResult> DeleteContactAsync(int id);
        Task<IActionResult> UpdateContactAsync(int id, Contact contact);
        Task<IActionResult> DeleteContactsAsync();
        Task<Contact?> ValidateContactAsync(Contact contact);
    }
}
