using System.ComponentModel.DataAnnotations;

namespace ldl4.Models
{
    public class Data
    {
        [Required(ErrorMessage = "Enter the Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Enter the Password")]
        [StringLength(30, ErrorMessage ="Enter a valid password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Enter the Email")]
        public string Mail { get; set; }  
    }
}
