using Plugin.LocalNotification;
using Plugin.LocalNotification.EventArgs;
using Vet_App_For_Freelancers.Notification;
using Vet_App_For_Freelancers.Views;

namespace Vet_App_For_Freelancers
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Current.UserAppTheme = AppTheme.Light;
            MainPage = new AppShell();
        }
    }
}
