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
using Vet_App_For_Freelancers.Presentation;

namespace Vet_App_For_Freelancers.ViewModels
{
    public partial class FinalizarNovoServicoModelView : ObservableObject
    {
        SQLiteConnection _connection = DatabaseConfig.GetConnection();

        private readonly LaboratorioDataAccess _laboratorioDataAccess;

        [ObservableProperty]
        public Tutor tutorView;

        [ObservableProperty]
        public Pet petView;

        [ObservableProperty]
        private decimal amout;


        public ObservableCollection<ItemServico> itemServicos { get; set; }
        public ObservableCollection<ItemAtendimento> itemAtendimentos { get; set; }
        public ObservableCollection<ServicoPresentation> ApresentacaoItems { get; set; }

        private decimal _Desconto;

        public decimal Desconto
        {
            get => _Desconto;
            set
            {
                SetProperty(ref _Desconto, value);
               if(Desconto > 0)
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
            set => SetProperty(ref _DescontoLoaded,value);
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
                MostrarData();
            } 
        }


        public ICommand BackCommand { get; }
        public FinalizarNovoServicoModelView(Tutor tutor, Pet pet, decimal amout, decimal desconto, ObservableCollection<ItemAtendimento> atendimentosItem, ObservableCollection<ItemServico> servicosItem) 
        {
            _laboratorioDataAccess = new LaboratorioDataAccess(_connection);
            ApresentacaoItems = new ObservableCollection<ServicoPresentation>();


            tutorView = tutor;
            petView = pet;
            Desconto = desconto;
            Amout = amout;
            itemAtendimentos = atendimentosItem;
            itemServicos = servicosItem;

            Date = DateTime.Today;

            Task.Run(async () => await InitializeAsync());

            BackCommand = new Command<object>(GoBack);
        }

        private async void GoBack(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();

        } 

        private async Task InitializeAsync()
        {
            await PresentationAsync();
        }

        private async void MostrarData()
        {
            await App.Current.MainPage.DisplayAlert("Alerta",$"{Date}", "Ok");
        }

        private async Task PresentationAsync()
        {
            await Task.Delay(500);
            try
            {
                foreach(var servico in itemServicos)
                {
                    if(servico.Nome == "VACINAÇÃO")
                    {
                        IsLoadedData = true;
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
            catch(Exception ex)
            {
                Debug.WriteLine($"Erro ao carregar serviços: {ex.Message}");
            }
            finally
            {
                ItensLoaded = true;
            }
        }

    }
}
