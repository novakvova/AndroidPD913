namespace NewMail.Web.Helpers
{
    public static class AppLoggerFile
    {
        public static void UserLoggerFile(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                // other code remove for clarity 
                loggerFactory.AddFile("Logs/mylog-{Date}.txt");

            }
        }
    }
}
