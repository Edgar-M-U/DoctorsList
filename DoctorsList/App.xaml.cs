using DoctorsList.MVVM.MODEL;
using DoctorsList.Repositories;
using Location = DoctorsList.MVVM.MODEL.Location;

namespace DoctorsList
{
    public partial class App : Application
    {
        public static LoginRepository loginRepository { get; private set; }
        public static BaseRepository<Root> RootRepo { get; private set; }
        public static BaseRepository<Result> ResultRepo { get; private set; }
        public static BaseRepository<Name> NameRepo { get; private set; }
        public static BaseRepository<Location> LocationRepo { get; private set; }
        public static BaseRepository<Picture> PictureRepo { get; private set; }
        public static BaseRepository<Street> StreetRepo { get; private set; }
        public static BaseRepository<Coordinates> CoordinatesRepo { get; private set; }
        public static BaseRepository<Timezone> TimezoneRepo { get; private set; }

        public App(LoginRepository repo, 
            BaseRepository<Root> rootRepo,
            BaseRepository<Result> resultRepo,
            BaseRepository<Name> nameRepo,
            BaseRepository<Location> locationRepo,
            BaseRepository<Picture> pictureRepo,
            BaseRepository<Street> streetRepo,
            BaseRepository<Coordinates> coordinatesRepo,
            BaseRepository<Timezone> timezoneRepo)
        {
            InitializeComponent();

            loginRepository = repo;
            RootRepo = rootRepo;
            ResultRepo = resultRepo;
            NameRepo = nameRepo;
            LocationRepo = locationRepo;
            PictureRepo = pictureRepo;
            StreetRepo = streetRepo;
            CoordinatesRepo = coordinatesRepo;
            TimezoneRepo = timezoneRepo;

            MainPage = new AppShell();
        }

    }
}
