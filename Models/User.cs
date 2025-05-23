﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ContactManagement.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Usuario")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        [DisplayName("Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DisplayName("Contraseña")]
        public string PasswordHash { get; set; } = string.Empty;

        public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();
    }
}
