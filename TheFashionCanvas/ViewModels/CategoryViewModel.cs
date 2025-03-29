using System.ComponentModel.DataAnnotations;

namespace TheFashionCanvas.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }
    }
}
