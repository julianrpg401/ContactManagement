﻿using ContactManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactManagement.DataAccess
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _context;

        public ContactRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Contact?> ValidateContactAsync(Contact contact)
        {
            try
            {
                var existingContact = await _context.Contacts.FirstOrDefaultAsync
                    (c => c.Phone == contact.Phone | c.Email == contact.Email);

                return existingContact;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IActionResult> AddContactAsync(Contact contact)
        {
            try
            {
                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();

                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { Message = ex.Message });
            }
        }

        public async Task<IActionResult> DeleteContactAsync(int id)
        {
            try
            {
                var contact = await _context.Contacts.FindAsync(id);

                if (contact == null)
                {
                    return new NotFoundResult();
                }

                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();

                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { Message = ex.Message });
            }
        }

        public async Task<IActionResult> DeleteContactsAsync()
        {
            try
            {
                var contacts = _context.Contacts.ToList();

                _context.Contacts.RemoveRange(contacts);
                await _context.SaveChangesAsync();

                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { Message = ex.Message });
            }
        }

        public async Task<Contact?> GetContactAsync(int id)
        {
            try
            {
                var contact = await _context.Contacts.FindAsync(id);

                if (contact == null)
                {
                    return null!;
                }

                return contact;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Contact>> GetContactsAsync()
        {
            try
            {
                var contacts = await _context.Contacts.ToListAsync();

                return contacts;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IActionResult> UpdateContactAsync(int id, Contact contact)
        {
            try
            {
                var existingContact = await _context.Contacts.FindAsync(id);

                if (existingContact == null)
                {
                    return new NotFoundResult();
                }

                existingContact.FirstName = contact.FirstName;
                existingContact.LastName = contact.LastName;
                existingContact.Email = contact.Email;
                existingContact.Phone = contact.Phone;
                existingContact.Address = contact.Address;
                existingContact.City = contact.City;

                _context.Contacts.Update(existingContact);
                await _context.SaveChangesAsync();

                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { Message = ex.Message });
            }
        }
    }
}
