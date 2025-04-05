using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using SQLite;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Vet_App_For_Freelancers.Data;
using Vet_App_For_Freelancers.DataAccess;
using Vet_App_For_Freelancers.Models.ModelPetETutor;

namespace Vet_App_For_Freelancers.ViewModels
{
    public partial class AdicionarPetViewModel : ObservableObject
    {
        SQLiteConnection _connection = DatabaseConfig.GetConnection();

        private readonly PetDataAccess petDataAccess;
        private readonly RacaDataAccess racaDataAccess;
        private readonly EspecieDataAccess especieDataAccess;

        [ObservableProperty]
        public string? nomePet;

        [ObservableProperty]
        public string? microship;

        [ObservableProperty]
        public int idadePet;

        [ObservableProperty]
        public string? sexoPet;

        [ObservableProperty]
        public float peso;

        [ObservableProperty]
        public ObservableCollection<Raca> racas;

        [ObservableProperty]
        public ObservableCollection<Especie> especies;

        [ObservableProperty]
        public Raca? racaSelecionada;

        [ObservableProperty]
        public Especie? especieSelecionada;

        private Tutor tutor1;

        private bool _IsLoaded = false;
        public bool IsLoaded
        {
            get => _IsLoaded;
            set => SetProperty(ref _IsLoaded, value);
        }
        public ICommand AdicionarRaca { get; }
        public ICommand AdicionarEspecie { get; }
        public ICommand AdicionarPet {  get; }
        public ICommand BackCommand { get; }
        public AdicionarPetViewModel(Tutor tutor)
        {
            tutor1 = tutor;

            petDataAccess = new PetDataAccess(_connection);
            racaDataAccess = new RacaDataAccess(_connection);
            especieDataAccess = new EspecieDataAccess(_connection);

            racas = new ObservableCollection<Raca>();
            especies = new ObservableCollection<Especie>();

            AdicionarRaca = new Command(CommandAdicionarRaca);
            AdicionarEspecie = new Command(CommandAdicionarEspecie);
            AdicionarPet = new Command(CommandAdicionarPet);
            BackCommand = new Command<object>(GoBack);

            _ = InitializeAsync();
        }

        // inicio assincrono
        private async Task InitializeAsync()
        {
            await GetRacas();
            await GetEspecies();
        }

        // pega as raças no banco de dados
        private async Task GetRacas()
        {
            await Task.Delay(200);
            Racas.Clear();

            try
            {
                var raca = racaDataAccess.GetAll();
                foreach (var item in raca)
                {
                    Racas.Add(new Raca
                    {
                        Id = item.Id,
                        TipoRaca = item.TipoRaca,
                    });
                    Debug.WriteLine($"Racas encontradas: {item.TipoRaca}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsLoaded = true;
            }
        }

        // função para adicionar a raça
        private async void CommandAdicionarRaca()
        {
            string raca = await Application.Current!.MainPage!.DisplayPromptAsync("Cadastro", "Insira a Raca");
            try
            {
                if (raca != null)
                {
                    racaDataAccess.Insert(new Raca { TipoRaca = raca.ToUpper()});
                    await Application.Current.MainPage.DisplayAlert($"Raça Cadastrada Com sucesso \n{raca}", "Alerta", "Ok");
                    await GetRacas();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Conexão: {_connection.DatabasePath}");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine($"Racas encontradas: {raca.Count()}");
            }
        }

        // função parar pegar as especies no banco de dados
        private async Task GetEspecies()
        {
            await Task.Delay(200);
            Especies.Clear();

            try
            {
                var especie = especieDataAccess.GetAll();

                if(!especie.Any(e => e.NomeEspecie.Equals("CACHORRO", StringComparison.OrdinalIgnoreCase)))
                    especieDataAccess.Insert(new Especie { NomeEspecie = "CACHORRO" });

                if(!especie.Any(e => e.NomeEspecie.Equals("GATO", StringComparison.OrdinalIgnoreCase)))
                    especieDataAccess.Insert(new Especie { NomeEspecie = "GATO" });


                foreach (var item in especie)
                {
                    Especies.Add(new Especie
                    {
                        Id = item.Id,
                        NomeEspecie = item.NomeEspecie,
                    });
                    Debug.WriteLine($"Especies encontradas: {item.NomeEspecie}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsLoaded = true;
            }
        }

        //função para adicionar especie
        private async void CommandAdicionarEspecie()
        {
            string especie = await Application.Current!.MainPage!.DisplayPromptAsync("Cadastro", "Insira a Especie");
            try
            {
                if (especie != null)
                {
                    especieDataAccess.Insert(new Especie { NomeEspecie = especie.ToUpper() });
                    await Application.Current.MainPage.DisplayAlert($"Especie Cadastrada Com sucesso \n{especie}", "Alerta", "Ok");
                    await GetEspecies();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Conexão: {_connection.DatabasePath}");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine($"Racas encontradas: {especie.Count()}");
            }
        }

        // função para adicionar pet
        private async void CommandAdicionarPet()
        {
            petDataAccess.Insert(new Pet
            {
                NomePet = NomePet!.ToUpper(),
                NumeroMicrochip = Microship!,
                Idade = IdadePet!,
                Sexo = SexoPet!.ToUpper(),
                Peso = Peso,
                IdRaca = RacaSelecionada!.Id,
                IdEspecie = EspecieSelecionada!.Id,
                IdTutor = tutor1.Id,
            });
            await Application.Current!.MainPage!.DisplayAlert($"Cadastrado Realizado Com sucesso", "Alerta", "Ok");
            WeakReferenceMessenger.Default.Send(new PetAddedMessage(tutor1.Id));
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        // função para voltar para a pagina anterior
        private async void GoBack(object obj)
        {
            await Application.Current!.MainPage!.Navigation.PopModalAsync();
        }
    }
}
