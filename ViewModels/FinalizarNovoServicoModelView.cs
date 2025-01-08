using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
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

namespace Vet_App_For_Freelancers.ViewModels
{
    public partial class FinalizarNovoServicoModelView : ObservableObject
    {
        SQLiteConnection _connection = DatabaseConfig.GetConnection();

        private readonly AtendimentoDataAccess _atendimentoDataAccess;
        private readonly ProdutosAtendimentoDataAccess _produtosAtendimentoDataAccess;
        private readonly ServicosAtendimentoDataAccess _servicosAtendimentoDataAccess;
        private readonly ProxVacinacaoAtendimentoDataAccess _proxVacinacaoAtendimentoDataAccess;
        private readonly PetDataAccess _petDataAccess;

        private readonly PagamentoDataAccess _pagamentoDataAccess;

        private Atendimento Atendimento;

        [ObservableProperty]
        public Tutor tutorView;

        [ObservableProperty]
        public Pet petView;

        [ObservableProperty]
        private decimal amout;

        private Pagamento pagamentoSelecionado;
        public Pagamento PagamentoSelecionado
        {
            get => pagamentoSelecionado;
            set
            {
                SetProperty(ref pagamentoSelecionado, value);
            }
        }

        public ObservableCollection<ItemServico> itemServicos { get; set; }
        public ObservableCollection<ItemAtendimento> itemAtendimentos { get; set; }
        public ObservableCollection<ServicoPresentation> ApresentacaoItems { get; set; }

        public ObservableCollection<Pagamento> Pagamentos { get; set; }

        private decimal _Desconto;

        public decimal Desconto
        {
            get => _Desconto;
            set
            {
                SetProperty(ref _Desconto, value);
                if (Desconto > 0)
                {
                    DescontoLoaded = true;
                }
            }
        }
        private bool _IsLoadedData = false;

        public bool IsLoadedData
        {
            get => _IsLoadedData;
            set => SetProperty(ref _IsLoadedData, value);
        }
        public decimal TotalAmout
        {
            get => Amout - Desconto;
        }

        private bool _DescontoLoaded = false;

        public bool DescontoLoaded
        {
            get => _DescontoLoaded;
            set => SetProperty(ref _DescontoLoaded, value);
        }

        private bool _ItensLoaded = false;
        public bool ItensLoaded
        {
            get => _ItensLoaded;
            set => SetProperty(ref _ItensLoaded, value);
        }

        private bool _IsLoaded = false;
        public bool Isloaded
        {
            get => _IsLoaded;
            set => SetProperty(ref _IsLoaded, value);
        }
        private DateTime _Date;
        public DateTime Date
        {
            get => _Date;
            set
            {
                SetProperty(ref _Date, value);
            }
        }

        private bool FezVacinacao;

        public ICommand FinalizarCommand { get; }
        public ICommand BackCommand { get; }
        public FinalizarNovoServicoModelView(Tutor tutor, Pet pet, decimal amout, decimal desconto, ObservableCollection<ItemAtendimento> atendimentosItem, ObservableCollection<ItemServico> servicosItem)
        {
            _pagamentoDataAccess = new PagamentoDataAccess(_connection);
            _atendimentoDataAccess = new AtendimentoDataAccess(_connection);
            _produtosAtendimentoDataAccess = new ProdutosAtendimentoDataAccess(_connection);
            _servicosAtendimentoDataAccess = new ServicosAtendimentoDataAccess(_connection);
            _proxVacinacaoAtendimentoDataAccess = new ProxVacinacaoAtendimentoDataAccess(_connection);
            _petDataAccess = new PetDataAccess(_connection);

            ApresentacaoItems = new ObservableCollection<ServicoPresentation>();
            Pagamentos = new ObservableCollection<Pagamento>();
            BackCommand = new Command<object>(GoBack);
            FinalizarCommand = new Command(async () => await FinalizarAsync());
            Atendimento = new Atendimento();

            tutorView = tutor;
            petView = pet;
            Desconto = desconto;
            Amout = amout;
            itemAtendimentos = atendimentosItem;
            itemServicos = servicosItem;

            Date = DateTime.Today;

            Task.Run(async () => await InitializeAsync()).ContinueWith(t =>
            {
                if (t.Exception != null)
                {
                    Debug.WriteLine($"Erro na inicialização: {t.Exception.Message}");
                }
            });

        }


        private async Task InitializeAsync()
        {
            await PresentationAsync();
            await GetPagamento();
        }

