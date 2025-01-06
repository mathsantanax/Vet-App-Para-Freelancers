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
        private readonly NotificationService _notificationService;

        [ObservableProperty]
        private bool isToggle = false;

        public ICommand ServicosCommand { get; }
        public ICommand ProdutosCommand { get; }
        public ICommand FecharAppCommand { get; }
        public ConfigModelView()
        {
            FecharAppCommand = new Command(FecharApp);
            ServicosCommand = new Command(ServicosPage);
            ProdutosCommand = new Command(ProdutosPage);

            InicializarNotificacoes();
        }

        private async void InicializarNotificacoes()
        {
            try
            {
                IsToggle = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("o erro está aqui olha para cá >>>>>>>>>>>>>>>>>" + ex.Message);
                IsToggle = false;
            }
        }


        //partial void OnIsToggleChanged(bool value)
        //{
        //    if (value)
        //    {
        //        _notificationService.EnableNotifications();
        //    }
        //    else
        //    {
        //        _notificationService.DisableNotifications();
        //    }
        //}

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
