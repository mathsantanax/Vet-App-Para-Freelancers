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
using Vet_App_For_Freelancers.DataAccess;
using Vet_App_For_Freelancers.Views;
using SQLite;
using Vet_App_For_Freelancers.Data;

namespace Vet_App_For_Freelancers.ViewModels
{
    public class HomePageViewModel : ObservableObject
    {
        const int RefreshDuration = 2;
        private bool isRefreshing;

        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetProperty(ref isRefreshing, value);
        }

        private bool _IsLoaded = false;
        public bool IsLoaded
        {
            get => _IsLoaded;
            set => SetProperty(ref _IsLoaded, value);
        }

        private string searchQuery;
        public string SearchQuery
        {
            get => searchQuery;
            set
            {
                if (SetProperty(ref searchQuery, value))
                {
                    FilterTutores();
                }
            }
        }

        private ObservableCollection<Tutor> filteredTutores;
        public ObservableCollection<Tutor> FilteredTutores
        {
            get => filteredTutores;
            set => SetProperty(ref filteredTutores, value);
        }
        SQLiteConnection connection = DatabaseConfig.GetConnection();
        private readonly TutorDataAccess tutorDataAccess;
        public ObservableCollection<Tutor> Tutores { get; private set; }
        public ICommand RefreshCommand => new Command(async () => await RefreshItemsAsync());
        public ICommand TutoresTapCommand { get; }
        public ICommand AddNewTutor { get; }
        public ICommand CofigCommand { get; }

        public HomePageViewModel()
        {
            tutorDataAccess = new TutorDataAccess(connection);
            Tutores = new ObservableCollection<Tutor>();
            TutoresTapCommand = new Command<Tutor>(OnItemSelected);
            CofigCommand = new Command(ConfigAsync);
            AddNewTutor = new Command(AddTutor);
            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            await GetRegisterAsync();
        }

        private Tutor selectedTutor;
        public Tutor SelectedTutor
        {
            get => selectedTutor;
            set
            {
                if (SetProperty(ref selectedTutor, value))
                {
                    TutoresTapCommand.Execute(selectedTutor);
                }
            }
        }

        public async void ConfigAsync()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new ConfiguracoesPageView());
            }
            catch
            {
                throw;
            }
        }

        private async Task GetRegisterAsync()
        {
            try
            {
                Tutores.Clear();
                await Task.Delay(500);
                var getTutor = tutorDataAccess.GetAllTutors();
                if (getTutor != null && getTutor.Any())
                {
                    foreach (var tutor in getTutor)
                    {
                        Tutores.Add(tutor);
                    }
                }

                // Atualiza a lista filtrada
                FilteredTutores = new ObservableCollection<Tutor>(Tutores);
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

        private async void OnItemSelected(Tutor selectedTutor)
        {
            try
            {
                if (selectedTutor != null)
                {
                    await Application.Current.MainPage.Navigation.PushModalAsync(new TutorPageView(selectedTutor));
                    SelectedTutor = null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao selecionar pet: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Erro", $"Ocorreu um problema ao tentar abrir o serviço.{ex.Message}", "OK");
            }
        }

        private async Task RefreshItemsAsync()
        {
            IsRefreshing = true;
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(RefreshDuration));
                FilteredTutores.Clear();
                Tutores.Clear();
                await GetRegisterAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao atualizar itens: {ex.Message}");
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        private async void AddTutor()
        {
            string nome = await Application.Current.MainPage.DisplayPromptAsync("Cadastro", "Nome do Tutor");

            if (nome != null)
            {
                string ddd = await Application.Current.MainPage.DisplayPromptAsync("Cadastro", "Insira o DDD", maxLength: 3, keyboard: Keyboard.Numeric);
                if (ddd != null)
                {
                    string celular = await Application.Current.MainPage.DisplayPromptAsync("Cadastro", "Nº de Celular", maxLength: 9, keyboard: Keyboard.Numeric);
                    if (celular != null)
                    {
                        tutorDataAccess.Insert(new Tutor
                        {
                            NomeTutor = nome.ToUpper(),
                            Celular = decimal.Parse($"{ddd}{celular}")
                        });
                        await Application.Current.MainPage.DisplayAlert("Sucesso", "Realizado com Sucesso", "OK");
                        await RefreshItemsAsync();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Alerta", "Celular nulo", "OK");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Alerta", "DDD nulo", "OK");
                }
            }
        }

        private void FilterTutores()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                FilteredTutores = new ObservableCollection<Tutor>(Tutores);
            }
            else
            {
                var query = SearchQuery.ToLower();
                var filtered = Tutores.Where(t => t.NomeTutor.ToLower().Contains(query)).ToList();
                FilteredTutores = new ObservableCollection<Tutor>(filtered);
            }
        }
    }
}
