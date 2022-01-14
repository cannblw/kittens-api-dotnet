namespace KittensApi.Dto.Details
{
    public class LoginDetails
    {
        public UserDetails User { get; set; }
        public string Token { get; set; }

        public LoginDetails(UserDetails user, string token)
        {
            User = user;
            Token = token;
        }
    }
}
