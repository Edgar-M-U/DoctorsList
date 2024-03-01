using SQLite;
using DoctorsList.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorsList.MVVM.MODEL;

namespace DoctorsList.Repositories
{
    public class LoginRepository
    {
        SQLiteConnection connection;

        public LoginRepository()
        {
            connection = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
            connection.CreateTable<LoginModel>();
            CheckIfLoggedIn().Wait();
        }

        public void AddFirstUser()
        {
            try
            {
                if (connection.Table<LoginModel>().Where(x => x.User == "edgarm@hotmail.com").Count() == 0)
                {
                    LoginModel newUser = new() { User = "edgarm@hotmail.com", Password = "Admin123", isUserLoggedIn = false };
                    var result = connection.Insert(newUser);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public LoginModel GetUserCredentials(LoginModel loginCredential)
        {
            return connection.Table<LoginModel>().FirstOrDefault(x => x.User == loginCredential.User && x.Password == loginCredential.Password);
        }

        public void LoginSucced(LoginModel loginCredential)
        {
            connection.Update(loginCredential);
        }

        public async Task<bool> CheckIfLoggedIn()
        {
            var user = connection.Table<LoginModel>().Where(x => x.isUserLoggedIn == true).FirstOrDefault();

            if (user != null)
            {
                return true;
            }
            else
            {
                AddFirstUser();
                return false;
            }
        }
    }
}

