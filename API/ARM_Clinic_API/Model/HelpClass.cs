namespace ARM_Clinic_API.Model
{
    public class ConnectionStrings
    {
        public static string ConnectionStringss()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

            var root = builder.Build();
            var connect = root.GetSection("ConnectionStrings")["DefaultConnection"].ToString();
            return connect;
        }
        public static string ConfigJWT(string key)
        {
            string strResultPath = string.Empty;

            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            var root = builder.Build();
            strResultPath = root.GetSection("AppConfiguration").GetSection("Jwt")[key].ToString();

            return strResultPath;
        }

        public static MailConfiguration EMailConfiguration()
        {
            MailConfiguration mailConfiguration = new MailConfiguration();

            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

            var root = builder.Build();
            mailConfiguration.HostMailName = root.GetSection("AppConfiguration").GetSection("Email")["HostMailName"].ToString();
            mailConfiguration.HostEMailUserName = root.GetSection("AppConfiguration").GetSection("Email")["HostEMailUserName"].ToString();
            mailConfiguration.HostEMailPassword = root.GetSection("AppConfiguration").GetSection("Email")["HostEMailPassword"].ToString();
            mailConfiguration.SiteWeb = root.GetSection("AppConfiguration")["SiteWeb"].ToString();
            mailConfiguration.TempMailDirectory = root.GetSection("AppConfiguration").GetSection("Email")["TempMailDirectory"].ToString();

            var Port = root.GetSection("AppConfiguration").GetSection("Email")["Port"].ToString();

            var EnableSsl = root.GetSection("AppConfiguration").GetSection("Email")["EnableSsl"].ToString();
            var IsPasswordMail = root.GetSection("AppConfiguration").GetSection("Email")["IsPasswordMail"].ToString();
            var IsTestMail = root.GetSection("AppConfiguration").GetSection("Email")["IsTestMail"].ToString();
            var IsSendMail = root.GetSection("AppConfiguration").GetSection("Email")["IsSendMail"].ToString();

            var IsSubjectTestMail = root.GetSection("AppConfiguration").GetSection("Email")["IsSubjectTestMail"].ToString();

            mailConfiguration.EnableSsl = bool.Parse(EnableSsl);
            mailConfiguration.Port = int.Parse(Port);
            mailConfiguration.IsPasswordMail = bool.Parse(IsPasswordMail);
            mailConfiguration.IsTestMail = bool.Parse(IsTestMail);
            mailConfiguration.IsSendMail = bool.Parse(IsSendMail);
            mailConfiguration.IsSubjectTestMail = bool.Parse(IsSubjectTestMail);
            return mailConfiguration;

        }
    }
}
