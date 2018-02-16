using SQLite.Net;

namespace PDDT.DependencyInterface
{
    public interface IClientDatabase
    {
        void InitializeDatabase();
        SQLiteConnection GetConnection();
    }
}
