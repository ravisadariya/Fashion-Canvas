using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TheFashionCanvas.Models;

public class Cart
{
    [Key]
    public int CartId { get; set; }

    [Required]
    [ForeignKey("User")]
    public string UserId { get; set; }

    public User User { get; set; }

    public ICollection<CartItem> CartItems { get; set; }
}
