using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vet_App_For_Freelancers.Models.ModelPetETutor;

namespace Vet_App_For_Freelancers.ViewModels
{
    public partial class FinalizarNovoServicoModelView : ObservableObject
    {
        [ObservableProperty]
        public Tutor tutorView;

        [ObservableProperty]
        public Pet petView;

        public ICommand BackCommand { get; }
        public FinalizarNovoServicoModelView(Tutor tutorView, Pet petView,)
        {
            Task.Run(async () => await InitializeAsync());

            BackCommand = new Command<object>(GoBack);

        }

        private async void GoBack(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();

        }

        private async Task InitializeAsync()
        {
            await GetTutorEPet();
        }

        private async Task GetTutorEPet()
        {
            await Task.Delay(100);
            TutorView.Id = 0;
            TutorView.NomeTutor = "matheus augusto santana".ToUpper();
            TutorView.Celular = 13981639944m;

            PetView.NomePet = "maskara".ToUpper();
        }
    }
}
