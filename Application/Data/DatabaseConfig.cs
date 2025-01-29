using SQLite;
using System.IO;

namespace Vet_App_For_Freelancers.Data
{
    public class DatabaseConfig
    {
        private static SQLiteConnection _connection;

        public static SQLiteConnection GetConnection()
        {
            if (_connection == null)
            {
                var dbPath = Path.Combine(FileSystem.AppDataDirectory, "VetAppDb.db3");
                _connection = new SQLiteConnection(dbPath);
            }
            return _connection;
        }
    }
}

