using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace TheFashionCanvas.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid category.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Stock is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be a non-negative number.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [DataType(DataType.Currency, ErrorMessage = "Invalid price format.")]
        public decimal Price { get; set; }

      
        [Display(Name = "Product Image")]
        public IFormFile? Image { get; set; }

        
        public string? ImagePath { get; set; }
    }
}
