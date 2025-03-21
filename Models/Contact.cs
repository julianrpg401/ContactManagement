using System.ComponentModel.DataAnnotations;

namespace ContactManagement.Models
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? LastName { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? Email { get; set; }
        [MaxLength(10)]
        public string Phone { get; set; } = string.Empty;
        public string? Address { get; set; }
        [MaxLength(50)]
        public string? City { get; set; }
    }
}
