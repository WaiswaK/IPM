using PDDT.Database;
using PDDT.DependencyInterface;
using Xamarin.Forms;

namespace PDDT.Entities
{
    public class Constants
    {
        public static string dbName = "PDDT.sqlite";
        private static string http = @"http://";
        public static string baseUrl = http  + HostIP();
        public static int port = HostPort();
        public static string hostUrl = baseUrl + ":" + port;
        public static string hostAPI = hostUrl + @"/api";
        public static string Json_link_request = hostAPI + @"/request";
        public static string Json_link_response = hostAPI + @"/response";
        public static string Json_link_pestlist = hostAPI + @"/pestlist";
        public static string Json_link_diseaselist = hostAPI + @"/diseaselist";
        public static string Json_link_chemicallist = hostAPI + @"/chemicallist";
        public static string Json_link_register =hostAPI + @"/account/register";
        public static string Json_link_activity = hostAPI + @"/activities";
        public static string NullRemove(string input)
        {
            if(input == null)
            {
                return string.Empty;
            }
            else
            {
                return input;
            }
        }
        //Method to Initialize the SQLite Database
        public static void InitializeDatabase()
        {
            DependencyService.Get<IClientDatabase>().InitializeDatabase();
        } 
        public static string HostIP()
        {
            string host = string.Empty;
            try
            {
                using (var db = DependencyService.Get<IClientDatabase>().GetConnection())
                {
                    var query = db.Table<Server>().FirstOrDefault();
                    host = query.Host;
                }
            }
            catch
            {

            }        
            return host;
        }
        public static int HostPort()
        {
            int port = 0;
            try
            {
                using (var db = DependencyService.Get<IClientDatabase>().GetConnection())
                {
                    var query = db.Table<Server>().FirstOrDefault();
                    port = query.Port;
                }
            }
            catch
            {

            }          
            return port;
        }
    }
}
