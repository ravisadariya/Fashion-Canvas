using System.ComponentModel.DataAnnotations;

namespace TheFashionCanvas.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(200)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
