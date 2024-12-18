using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vet_App_For_Freelancers.Views;

namespace Vet_App_For_Freelancers.ViewModels
{
    public class ConfigModelView : ObservableObject
    {


        public ICommand ServicosCommand { get; }
        public ICommand ProdutosCommand { get; }
        public ICommand FecharAppCommand { get; }
        public ConfigModelView()
        {
            FecharAppCommand = new Command(FecharApp);
            ServicosCommand = new Command(ServicosPage);
        }

        public async void ServicosPage()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new ServicosPageview());
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async void ProdutosPage()
        {

        }

        public async void FecharApp()
        {
            await Task.Delay(200);
            Environment.Exit(0);
        }
    }
}
