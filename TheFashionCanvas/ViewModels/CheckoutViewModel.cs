using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TheFashionCanvas.Models;

namespace TheFashionCanvas.ViewModels
{
    public class CheckoutViewModel
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        [Required]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Required]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Display(Name = "Apartment, Suite, etc.")]
        public string Apartment { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Province")]
        public string Province { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Use as billing address")]
        public bool UseAsBillingAddress { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal Tax { get; set; }

        public decimal ShippingFee { get; set; }
    }
}
