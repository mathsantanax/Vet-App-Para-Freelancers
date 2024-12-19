using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vet_App_For_Freelancers.Data;
using Vet_App_For_Freelancers.DataAccess;
using Vet_App_For_Freelancers.Models.ModelServicos;

namespace Vet_App_For_Freelancers.ViewModels
{
    public partial class ProdutosViewModel : ObservableObject
    {

        SQLiteConnection connection = DatabaseConfig.GetConnection();
        private readonly ProdutoDataAccess produtoDataAccess;
        private readonly LaboratorioDataAccess laboratorioDataAccess;


        private string nomeLaboratorio;

        public string NomeLaboratorio
        {
            get => nomeLaboratorio;
            set
            {
                SetProperty(ref nomeLaboratorio, value);
                IsVisibleButtonLab = true;
            }
        }

        [ObservableProperty]
        private ObservableCollection<Produto> produtosColletion;

        [ObservableProperty]
        private ObservableCollection<Lab> labCollection;

        private bool _IsVisibleButtonLab = false;
        public bool IsVisibleButtonLab
        {
            get => _IsVisibleButtonLab;
            set => SetProperty(ref _IsVisibleButtonLab, value);
        }

        public ICommand AdicionarLaboratorioCommand { get; }

        

        public ProdutosViewModel()
        {
            produtoDataAccess = new ProdutoDataAccess(connection);
            laboratorioDataAccess = new LaboratorioDataAccess(connection);

            produtosColletion = new ObservableCollection<Produto>();
            labCollection = new ObservableCollection<Lab>();

            AdicionarLaboratorioCommand = new RelayCommand(async() => await CadastrarLaboratorio());

            Task.Run(async () => await InitializeAsync());
        }

        private async Task InitializeAsync()
        {
            await GetProdutos();
        }

        private async Task GetProdutos()
        {
            try
            {
                await Task.Delay(200);
                ProdutosColletion.Clear();
                var getProdutos = produtoDataAccess.GetAll(); 
                if (getProdutos.Any())
                {
                    foreach(var produtos in getProdutos)
                    {
                        ProdutosColletion.Add(produtos);
                    }
                }
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", $"{ex.Message}", "Ok");
            }
        }

        private async Task GetLaboratorios()
        {
            try
            {
                LabCollection.Clear();
                var getLab = laboratorioDataAccess.GetAll();
                if (getLab.Any())
                {
                    foreach(var lab in getLab)
                    {
                        LabCollection.Add(lab);
                    }
                }
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", $"{ex.Message}", "Ok");
            }
        }

        private async Task CadastrarLaboratorio()
        {
            try
            {
                if(NomeLaboratorio != null)
                {
                    laboratorioDataAccess.Insert(new Lab
                    {
                        NomeLaboratorio = nomeLaboratorio.ToUpper()
                    });
                    await Application.Current.MainPage.DisplayAlert("Sucesso", $"Cadastrado com Sucesso!", "Ok");
                    NomeLaboratorio = null;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Alerta", $"O Laboratorio não pode estar vazio!", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", $"{ex.Message}", "Ok");
            }
            finally
            {
                await GetLaboratorios();
            }
        }
    }
}
