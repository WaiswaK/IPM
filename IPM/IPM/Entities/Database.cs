using PDDT.Database;
using PDDT.DependencyInterface;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace PDDT.Entities
{
    public class Database
    {
        public static List<User> SelectAllUsers()
        {
            List<User> users = new List<User>();
            List<User> nullUser = null;
            int count = 0;
            using (var db = DependencyService.Get<IClientDatabase>().GetConnection())
            {
                var query = (db.Table<User>().ToList());
                users = query;
                count = query.Count;
            }
            if (count > 0)
                return users;
            else
                return nullUser;
        }
        public static User UserDetails(string user)
        {
            using (var db = DependencyService.Get<IClientDatabase>().GetConnection())
            {
                var query = (db.Table<User>().Where(c => c.User_name == user)).Single();
                return new User()
                {
                    Active = query.Active,
                    Code = query.Code,
                    User_name = query.User_name
                };
            }
        }
        public static string GetActiveUser()
        {
            List<User> users = SelectAllUsers();
            string active = string.Empty;
            if (users == null)
            {
                return active;
            }
            else
            {
                foreach (var user in users)
                {
                    if (user.Active == true)
                        active = user.User_name;
                    else
                    {
                        ;
                    }
                }
                return active;
            }          
        }
        public static void UpdateUser(bool active, string user)
        {
            User current = UserDetails(user);
            User final = new User()
            {
                Active = active,
                Code = current.Code,
                User_name = current.User_name
            };
            var db = DependencyService.Get<IClientDatabase>().GetConnection();
            db.Update(final);
        }
        public static void InsertUser(User user)
        {
            var db = DependencyService.Get<IClientDatabase>().GetConnection();
            try
            {
                db.Insert(new User()
                {
                    User_name = user.User_name,
                    Code = user.Code,
                    Active = true
                });
            }
            catch
            {
            }
        }
    }
}
