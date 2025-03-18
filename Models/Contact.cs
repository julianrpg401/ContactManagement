

using System.ComponentModel.DataAnnotations;

namespace ContactManagement.Models
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Email { get; set; }
        [MaxLength(100)]
        public string Phone { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? City { get; set; }
    }
}
