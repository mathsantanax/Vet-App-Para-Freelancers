using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vet_App_For_Freelancers.Data;
using Vet_App_For_Freelancers.DataAccess;
using Vet_App_For_Freelancers.Models.ModelServicos;

namespace Vet_App_For_Freelancers.ViewModels
{
    public partial class RelatorioViewModel : ObservableObject
    {
        SQLiteConnection _connection = DatabaseConfig.GetConnection();
        private readonly AtendimentoDataAccess _atendimentoDataAccess;
        private readonly PetDataAccess _petDataAccess;
        private readonly TutorDataAccess _tutorDataAccess;

        [ObservableProperty]
        private bool isLoaded = false;

        [ObservableProperty]
        private bool itensIsVisible = false;

        [ObservableProperty]
        private string? mesAnoAtual;

        [ObservableProperty]
        private string? mesAtual;

        [ObservableProperty]
        private decimal totalMes;

        [ObservableProperty]
        private ObservableCollection<Atendimento> atendimentoCollection;

        private DateTime dataAtual;

        public ICommand AvancarCommand { get; }
        public ICommand VoltarCommand { get; }
        public RelatorioViewModel()
        {
            _atendimentoDataAccess = new AtendimentoDataAccess(_connection);
            _petDataAccess = new PetDataAccess(_connection);
            _tutorDataAccess = new TutorDataAccess(_connection);
            atendimentoCollection = new ObservableCollection<Atendimento>();

            AvancarCommand = new Command(async() => await AvancarMes());
            VoltarCommand = new Command(async () => await VoltarMes());

            dataAtual = DateTime.Now;
            AtualizarData(dataAtual);
            AtulizarDataMes(dataAtual);

            Task.Run(async () => await InitializeAsync());
        }
        // inicio assincrono
        private async Task InitializeAsync()
        {
            try
            {
                await CarregarDadosAsync();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                await Task.Delay(300);
                IsLoaded = true;
            }
        }
        // carregar atendimento
        private async Task CarregarDadosAsync()
        {
            try
            {
                AtendimentoCollection.Clear();
                var atendimentos = new ObservableCollection<Atendimento>();
                TotalMes = 0;
                var atendimentoMes = _atendimentoDataAccess.GetAtendimentosByDate(dataAtual);
                foreach (var a in atendimentoMes)
                {
                    TotalMes += a.ValorTotal;
                    a.Pet = _petDataAccess.GetById(a.IdPet);
                    a.Tutor = _tutorDataAccess.GetById(a.IdTutor);
                    atendimentos.Add(a);
                }
                var sortedAtendimentos = atendimentos.OrderByDescending(a => a.Data).ToList();
                foreach (var atendimento in sortedAtendimentos)
                {
                    AtendimentoCollection.Add(atendimento);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                await Task.Delay(200);
                ItensIsVisible = true;
            }
        }
        // avaçar mes
        private async Task AvancarMes()
        {
            dataAtual = dataAtual.AddMonths(1);

            AtualizarData(dataAtual);
            AtulizarDataMes(dataAtual);

            await CarregarDadosAsync();
        }
        // voltar mes
        private async Task VoltarMes()
        {
            dataAtual = dataAtual.AddMonths(-1);

            AtualizarData(dataAtual);
            AtulizarDataMes(dataAtual);

            await CarregarDadosAsync();
        }
        // atualizar data 
        private void AtualizarData(DateTime data)
        {
            MesAnoAtual = data.ToString("MMMM yyyy", CultureInfo.CurrentCulture);
        }
        // atualizar mes
        private void AtulizarDataMes(DateTime data)
        {
            MesAtual = data.ToString("MMMM", CultureInfo.CurrentCulture);
        }
    }
}
