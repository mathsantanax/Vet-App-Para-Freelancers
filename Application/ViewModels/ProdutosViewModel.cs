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

        private Produto EditarProduto = new Produto();

        [ObservableProperty]
        private ObservableCollection<Produto> produtosColletion;

        [ObservableProperty]
        private ObservableCollection<Lab> labCollection;

        [ObservableProperty]
        private Lab selectedLab;

        [ObservableProperty]
        private string nome;

        [ObservableProperty]
        private decimal valor;

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

        private bool _IsVisibleLab = false;
        public bool IsVisibleLab
        {
            get => _IsVisibleLab;
            set => SetProperty(ref _IsVisibleLab, value);
        }
        
        private bool _IsVisibleEditProduto = false;
        public bool IsVisibleEditProduto
        {
            get => _IsVisibleEditProduto;
            set => SetProperty(ref _IsVisibleEditProduto, value);
        }

        private bool _IsVisibleButtons = true;
        public bool IsVisibleButtons
        {
            get => _IsVisibleButtons;
            set => SetProperty(ref _IsVisibleButtons, value);
        }
        
        private bool _IsVisibleProduto = false;
        public bool IsVisibleProduto
        {
            get => _IsVisibleProduto;
            set => SetProperty(ref _IsVisibleProduto, value);
        }

        private bool _IsVisibleButtonLab = false;
        public bool IsVisibleButtonLab
        {
            get => _IsVisibleButtonLab;
            set => SetProperty(ref _IsVisibleButtonLab, value);
        }

        public ICommand AdicionarLaboratorioCommand { get; }
        public ICommand DeixarVisivelLaboratorioCommand { get; }
        public ICommand DeixarVisivelProdutosCommand { get; }
        public ICommand AdicionarProdutosCommand { get; }
        public ICommand ProdutoTapCommand { get; }
        public ICommand EditarProdutoCommand { get; }
        

        public ProdutosViewModel()
        {
            produtoDataAccess = new ProdutoDataAccess(connection);
            laboratorioDataAccess = new LaboratorioDataAccess(connection);

            produtosColletion = new ObservableCollection<Produto>();
            labCollection = new ObservableCollection<Lab>();

            AdicionarLaboratorioCommand = new RelayCommand(async() => await CadastrarLaboratorio());
            DeixarVisivelLaboratorioCommand = new RelayCommand(async () => await LaboratorioVisivel());
            DeixarVisivelProdutosCommand = new RelayCommand(async () => await ProdutosVisivel());
            AdicionarProdutosCommand = new RelayCommand(async () => await AddProdutos());
            ProdutoTapCommand = new Command<Produto>(OnItemSelected);
            EditarProdutoCommand = new RelayCommand(async () => await EditProdutos());

            Task.Run(async () => await InitializeAsync());
        }

        private async Task InitializeAsync()
        {
            await GetProdutos();
            await GetLaboratorios();
        }

        private async void OnItemSelected(Produto selectedProduto)
        {
            try
            {
                if (selectedProduto != null)
                {
                    EditarProduto.Id = selectedProduto.Id;
                    EditarProduto.NomePruduto = selectedProduto.NomePruduto;
                    EditarProduto.IdLab = selectedProduto.IdLab;
                    EditarProduto.Preco = selectedProduto.Preco;

                    Nome = selectedProduto.NomePruduto;
                    Valor = selectedProduto.Preco;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", $"Erro ao selecionar o serviço. \n {ex.Message}", "Ok");
            }
            finally
            {
                IsVisibleEditProduto = true;
                IsVisibleButtons = false;
            }
        }

        private async Task EditProdutos()
        {
            try
            {
                if (Nome != null && Nome != "")
                {
                    if (Valor != null & Valor != 0)
                    {
                        EditarProduto.NomePruduto = Nome.ToUpper();
                        EditarProduto.Preco = Valor;
                        produtoDataAccess.Update(EditarProduto);
                        await Application.Current.MainPage.DisplayAlert("Sucesso", "Alterado com Sucesso", "Ok");
                        Nome = null;
                        Valor = 0;
                    }
                    else
                    { 
                        await Application.Current.MainPage.DisplayAlert("Erro", "Valor Não pode estar Vazio", "Ok");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Nome Não pode estar Vazio", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", $"{ex.Message}", "Ok");
            }
            finally
            {
                await GetProdutos();
                IsVisibleEditProduto = false;
                IsVisibleButtons = true;
            }
        }

        private async Task AddProdutos()
        {
            try
            {
                if (SelectedLab != null)
                {
                    if(Nome != null && Nome != "")
                    {
                        if(Valor != null & Valor != 0)
                        {
                            produtoDataAccess.Insert(new Produto
                            {
                                IdLab = SelectedLab.Id,
                                NomePruduto = nome.ToUpper(),
                                Preco = valor
                            });
                            await Application.Current.MainPage.DisplayAlert("Sucesso", "Cadastrado com Sucesso", "Ok");
                            Nome = null;
                            Valor = 0;
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Erro", "Valor Não pode estar Vazio", "Ok");
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Erro", "Nome Não pode estar Vazio", "Ok");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Selecione um Laboratório", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", $"{ex.Message}", "Ok");
            }
            finally
            {
                await GetProdutos();
                IsVisibleProduto = false;
                IsVisibleButtons = true;
            }
        }

        private async Task ProdutosVisivel()
        {
            await Task.Delay(100);
            if (IsVisibleProduto == false)
            {
                IsVisibleProduto = true;
                IsVisibleButtons = false;
            }
        }

        private async Task LaboratorioVisivel()
        {
            await Task.Delay(100);
            if(IsVisibleLab == false)
            {
                IsVisibleLab = true;
                IsVisibleButtons = false;
            }
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
                        produtos.Lab = laboratorioDataAccess.GetById(produtos.IdLab);
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
                if(NomeLaboratorio != null && NomeLaboratorio != "")
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
                IsVisibleLab = false;
                IsVisibleButtons = true;
            }
        }
    }
}
