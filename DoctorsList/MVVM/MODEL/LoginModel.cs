using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using DataAnnotations = System.ComponentModel.DataAnnotations;


namespace DoctorsList.MVVM.MODEL
{
    [Table("Users")]
    public class LoginModel : ObservableValidator
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }

        [Column("User"), Indexed]
        public string User { get; set; }

        [Column("Password")]
        public string Password { get; set; }

        [Column("IsUserLoggedIn")]
        public bool isUserLoggedIn { get; set; } = false;
    }
}
