using SQLite.Net.Attributes;

namespace PDDT.Database
{
    [Table("User")]
    public class User
    {
        [PrimaryKey]
        public string User_name { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
    }
}
