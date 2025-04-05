using Plugin.LocalNotification;
using Plugin.LocalNotification.EventArgs;
using Vet_App_For_Freelancers.Notification;

namespace Vet_App_For_Freelancers
{
    public partial class App : Application
    {
        NotificationService notificationService = new NotificationService();
        public App()
        {
            InitializeComponent();

            UserAppTheme = AppTheme.Light;
            MainPage = new AppShell();
            notificationService?.VerificarPermissao(); // verifica se tem permissão para notificação
            LocalNotificationCenter.Current.NotificationActionTapped += AbrirPaginaPet; // quando clicado na notificação abrir pet page
        }
        // metodo para abrir a pagina do pet
        private async void AbrirPaginaPet(NotificationEventArgs e)
        {
            if (e.Request.ReturningData is string petIdStr && int.TryParse(petIdStr, out int petId))
            {
                await Shell.Current.GoToAsync($"petpageview?petId={e.Request.NotificationId}");
            }
        }
    }
}
