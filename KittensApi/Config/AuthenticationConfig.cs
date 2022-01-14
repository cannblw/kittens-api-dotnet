namespace KittensApi.Config
{
    public class AuthenticationConfig
    {
        public string JwtSecret { get; set; }
        public int JwtHoursUntilExpiration { get; set; }
    }
}
