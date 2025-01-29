using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Vet_App_For_Freelancers.Data;
using Vet_App_For_Freelancers.DataAccess;

namespace Vet_App_For_Freelancers.Notification
{
    public class NotificationService
    {
        //SQLiteConnection _connection = DatabaseConfig.GetConnection();
        //private readonly ProxVacinacaoAtendimentoDataAccess _dataAccess;
        //private readonly PetDataAccess _petDataAccess;

        //public NotificationService()
        //{
        //    _dataAccess = new ProxVacinacaoAtendimentoDataAccess(_connection);
        //    _petDataAccess = new PetDataAccess(_connection);

        //    Task.Run(async () => await VerificarPermissao());
        //}

        //private CancellationTokenSource _cancellationTokenSource;
        //private bool _isCheckingVacinas = false;
        //private bool _verificouPermissao = false;

        //public async Task<bool> AreNotificationsEnabled()
        //{
        //    var status = await Permissions.CheckStatusAsync<Permissions.PostNotifications>();
        //    if (status != PermissionStatus.Granted)
        //    {
        //        return _isCheckingVacinas;
        //    }
        //    else
        //    {
        //        return _isCheckingVacinas;
        //    }
        //}

        //public void EnableNotifications()
        //{
        //    if (!_isCheckingVacinas)
        //    {
        //        IniciarVerificacaoDeVacinas();
        //    }
        //}

        //public void DisableNotifications()
        //{
        //    if (_isCheckingVacinas)
        //    {
        //        PararVerificacaoDeVacinas();
        //    }
        //}

        //public void IniciarVerificacaoDeVacinas()
        //{
        //    _cancellationTokenSource = new CancellationTokenSource();

        //    Task.Run(async () =>
        //    {
        //        while (!_cancellationTokenSource.Token.IsCancellationRequested)
        //        {
        //            VerificarVacinasProximas();
        //            await Task.Delay(TimeSpan.FromHours(1), _cancellationTokenSource.Token); // Verifica a cada 1 hora
        //        }
        //    }, _cancellationTokenSource.Token);
        //}

        //public void PararVerificacaoDeVacinas()
        //{
        //    _cancellationTokenSource?.Cancel();

        //}

        //private async Task VerificarPermissao()
        //{
        //    var status = await Permissions.CheckStatusAsync<Permissions.PostNotifications>();
        //    if (status != PermissionStatus.Granted)
        //    {
        //        status = await Permissions.RequestAsync<Permissions.PostNotifications>();
        //    }

        //    _verificouPermissao = status == PermissionStatus.Granted;

        //    if (_verificouPermissao)
        //    {
        //        IniciarVerificacaoDeVacinas();
        //    }
        //}

        //private void VerificarVacinasProximas()
        //{
        //    var vacinasProximas = _dataAccess.GetTwoDaysVacinas();
        //    try
        //    {
        //        foreach (var vacina in vacinasProximas)
        //        {
        //            vacina.Pet = _petDataAccess.GetById(vacina.IdPet);
        //            var notification = new NotificationRequest
        //            {
        //                Title = "Vacinação Próxima.",
        //                Description = $"A Vacinação do Pet {vacina.Pet.NomePet}\nEstá marcada para {vacina.DataProxima.ToString("dd MM YY")}",
        //                Schedule = new NotificationRequestSchedule
        //                {
        //                    NotifyTime = DateTime.Now.AddSeconds(5),
        //                },
        //                Android = new AndroidOptions
        //                {
        //                    AutoCancel = true,
        //                    IconSmallName = { ResourceName = "pata.png" },
        //                },
        //                ReturningData = vacina.IdPet.ToString()
        //            };
        //            LocalNotificationCenter.Current.Show(notification);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine($"Erro ao verificar vacinas próximas: {ex.Message}");
        //    }
        //}

    }
}
