using System.ComponentModel.DataAnnotations;

namespace KittensApi.Dto.Actions
{
    public class LoginAction
    {
        [Required]
        public string UserName { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}
