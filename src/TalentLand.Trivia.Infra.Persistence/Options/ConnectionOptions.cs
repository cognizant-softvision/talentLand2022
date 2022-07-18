using Microsoft.Data.SqlClient;
using System.Reflection;

namespace TalentLand.Trivia.Infra.Persistence.Options
{
    public class ConnectionOptions
    {
        public const string ConnectionParameters = nameof(ConnectionParameters);

        public string DataSource { get; set; } = null!;

        public string InitialCatalog { get; set; } = null!;

        public int Port { get; set; } = 1433;        

        public string UserID { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool UseInMemory { get; set; } = false;

        public string ConnectionString => new SqlConnectionStringBuilder
        {
            DataSource = $"{DataSource},{Port}",
            InitialCatalog = InitialCatalog,
            UserID = UserID,
            Password = Password,
            ApplicationName = Assembly.GetEntryAssembly()?.GetName()?.Name,
        }.ConnectionString;
    }
}
