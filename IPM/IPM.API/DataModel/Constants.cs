using IPM.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IPM.API.DataModel
{
    public class Constants
    {
        private static IPMEntities db = new IPMEntities();
        public static int NextNumber(string data)
        {
            char[] delimiter = { '-' };
            string[] datasplit = data.Split(delimiter);
            List<string> datalist = datasplit.ToList();
            return Int32.Parse(datalist.Last()) + 1;
        }
        public static string RemoveEmptyString(string word)
        {
            if (word == null)
                return "null";
            else return word;
        }
        public static List<string> Data(string data, char[] delimiter)
        {
            string[] datasplit = data.Split(delimiter);
            List<string> datalist = datasplit.ToList();
            return datalist;
        }
        public static string User_ID(string username)
        {
            string user = string.Empty;
            bool success = false;
            try
            {
                var email = db.AspNetUsers.FirstOrDefault(g => g.Email == username).Id;
                if (email != null)
                  user = email;

                success = true;
            }
            catch
            {
                success = false;   
            }
            if(success == false)
            {
                try
                {
                    var UserName = db.AspNetUsers.FirstOrDefault(h => h.UserName == username).Id;
                       if (UserName != null)
                            user = UserName;
                }
                catch
                {
                    
                }
            }
            return user;
        }
    }
}