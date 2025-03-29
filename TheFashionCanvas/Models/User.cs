using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheFashionCanvas.Models
{
    public class User
    {
        [Key]
        public string UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [MaxLength(20)]
        public string Role { get; set; }

        [MaxLength(200)]
        public string StreetAddress { get; set; }

        [MaxLength(100)]
        public string Apartment { get; set; } 

        [MaxLength(10)]
        public string PostalCode { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(50)]
        public string Province { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        public ICollection<Cart> Carts { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
