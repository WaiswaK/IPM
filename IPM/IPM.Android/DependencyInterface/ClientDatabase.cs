using Xamarin.Forms;
using PDDT.Droid.DependencyInterface;
using SQLite.Net;
using System.IO;
using PDDT.Entities;
using PDDT.Database;
using PDDT.DependencyInterface;

[assembly: Dependency(typeof(ClientDatabase))]
namespace PDDT.Droid.DependencyInterface
{
    class ClientDatabase : IClientDatabase
    {
        public SQLiteConnection GetConnection()
        {
            string dbPath = Path.Combine(AppFolderPath(),"PDDT");
            var conn = new SQLiteConnection(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), dbPath, true, null, null, null, null);
            return conn;
        }
        public void InitializeDatabase()
        {
            if (LocalDatabaseNotPresent(Constants.dbName))
            {
                using (var db = GetConnection())
                {
                    db.CreateTable<Server>();
                    db.CreateTable<User>();
                };
            }
            else
            {
            }
        }
        bool LocalDatabaseNotPresent(string fileName)
        {
            if (!File.Exists(fileName))
                return true;
            else
                return false;
        }
        public static string AppFolderPath()
        {
            string externalStorageDirectory = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            if (!Directory.Exists(Path.Combine(externalStorageDirectory, "PDDT")))
                Directory.CreateDirectory(Path.Combine(externalStorageDirectory, "PDDT"));
            return Path.Combine(externalStorageDirectory, "PDDT");
        }
    }
}