        private async Task GetPagamento()
        {
            await Task.Delay(100);
            try
            {
                Pagamentos.Clear();
                var pagamentos = _pagamentoDataAccess.GetAll();
                if (pagamentos != null && pagamentos.Any())
                {
                    foreach (var pagamento in pagamentos)
                    {
                        Pagamentos.Add(pagamento);
                    }
                    Debug.WriteLine($"Total de pagamentos carregados: {Pagamentos.Count}");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", $"Erro ao carregar pagamentos: {ex.Message}", "OK");
                Debug.WriteLine(ex.Message);
            }
        }

        private async Task PresentationAsync()
        {
            await Task.Delay(100);
            try
            {
                foreach (var servico in itemServicos)
                {
                    if (string.Equals(servico.Nome, "VACINAÇÃO", StringComparison.OrdinalIgnoreCase))
                    {
                        IsLoadedData = true;
                        FezVacinacao = true;
                    }
                    ApresentacaoItems.Add(new ServicoPresentation
                    {
                        Nome = servico.Nome,
                        Preco = servico.Preco,
                        Quantidade = servico.Quantidade,
                        Total = servico.Total,
                    });
                }
                foreach (var produto in itemAtendimentos)
                {
                    ApresentacaoItems.Add(new ServicoPresentation
                    {
                        Nome = produto.Nome,
                        Quantidade = produto.Quantidade,
                        Preco = produto.Preco,
                        Total = produto.Total
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao carregar serviços: {ex.Message}");
            }
            finally
            {
                ItensLoaded = true;
            }
        }

        private async void GoBack(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();

        }

        private async Task FinalizarAsync()
        {
            await Task.Delay(100);
            try
            {

                if (CadastrarAtendimento())
                {
                    CadastrarItensDeServico();
                    CadastrarItensDeProdutos();
                    AtualizarNumeroMicrochip();
                    if (FezVacinacao == true)
                    {
                        CadastrarProximaVacinacao();
                    }
                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Venda Finalizada", "Ok");
                    WeakReferenceMessenger.Default.Send(new PetMessage(PetView.Id));
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Alerta", "Houve um Problema.", "Ok");
                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Alerta", $"{ex.Message}", "OK");
            }
        }

        private void AtualizarNumeroMicrochip()
        {
            if(PetView.NumeroMicrochip != null && PetView.NumeroMicrochip != "")
            {
                _petDataAccess.Update(PetView);
            }
        }

        private bool CadastrarAtendimento()
        {
            // Verificando se o método de pagamento foi selecionado
            if (string.IsNullOrEmpty(PagamentoSelecionado?.MetodoPagamento))
            {
                // Caso o método de pagamento não tenha sido selecionado
                Application.Current.MainPage.DisplayAlert("Alerta", "Selecione a Forma de Pagamento", "OK");
                return false;
            }

            // Caso o método de pagamento tenha sido selecionado
            Atendimento.Data = DateTime.Now.Date;
            Atendimento.Desconto = Desconto;
            Atendimento.FezVacinacao = FezVacinacao;
            Atendimento.ValorTotal = TotalAmout;
            Atendimento.IdPagamento = PagamentoSelecionado.Id;
            Atendimento.IdTutor = TutorView.Id;
            Atendimento.IdPet = PetView.Id;

            try
            {
                // Inserir atendimento no banco de dados
                _atendimentoDataAccess.Insert(Atendimento);
                return true;
            }
            catch (Exception ex)
            {
                // Caso ocorra um erro durante a inserção
                Debug.WriteLine(ex.Message);
                Application.Current.MainPage.DisplayAlert("Erro", "Erro ao cadastrar o atendimento", "OK");
                return false;
            }

        }

        private void CadastrarProximaVacinacao()
        {
            try
            {
                if(Date != DateTime.Now)
                {
                    _proxVacinacaoAtendimentoDataAccess.Insert(new Vacinacao
                    {
                        DataProxima = Date,
                        IdPet = PetView.Id,
                        IdAtendimento = Atendimento.Id,
                    }); 
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void CadastrarItensDeProdutos()
        {
            try
            {
                foreach (var produto in itemAtendimentos)
                {
                    _produtosAtendimentoDataAccess.Insert(new ItemAtendimento
                    {
                        Nome = produto.Nome,
                        Quantidade = produto.Quantidade,
                        Preco = produto.Preco,
                        Total = produto.Total,
                        IdProduto = produto.IdProduto,
                        IdAtendimento = Atendimento.Id
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void CadastrarItensDeServico()
        {
            try
            {
                foreach (var servico in itemServicos)
                {
                    _servicosAtendimentoDataAccess.Insert(
                        new ItemServico
                        {
                            Nome = servico.Nome,
                            Quantidade = servico.Quantidade,
                            Preco = servico.Preco,
                            Total = servico.Total,
                            IdAtendimento = Atendimento.Id,
                            IdServico = servico.IdServico
                        });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}