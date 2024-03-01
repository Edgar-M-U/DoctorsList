using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using DoctorsList.MVVM.MODEL;
using DoctorsList.MVVM.VIEW;
using System.Windows.Input;

namespace DoctorsList.MVVM.VIEWMODEL
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand LoginCommand { get;}
        public ICommand onAppearingCommand { get; }

        private LoginModel _userCredentials;
        public LoginModel UserCredentials
        {
            get { return _userCredentials; }
            set { _userCredentials = value; OnPropertyChanged(); }
        }

        public LoginViewModel() 
        {
            LoginCommand = new Command(OnLoginClick);
            onAppearingCommand = new Command(onAppearingMethod);
            UserCredentials = new LoginModel();
        }

        private async void OnLoginClick()
        {
            if (String.IsNullOrEmpty(UserCredentials.User) && (String.IsNullOrEmpty(UserCredentials.Password)))
            {
                var toast = Toast.Make("Los datos de inicio de sesion son necesarios.", ToastDuration.Long, 20);
                await toast.Show();
                return;
            }
            if (String.IsNullOrEmpty(UserCredentials.User))
            {
                var toast = Toast.Make("Es necesario el Usuario.", ToastDuration.Long, 20);
                await toast.Show();
                return;
            }
            if (String.IsNullOrEmpty(UserCredentials.Password))
            {
                var toast = Toast.Make("Es necesaria la contraseña.", ToastDuration.Long, 20);
                await toast.Show();
                return;
            }
            

            if (await CredentialValidated())
            {

                await Shell.Current.GoToAsync("//DoctorsListView", true);
            }
            else
            {
                var toast = Toast.Make("Verifique usuario o contraseña.",ToastDuration.Long,20);
                await toast.Show();
            }
        }

        private async Task<bool> CredentialValidated()
        {
           return await Task.Run(() => {
               var result = App.loginRepository.GetUserCredentials(_userCredentials);
               if (result == null)
               {
                   return false;
               }
               result.isUserLoggedIn = true;
               App.loginRepository.LoginSucced(result);
               return true;
           });
        }

        private async void onAppearingMethod()
        {
            if (await App.loginRepository.CheckIfLoggedIn())
            {
                await Shell.Current.GoToAsync("//DoctorsListView", true);
            }
        }

    }
}
