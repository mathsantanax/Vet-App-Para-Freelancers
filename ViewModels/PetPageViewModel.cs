using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
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
        private readonly TutorDataAccess _tutorDataAccess;
        private readonly PetDataAccess _petDataAccess;
        private readonly RacaDataAccess _racaDataAccess;
        private readonly EspecieDataAccess _especieDataAccess;

        public Pet Pet { get; set; }
        public Tutor Tutor { get; set; }

        private bool _IsLoaded = false;
        public bool IsLoaded
        {
            get => _IsLoaded;
            set => SetProperty(ref _IsLoaded, value);
        }

        [ObservableProperty]
        private ObservableCollection<Atendimento> atendimentosCollection;

        public ICommand BackCommand { get; }
        public ICommand AdicionarServicoCommand { get; }

        public PetPageViewModel(int pet)
        {
            _atendimentoDataAccess = new AtendimentoDataAccess(_connection);
            _produtosAtendimentoDataAccess = new ProdutosAtendimentoDataAccess(_connection);
            _servicosAtendimentoDataAccess = new ServicosAtendimentoDataAccess(_connection);
            _proxVacinacaoAtendimentoDataAccess = new ProxVacinacaoAtendimentoDataAccess(_connection);
            _pagamentoDataAccess = new PagamentoDataAccess(_connection);
            _tutorDataAccess = new TutorDataAccess(_connection);
            _petDataAccess = new PetDataAccess(_connection);
            _racaDataAccess = new RacaDataAccess(_connection);
            _especieDataAccess = new EspecieDataAccess(_connection);
            BackCommand = new Command<object>(GoBack);
            AdicionarServicoCommand = new Command(AdicionarServico);
            atendimentosCollection = new ObservableCollection<Atendimento>();

            WeakReferenceMessenger.Default.Register<PetMessage>(this, OnPetMessageReceived);
            _ = InitializeAsync(pet);
        }

        private async Task InitializeAsync(int id)
        {
            await GetPet(id);
            await GetTutor();
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
                Debug.WriteLine(ex.Message);
            }
        }

        private async Task GetServices()
        {
            await Task.Delay(200);
            AtendimentosCollection.Clear();
            try
            {
                var atendimentos = _atendimentoDataAccess.SearchByIdPet(Pet.Id);
                foreach (var atendimento in atendimentos)
                {
                    atendimento.itemAtendimento = _produtosAtendimentoDataAccess.getProdutosByIdAtendimento(atendimento.Id);
                    atendimento.ItemServico = _servicosAtendimentoDataAccess.GetItemServicos(atendimento.Id);
                    atendimento.Pagamento = _pagamentoDataAccess.GetById(atendimento.IdPagamento);

                    AtendimentosCollection.Add(atendimento);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async Task GetTutor()
        {
            try
            {
                Tutor = _tutorDataAccess.GetById(Pet.IdTutor);
                await Task.Delay(100);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async Task GetPet(int pet)
        {
            try
            {
                Pet = _petDataAccess.GetById(pet);
                if(Pet != null)
                {
                    Pet.Raca = _racaDataAccess.GetById(Pet.IdRaca);
                    Pet.Especie = _especieDataAccess.GetById(Pet.IdEspecie);
                }
                await Task.Delay(100);
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

        private void OnPetMessageReceived(object recipient, PetMessage message)
        {
            if (message.Value == Pet.Id)
            {
                _ = GetServices();
            }
        }

        ~PetPageViewModel()
        {
            WeakReferenceMessenger.Default.Unregister<PetMessage>(this);
        }
    }
    public class PetMessage : ValueChangedMessage<int>
    {
        public PetMessage(int tutorId) : base(tutorId) { }
    }
}
