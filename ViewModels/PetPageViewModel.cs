using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vet_App_For_Freelancers.Data;
using Vet_App_For_Freelancers.DataAccess;
using Vet_App_For_Freelancers.Models.ModelPetETutor;
using Vet_App_For_Freelancers.Models.ModelServicos;
using Vet_App_For_Freelancers.Views;

namespace Vet_App_For_Freelancers.ViewModels
{
    public partial class PetPageViewModel : ObservableObject
    {
        SQLiteConnection _connection = DatabaseConfig.GetConnection();

        private readonly AtendimentoDataAccess _atendimentoDataAccess;
        private readonly ProdutosAtendimentoDataAccess _produtosAtendimentoDataAccess;
        private readonly ServicosAtendimentoDataAccess _servicosAtendimentoDataAccess;
        private readonly ProxVacinacaoAtendimentoDataAccess _proxVacinacaoAtendimentoDataAccess;
        private readonly PagamentoDataAccess _pagamentoDataAccess;
        public Pet Pet { get; set; }
        public Tutor Tutor { get; set; }

        private bool _IsLoaded = false;
        public bool IsLoaded
        {
            get => _IsLoaded;
            set => SetProperty(ref _IsLoaded, value);
        }

        public ObservableCollection<Atendimento> atendimentosCollection;

        public ICommand BackCommand { get; }
        public ICommand AdicionarServicoCommand { get; }

        public PetPageViewModel(Pet pet, Tutor tutor)
        {
            _atendimentoDataAccess = new AtendimentoDataAccess(_connection);
            Pet = pet;
            Tutor = tutor;
            BackCommand = new Command<object>(GoBack);
            AdicionarServicoCommand = new Command(AdicionarServico);
            atendimentosCollection = new ObservableCollection<Atendimento>();
            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            await GetServices();
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
            await Task.Delay(100);
            try
            {
                var atendimentos = _atendimentoDataAccess.SearchByIdPet(Pet.Id);
                Debug.Write("total de Atendimentos Coletado no banco" + atendimentos.Count);
                foreach (var atendimento in atendimentos)
                {
                    atendimentosCollection.Add(atendimento);
                    await Application.Current.MainPage.DisplayAlert("Alerta", $"{atendimento}", "Ok");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void GoBack(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
