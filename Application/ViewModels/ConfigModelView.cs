using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vet_App_For_Freelancers.Notification;
using Vet_App_For_Freelancers.Views;

namespace Vet_App_For_Freelancers.ViewModels
{
    public partial class ConfigModelView : ObservableObject
    {
        [ObservableProperty]
        private bool isNotificationEnabled;

        public NotificationService NotificationService { get; }

        public ICommand ServicosCommand { get; }
        public ICommand ProdutosCommand { get; }
        public ICommand FecharAppCommand { get; }
        public ConfigModelView()
        {
            // Inicializando o NotificationService
            NotificationService = new NotificationService();

            FecharAppCommand = new Command(FecharApp);
            ServicosCommand = new Command(ServicosPage);
            ProdutosCommand = new Command(ProdutosPage);

            _ = InicializarNotificacoes();
        }

        private async Task InicializarNotificacoes()
        {
            try
            {
                IsNotificationEnabled = await NotificationService.AreNotificationsEnabled();
            }
            catch (Exception ex)
            {
                // Log or handle the error appropriately
                Debug.WriteLine($"Erro ao inicializar notificações: {ex.Message}");
            }
        }

        partial void OnIsNotificationEnabledChanged(bool value)
        {
            if (value)
            {
                NotificationService.EnableNotifications();
            }
            else
            {
                NotificationService.DisableNotifications();
            }
        }

        private async void ServicosPage()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new ServicosPageview());
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", $"{ex.Message}", "Ok");
            }
        }


        private async void ProdutosPage()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new ProdutosPageView());
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", $"{ex.Message}", "Ok");
            }
        }

        private async void FecharApp()
        {
            await Task.Delay(200);
            Environment.Exit(0);
        }
    }
}
