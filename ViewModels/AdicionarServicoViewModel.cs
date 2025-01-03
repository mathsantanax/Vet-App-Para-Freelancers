using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
using Vet_App_For_Freelancers.Presentation;
using Vet_App_For_Freelancers.Views;

namespace Vet_App_For_Freelancers.ViewModels
{
    public partial class AdicionarServicoViewModel : ObservableObject
    {
        SQLiteConnection _connection = DatabaseConfig.GetConnection();

        private readonly ServicoDataAccess _servicoDataAccess;
        private readonly ProdutoDataAccess _produtoDataAccess;
        private readonly LaboratorioDataAccess _laboratorioDataAccess;

        [ObservableProperty]
        public Tutor tutorView;

        [ObservableProperty]
        public Pet petView;

        private Servico servicoSelecionado;

        public Servico ServicoSelecionado
        {
            get => servicoSelecionado;
            set
            {
                SetProperty(ref servicoSelecionado, value);
                AtualizarPreco();
                IsLoadedItemServico = true;
            }
        }

        private Produto produtoSelecionado;

        public Produto ProdutoSelecionado
        {
            get => produtoSelecionado;
            set
            {
                SetProperty(ref produtoSelecionado, value);
                AtualizarPreco();
                IsLoadedItemProduto = true;
            }
        }

        [ObservableProperty]
        private decimal amount;

        [ObservableProperty]
        private decimal desconto;

        private decimal _TotalAmout;
        public decimal TotalAmout
        {
            get => _TotalAmout;
            set
            {
                SetProperty(ref _TotalAmout, value);
            }
        }

        [ObservableProperty]
        private int quantidadeServicos = 1;

        [ObservableProperty]
        private int quantidadeProdutos = 1;

        [ObservableProperty]
        private decimal totalServicos;

        [ObservableProperty]
        private decimal totalProdutos;

        public ObservableCollection<Produto> ObservableProdutos { get; set; }

        public ObservableCollection<Servico> ObservableServico {  get; set; }

        public ObservableCollection<ItemServico> itemServicos { get; set; }

        public ObservableCollection<ItemAtendimento> itemAtendimentos { get; set; }

        public ObservableCollection<ServicoPresentation> ApresentacaoItems { get; set; }

        private bool _IsLoaded = false;
        public bool IsLoaded
        {
            get => _IsLoaded;
            set => SetProperty(ref _IsLoaded, value);
        }

        private bool _IsLoadedDesc = false;

        public bool IsLoadedDesc
        {
            get => _IsLoadedDesc;
            set => SetProperty(ref _IsLoadedDesc, value);
        }

        private bool _IsLoadedItemProduto = false;

        public bool IsLoadedItemProduto
        {
            get => _IsLoadedItemProduto;
            set => SetProperty(ref _IsLoadedItemProduto, value);
        }
        
        private bool _IsLoadedItemServico = false;

        public bool IsLoadedItemServico
        {
            get => _IsLoadedItemServico;
            set => SetProperty(ref _IsLoadedItemServico, value);
        }
        public ICommand BackCommand { get; }
        public ICommand AdicionarQuantidadeServicosCommand { get; }
        public ICommand RemoverQuantidadeServicosCommand { get; }
        public ICommand AdicionarQuantidadeProdutosCommand { get; }
        public ICommand RemoverQuantidadeProdutosCommand { get; }

        public ICommand AdicionarServico { get; }
        public ICommand AdicionarProdutos { get; }

        public ICommand AdicionarDesconto { get; }

        public ICommand FinalizarCommand { get; }

        public AdicionarServicoViewModel(Tutor tutor, Pet pet)
        {
            _servicoDataAccess = new ServicoDataAccess(_connection);
            _produtoDataAccess = new ProdutoDataAccess(_connection);
            _laboratorioDataAccess = new LaboratorioDataAccess(_connection);

            ObservableServico = new ObservableCollection<Servico>();
            ObservableProdutos = new ObservableCollection<Produto>();
            itemServicos = new ObservableCollection<ItemServico>();
            itemAtendimentos = new ObservableCollection<ItemAtendimento>();
            ApresentacaoItems = new ObservableCollection<ServicoPresentation>();

            TutorView = tutor;
            PetView = pet;

            AdicionarServico = new AsyncRelayCommand(InsertServiceOnItemServicoAsync);
            AdicionarProdutos = new AsyncRelayCommand(InsertProdutosOnItemAtendimento);
            AdicionarDesconto = new AsyncRelayCommand(InsertDesconto);

            AdicionarQuantidadeServicosCommand = new Command(() => AlterarQuantidadeServicos(1));
            RemoverQuantidadeServicosCommand = new Command(() => AlterarQuantidadeServicos(-1));

            AdicionarQuantidadeProdutosCommand = new Command(() => AlterarQuantidadeProdutos(1));
            RemoverQuantidadeProdutosCommand = new Command(() => AlterarQuantidadeProdutos(-1));
            BackCommand = new Command<object>(GoBack);
            FinalizarCommand = new Command(async() => await FinalizarAsync());

            Task.Run(async () => await InitializeAsync());
        }

