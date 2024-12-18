using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vet_App_For_Freelancers.Models.ModelPetETutor;
using Vet_App_For_Freelancers.Views;

namespace Vet_App_For_Freelancers.ViewModels
{
    public partial class PetPageViewModel : ObservableObject
    {
        public Pet Pet { get; set; }
        public Tutor Tutor { get; set; }

        private bool _IsLoaded = false;
        public bool IsLoaded
        {
            get => _IsLoaded;
            set => SetProperty(ref _IsLoaded, value);
        }

        public ICommand BackCommand { get; }
        public ICommand AdicionarServicoCommand { get; }

        public PetPageViewModel(Pet pet, Tutor tutor)
        {
            Pet = pet;
            Tutor = tutor;
            BackCommand = new Command<object>(GoBack);
            AdicionarServicoCommand = new Command(AdicionarServico);
            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            IsLoaded = true;
        }

        private async void AdicionarServico()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new AdicionarNovoServicoPageView(Tutor, Pet));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task GetServices()
        {

        }

        private async void GoBack(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
