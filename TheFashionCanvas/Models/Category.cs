using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheFashionCanvas.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
