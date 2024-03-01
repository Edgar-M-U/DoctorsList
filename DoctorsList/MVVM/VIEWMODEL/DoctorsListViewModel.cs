using CommunityToolkit.Maui.Core.Extensions;
using DoctorsList.MVVM.MODEL;
using System.Collections.ObjectModel;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Windows.Input;


namespace DoctorsList.MVVM.VIEWMODEL
{
    [QueryProperty("FromLogin", "param")]
    public class DoctorsListViewModel : BaseViewModel
    {
        public ICommand GenerateUsersCommand { get; }
        public ICommand GoToDetailCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand CerrarSesionCommand { get; }

        private ObservableCollection<Root> _RootList;
        public ObservableCollection<Root> RootList
        {
            get { return _RootList; }
            set { _RootList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Result> _ResultList;
        public ObservableCollection<Result> ResultList
        {
            get { return _ResultList; }
            set { _ResultList = value; OnPropertyChanged(); }
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { _isRefreshing = value; OnPropertyChanged(); }
        }

        private string _fromLogin;
        public string FromLogin
        {
            get { return _fromLogin; }
            set { _fromLogin = Uri.UnescapeDataString(value ?? "False");
                OnPropertyChanged();        
            }
        }


        private HttpClient client;

        public DoctorsListViewModel()
        {
            GenerateUsersCommand = new Command(GetRandomPeople);
            GoToDetailCommand = new Command(GotoDetailMethod);
            RefreshCommand = new Command(RefreshMethod);
            CerrarSesionCommand = new Command(CerrarSesion);
            client = new HttpClient();

            RootList = new ObservableCollection<Root>();
            ResultList = new ObservableCollection<Result>();
        }


        private async void GetRandomPeople()
        {
            ResultList.Clear();
            RootList.Clear();

            RootList = App.RootRepo.GetItemsWithChildren().ToObservableCollection();

            foreach (var item in RootList)
            {
                foreach (var item2 in item.results)
                {
                    ResultList.Add(item2);
                }
            }

            if (Convert.ToBoolean(FromLogin))
            {
                HttpClient client = new();
                var url = "https://randomuser.me/api/?results=5&inc=name,picture,email,location,phone";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        var data = await JsonSerializer.DeserializeAsync<Root>(responseStream);

                        if (data != null)
                        {
                            foreach (var item2 in data.results)
                            {
                                Random random = new Random();
                                double total = 0;
                                for (int i = 0; i < 10; i++)
                                {
                                    total += random.Next(0, 5);
                                }

                                item2.Rating = total / 10;

                                ResultList.Add(item2);
                            }

                            App.RootRepo.SaveItemWithChildren(data, true);
                        }
                    }
                }
            }

            
        }

        private async void GotoDetailMethod(object id)
        {
            var result = App.ResultRepo.GetItem((int)id,true);

            await Shell.Current.GoToAsync($"//DoctorsListView/DetailDoctorView?param={id}", true);
        }

        private async void RefreshMethod()
        {
            IsRefreshing = true;

            var url = "https://randomuser.me/api/?results=5&inc=name,picture,email,location,phone";
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<Root>(responseStream);

                    if (data != null)
                    {
                        foreach (var item in data.results)
                        {
                            Random random = new Random();
                            double total = 0;
                            for (int i = 0; i < 10; i++)
                            {
                                total += random.Next(0, 100);
                            }

                            item.Rating = total / 10;

                            ResultList.Add(item);
                        }

                        App.RootRepo.SaveItemWithChildren(data, true);
                    }
                }
            }


            IsRefreshing = false;
        }
        
        private async void CerrarSesion()
        {
            var user = App.loginRepository.GetCurrentUser();

            user.isUserLoggedIn = false;

            App.loginRepository.LoginOrLogout(user);

            await Shell.Current.GoToAsync("//LogInView", true);
        }
    }
}
