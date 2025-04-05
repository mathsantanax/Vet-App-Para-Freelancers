using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Vet_App_For_Freelancers.Models.ModelPetETutor;
using Vet_App_For_Freelancers.Views;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Vet_App_For_Freelancers.DataAccess;
using SQLite;
using Vet_App_For_Freelancers.Data;

namespace Vet_App_For_Freelancers.ViewModels
{
    public partial class TutorViewModel : ObservableObject
    {
        SQLiteConnection _connection = DatabaseConfig.GetConnection();

        private readonly PetDataAccess petDataAccess;
        private readonly RacaDataAccess racaDataAccess;
        private readonly EspecieDataAccess especieDataAccess;

        private Tutor _Tutor { get; set; }
        public Tutor Tutor
        {
            get => _Tutor;
        }

        [ObservableProperty]
        private ObservableCollection<Pet> pets;

        [ObservableProperty]
        public bool _IsLoaded = false;

        public ICommand BackCommand { get; }
        public ICommand PetTapCommand { get; }
        public ICommand AddNewPet { get; }

        public TutorViewModel(Tutor tutor)
        {
            petDataAccess = new PetDataAccess(_connection);
            racaDataAccess = new RacaDataAccess(_connection);
            especieDataAccess = new EspecieDataAccess(_connection);

            _Tutor = tutor;
            AddNewPet =new Command(OnAddPetClicked);
            pets = new ObservableCollection<Pet>();
            PetTapCommand = new Command<Pet>(OnItemSelected);
            BackCommand = new Command<object>(GoBack);

            // Registrar para ouvir mensagens de Pets adicionados
            WeakReferenceMessenger.Default.Register<PetAddedMessage>(this, OnPetAddedMessageReceived);

            _ = InitializeAsync();
        }

        // inicio assincrono
        private async Task InitializeAsync()
        {
            await LoadPetsAsync();
        }

        // carrega os pets do cliente selecionado 

        private async Task LoadPetsAsync()
        {
            try
            {
                Pets.Clear();

                await Task.Delay(500);
                var petsFromDb = petDataAccess.GetByIdTutor(Tutor.Id);

                if (petsFromDb != null && petsFromDb.Any())
                {
                    foreach (var p in petsFromDb)
                    {
                        p.Raca = racaDataAccess.GetById(p.IdRaca);
                        p.Especie = especieDataAccess.GetById(p.IdEspecie);
                        Pets.Add(p);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao obter registros: {ex.Message}");
            }
            finally
            {
                IsLoaded = true;
            }
        }


        // quando o pet for selecionar abre a pagina do pet
        private async void OnItemSelected(Pet selectedPet)
        {
            try
            {
                await Shell.Current.GoToAsync($"petpageview?petId={selectedPet.Id}");

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao selecionar pet: {ex.Message}");
            }
        }

        // função para adicionar pet 
        private async void OnAddPetClicked()
        {
            try
            {
                await Application.Current!.MainPage!.Navigation.PushModalAsync(new AdicionarPetPageView(_Tutor));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao adicionar pet: {ex.Message}");
            }
        }

        // retorna para pagina anterior
        private async void GoBack(object obj)
        {
            await Application.Current!.MainPage!.Navigation.PopModalAsync();
        }
        // mensagem para quando o pet for adicionado recarregar pagina e mostrar o pet novo
        private async void OnPetAddedMessageReceived(object recipient, PetAddedMessage message)
        {
            if (message.Value == Tutor.Id)
            {
                await LoadPetsAsync();
            }
        }
        // descontrutor
        ~TutorViewModel()
        {
            WeakReferenceMessenger.Default.Unregister<PetAddedMessage>(this);
        }

    }
    public class PetAddedMessage : ValueChangedMessage<int>
    {
        public PetAddedMessage(int tutorId) : base(tutorId) { }
    }
}
