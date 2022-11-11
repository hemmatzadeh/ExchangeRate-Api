using DbUp;
using Microsoft.Extensions.Configuration;
using System.Reflection;

var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build()
                .GetSection(nameof(ConnectionStrings))
                .Get<ConnectionStrings>();

Console.WriteLine("configuration.ConnectionString");
Console.WriteLine(configuration.Default);

EnsureDatabase.For.SqlDatabase(configuration.Default);

var upgradeEngine = DeployChanges.To
     .SqlDatabase(configuration.Default)
     .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
     .LogToConsole()
     .Build();

var result = upgradeEngine.PerformUpgrade();
if (!result.Successful)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(result.Error);
    Console.ResetColor();
    return;
}
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine(value: "Success!");
Console.ResetColor();