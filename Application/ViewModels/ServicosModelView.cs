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
using Vet_App_For_Freelancers.Models.ModelServicos;

namespace Vet_App_For_Freelancers.ViewModels
{
    public partial class ServicosModelView : ObservableObject
    {
        SQLiteConnection connection = DatabaseConfig.GetConnection();

        private readonly ServicoDataAccess servicoDataAccess;

        [ObservableProperty]
        private ObservableCollection<Servico> listServicos;

        private int id;

        [ObservableProperty]
        private string? nome;

        [ObservableProperty]
        private decimal preco;

        private bool _IsVisibleButtonAlterar = false;

        public bool IsVisibleButtonAlterar
        {
            get => _IsVisibleButtonAlterar;
            set => SetProperty(ref _IsVisibleButtonAlterar, value);
        }

        public bool _IsVisibleButtonCadastrar = true;

        public bool IsVisibleButtonCadastrar
        {
            get => _IsVisibleButtonCadastrar;
            set => SetProperty(ref _IsVisibleButtonCadastrar, value);
        }
        private Servico? servicoSelected;

        public Servico? ServicoSelected
        {
            get => servicoSelected;
            set 
            {
                if (SetProperty(ref servicoSelected, value))
                {
                    AdicionarServicoCommand.Execute(servicoSelected);
                }
            } 
        }

        public ICommand AdicionarServicoCommand { get; }
        public ICommand AlterarServicoCommand { get; }
        public ICommand ServicoTapCommand { get; }

        public ServicosModelView()
        {
            servicoDataAccess = new ServicoDataAccess(connection);

            listServicos = new ObservableCollection<Servico>();

            AdicionarServicoCommand = new RelayCommand(async() => await AddServico());
            ServicoTapCommand = new Command<Servico>(OnItemSelected);
            AlterarServicoCommand = new RelayCommand(async () => await AlterarServico());

            Task.Run(async () => await InitializeAsync());
        }


        // inicio assincrono
        public async Task InitializeAsync()
        {
            await GetServicos();
        }
        // selecionar serviço
        private async void OnItemSelected(Servico selectedServico)
        {
            try
            {
                if (selectedServico != null)
                {
                    id = selectedServico.Id;
                    Nome = selectedServico.NomeService;
                    Preco = selectedServico.Preco;
                }
            }
            catch (Exception ex)
            {
                await Application.Current!.MainPage!.DisplayAlert("Erro", $"Erro ao selecionar o serviço. \n {ex.Message}", "Ok");
            }
            finally
            {
                IsVisibleButtonCadastrar = false;
                IsVisibleButtonAlterar = true;
            }
        }
        // carregar serviços
        public async Task GetServicos()
        {
            try
            {
                await Task.Delay(100);
                ListServicos.Clear();
                var getServicos = servicoDataAccess.GetAll();
                if (getServicos.Any())
                {
                    foreach(var Servicos in getServicos)
                    {
                        ListServicos.Add(Servicos);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        // alterar serviços
        private async Task AlterarServico()
        {
            try
            {
                servicoDataAccess.Update(new Servico {
                    Id = id,
                    NomeService = Nome!,
                    Preco = Preco
                });
                await Application.Current!.MainPage!.DisplayAlert("Sucesso", "Alterado com Sucesso", "Ok");
            }
            catch(Exception ex)
            {
                await Application.Current!.MainPage!.DisplayAlert("Erro", $"{ex.Message}", "Ok");
            }
            finally
            {
                await GetServicos();
                IsVisibleButtonCadastrar = true;
                IsVisibleButtonAlterar = false;
                id = 0;
                Preco = 0;
                Nome = "";
            }
        }
        // adicionar serviço
        private async Task AddServico()
        {
            try
            {
                if(Nome != null)
                {
                    if(Preco != 0)
                    {
                        servicoDataAccess.Insert(new Servico
                        {
                            NomeService = Nome.ToUpper(),
                            Preco = Preco
                        });
                        await Application.Current!.MainPage!.DisplayAlert("Sucesso", "Cadastrado com Sucesso", "Ok");
                    }
                    else
                    {
                        await Application.Current!.MainPage!.DisplayAlert("Erro", $"O preço não pode ser igual a 0. {Preco}", "Ok");
                    }
                }
                else
                {
                    await Application.Current!.MainPage!.DisplayAlert("Erro", $"O Serviço não pode estar vazio {Nome}", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current!.MainPage!.DisplayAlert("Erro",$"{ex.Message}","Ok");
            }
            finally
            {
                await GetServicos();
                Preco = 0;
                Nome = "";
            }
        }
    }
}