        private async Task InitializeAsync()
        {
            await GetServicos();
            await GetProdutos();
        }

        private async Task FinalizarAsync()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new FinalizarNovoServicoPageView(tutorView, petView, amount, desconto, itemAtendimentos, itemServicos));
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Erro ao carregar serviços: {ex.Message}");
            }
        }

        private async Task GetProdutos()
        {
            IsLoaded = false;
            try
            {
                ObservableProdutos.Clear();
                var produtos = await Task.Run(() => _produtoDataAccess.GetAll());
                if(produtos != null)
                {
                    foreach(var produto in produtos)
                    {
                        ObservableProdutos.Add(produto);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao carregar os produtos: {ex.Message}");
            }
            finally
            {
                IsLoaded = true;
            }
        }

        private async Task GetServicos()
        {
            IsLoaded = false;
            try
            {
                ObservableServico.Clear();
                var servicos = await Task.Run(() => _servicoDataAccess.GetAll());
                if (servicos != null)
                {
                    foreach (var servico in servicos)
                    {
                        ObservableServico.Add(servico);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao carregar serviços: {ex.Message}");
            }
            finally
            {
                IsLoaded = true;
            }
        }
        private async void GoBack(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        #region Quantidades
        private void AlterarQuantidadeServicos(int incremento)
        {
            if(servicoSelecionado != null)
            {
                QuantidadeServicos = Math.Max(1, QuantidadeServicos + incremento);
                TotalServicos = ServicoSelecionado.Preco * QuantidadeServicos; 
            }
        }

        private void AlterarQuantidadeProdutos(int incremento)
        {
            if(produtoSelecionado != null)
            {
                QuantidadeProdutos = Math.Max(1, QuantidadeProdutos + incremento);
                TotalProdutos = ProdutoSelecionado.Preco * QuantidadeProdutos;

            }
        }
        #endregion


        #region Adicionar Serviços e Produtos e Desconto

        private void AtualizarPreco()
        {
            TotalServicos = ServicoSelecionado?.Preco ?? 0;
            TotalProdutos = ProdutoSelecionado?.Preco ?? 0;
        }

        private void AtualizarTotal()
        {
            TotalAmout = Amount - Desconto;
        }

        private async Task InsertDesconto()
        {
            if (Desconto > Amount)
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "Desconto não pode ser maior que total de serviço", "ok");
            }
            else
            {
                AtualizarTotal();
            }
        }
        private async Task InsertServiceOnItemServicoAsync()
        {
            if(ServicoSelecionado != null)
            {
                ItemServico itemServico = new ItemServico
                {
                    IdServico = ServicoSelecionado.Id,
                    Nome = ServicoSelecionado.NomeService,
                    Preco = ServicoSelecionado.Preco,
                    Quantidade = QuantidadeServicos,
                    Total = TotalServicos,
                };
                itemServicos.Add(itemServico);
                ApresentacaoItems.Add(new ServicoPresentation
                {
                    Nome = itemServico.Nome,
                    Preco = itemServico.Preco,
                    Quantidade = itemServico.Quantidade,
                    Total = TotalServicos,
                });

                Amount += TotalServicos;
                AtualizarTotal();
                ServicoSelecionado = null;
                QuantidadeServicos = 1;
                TotalServicos = 00.00m;
                IsLoadedDesc = true;
                IsLoadedItemServico = false;
            }
        }

        private async Task InsertProdutosOnItemAtendimento()
        {
            if(ProdutoSelecionado != null)
            {
                ItemAtendimento itemAtendimento = new ItemAtendimento
                {
                    Quantidade = QuantidadeProdutos,
                    Nome = ProdutoSelecionado.NomePruduto,
                    Preco = ProdutoSelecionado.Preco,
                    Total = TotalProdutos,
                    IdProduto = ProdutoSelecionado.Id
                };
                itemAtendimentos.Add(itemAtendimento);
                ApresentacaoItems.Add(new ServicoPresentation
                {
                  Nome = itemAtendimento.Nome,
                  Preco = itemAtendimento.Preco,
                  Total = TotalProdutos,
                  Quantidade = itemAtendimento.Quantidade
                });

                Amount += TotalProdutos;
                AtualizarTotal();
                ProdutoSelecionado = null;
                QuantidadeProdutos = 1;
                TotalProdutos = 00.00m;
                IsLoadedDesc = true;
                IsLoadedItemProduto = false;
            }
        }

        #endregion
    }
}
