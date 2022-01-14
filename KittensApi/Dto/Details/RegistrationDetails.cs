using System.Collections.Generic;

namespace KittensApi.Dto.Details
{
    public class RegistrationDetails
    {
        public string Token { get; set; }
        
        public bool Success { get; set; }
        
        public List<string> Errors { get; set; }
    }
}
