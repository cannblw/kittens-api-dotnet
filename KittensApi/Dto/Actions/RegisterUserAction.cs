using System.ComponentModel.DataAnnotations;

namespace KittensApi.Dto.Actions
{
    public class RegisterUserAction
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
     
        [Required]
        public string UserName { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}
