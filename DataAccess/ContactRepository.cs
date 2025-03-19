using ContactManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagement.DataAccess
{
    public class ContactRepository : IContactRepository
    {
        public Task<IActionResult> AddContactAsync(Contact contact)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> DeleteContactAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> DeleteContactsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetContactAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetContactsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> UpdateContactAsync(int id, Contact contact)
        {
            throw new NotImplementedException();
        }
    }
}
