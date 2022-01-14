namespace KittensApi.Dto.Details
{
    public class RegistrationDetails
    {
        public UserDetails User { get; set; }
        public string Token { get; set; }

        public RegistrationDetails(UserDetails user, string token)
        {
            User = user;
            Token = token;
        }
    }
}
