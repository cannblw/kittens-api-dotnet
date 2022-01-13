namespace KittensApi.Config
{
    public class AppSettings
    {
        public CatsApiConfig CatsApi { get; set; }
        public AuthenticationConfig Authentication { get; set; }
        public DatabaseConfig Database { get; set; }
    }
}