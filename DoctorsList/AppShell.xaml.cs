using DoctorsList.MVVM.VIEW;
using Microsoft.Win32;

namespace DoctorsList
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("LogInView", typeof(LoginView));
            Routing.RegisterRoute("DoctorsListView", typeof(DoctorsListView));
            Routing.RegisterRoute("DoctorsListView/DetailDoctorView", typeof(DoctorDetailView));
            
        }
    }
}
