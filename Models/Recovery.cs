using System.ComponentModel.DataAnnotations;

namespace ldl4.Models
{
    public class Recovery
    {
        
        [Required(ErrorMessage = "Enter the Email")]
        public string Email { get; set; }
    }
}
