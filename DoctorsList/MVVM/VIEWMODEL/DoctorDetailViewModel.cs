using DoctorsList.MVVM.MODEL;
using System.Windows.Input;
using Location = Microsoft.Maui.Devices.Sensors.Location;
using Microsoft.Maui.Controls.Maps;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace DoctorsList.MVVM.VIEWMODEL
{
    [QueryProperty("DoctorID", "param")]
    public class DoctorDetailViewModel : BaseViewModel
    {
        private HttpClient client;

        public ICommand onAppearingCommand { get; }
        private string _doctorID;

        public string DoctorID
        {
            get { return _doctorID; }
            set { _doctorID = Uri.UnescapeDataString(value ?? string.Empty);
                OnPropertyChanged();
            }
        }


        private Result _myDoctor;
        public Result MyDoctor
        {
            get { return _myDoctor; }
            set { _myDoctor = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Place> Places { get; } = new();

        private Location _location;

        public Location Location
        {
            get { return _location; }
            set { _location = value; OnPropertyChanged(); }
        }



        public DoctorDetailViewModel()
        {
            client = new HttpClient();

            onAppearingCommand = new Command(onAppearingMethod);
            Location = new Location();
        }

        private async void onAppearingMethod()
        {
            if (!String.IsNullOrEmpty(DoctorID))
            {
                MyDoctor = App.ResultRepo.GetItem(Convert.ToInt32(DoctorID),true);

                Location.Latitude = Convert.ToDouble(MyDoctor.location.coordinates.latitude);
                Location.Longitude = Convert.ToDouble(MyDoctor.location.coordinates.longitude);

                var location = Location;
                var placemarks = await Geocoding.GetPlacemarksAsync(location);
                var address = placemarks?.FirstOrDefault()?.AdminArea;

                Places.Add(new Place()
                {
                    Location = location,
                    Address = address,
                    Description = "Current Location"
                });


            }
        }

    }
}
