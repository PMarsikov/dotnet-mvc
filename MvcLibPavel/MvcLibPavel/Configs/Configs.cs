

namespace MvcLibPavel.Configs
{
    internal class Configs
    {
        public Settings GetConfig()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            return config.GetRequiredSection("Settings").Get<Settings>();
        }
    }
}
