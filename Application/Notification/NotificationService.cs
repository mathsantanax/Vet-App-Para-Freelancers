using CommunityToolkit.Mvvm.ComponentModel;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Vet_App_For_Freelancers.Data;
using Vet_App_For_Freelancers.DataAccess;

namespace Vet_App_For_Freelancers.Notification
{
    public class NotificationService
    {
        private readonly SQLiteConnection _connection;
        private readonly ProxVacinacaoAtendimentoDataAccess _dataAccess;
        private readonly PetDataAccess _petDataAccess;
        private CancellationTokenSource _cancellationTokenSource;

        public NotificationService()
        {
            _connection = DatabaseConfig.GetConnection();
            _dataAccess = new ProxVacinacaoAtendimentoDataAccess(_connection);
            _petDataAccess = new PetDataAccess(_connection);

            Task.Run(async () => await VerificarPermissao());
        }

        // Verifica se as notificações estão habilitadas
        public async Task<bool> AreNotificationsEnabled()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.PostNotifications>();
            return status == PermissionStatus.Granted;
        }

        // Método para verificar a permissão de notificações
        public async Task<bool> VerificarPermissao()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.PostNotifications>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.PostNotifications>();
                status = PermissionStatus.Denied;
                Debug.WriteLine($"a permissão do aplicativo está {status}");
            }
            EnableNotifications();
            return status == PermissionStatus.Granted;
        }

        // inicia a verificação
        public void EnableNotifications()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            Task.Run(async () =>
            {
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    VerificarVacinasProximas();
                    await Task.Delay(TimeSpan.FromHours(5), _cancellationTokenSource.Token);
                }
            }, _cancellationTokenSource.Token);
        }

        // para a verificação de vacinas
        public void DisableNotifications()
        {
            _cancellationTokenSource?.Cancel();
        }

        // verifica as próximas vacinas se está dentro de 2 dias e gera a notificação
        private async void VerificarVacinasProximas()
        {
            DateTime notifyTime = DateTime.Now.AddHours(12);

            // Garante que o horário da notificação sempre seja no futuro
            if (notifyTime <= DateTime.Now)
            {
                notifyTime = DateTime.Now.AddMinutes(1);
            }

            try
            {
                var vacinasProximas = _dataAccess.GetTwoDaysVacinas();

                foreach (var vacina in vacinasProximas)
                {
                    try
                    {
                        vacina.Pet = _petDataAccess.GetById(vacina.IdPet);
                        if (vacina.Pet == null)
                            continue;
                        Debug.WriteLine($"Nome do pet{vacina.Pet.NomePet}");

                        if(vacina.DataProxima.Date <= DateTime.Now.Date.AddDays(2))
                        {
                            var notification = new NotificationRequest
                            {
                                NotificationId = vacina.IdPet,
                                Title = "Vacinação Próxima",
                                Description = $"A vacinação do pet {vacina.Pet.NomePet} está marcada para {vacina.DataProxima:dd/MM/yyyy}.",
                                Schedule = new NotificationRequestSchedule
                                {
                                    NotifyTime = notifyTime,
                                    NotifyRepeatInterval = TimeSpan.FromHours(10),
                                    RepeatType = NotificationRepeat.TimeInterval,
                                },
                                Android = new AndroidOptions
                                {
                                    AutoCancel = true,
                                    IconSmallName = { ResourceName = "pata.png" },
                                    VibrationPattern = new long[] { 0, 500, 200, 500 }
                                },
                                ReturningData = vacina.IdPet.ToString()
                            };
                            try
                            {
                                await LocalNotificationCenter.Current.Show(notification);
                                Debug.WriteLine($"📢 Notificação agendada para {notifyTime}");
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"Erro ao exibir notificação: {ex.Message}");
                            }
                        }
                        else if(vacina.DataProxima.Date == DateTime.Now.Date)
                        {
                            var notification = new NotificationRequest
                            {
                                NotificationId = vacina.IdPet,
                                Title = "Vacinação Próxima",
                                Description = $"A vacinação do pet {vacina.Pet.NomePet} está marcada para Hoje {vacina.DataProxima:dd/MM/yyyy}.",
                                Schedule = new NotificationRequestSchedule
                                {
                                    NotifyTime = DateTime.Now.AddHours(5),
                                    NotifyRepeatInterval = TimeSpan.FromHours(10),
                                    RepeatType = NotificationRepeat.TimeInterval
                                },
                                Android = new AndroidOptions
                                {
                                    AutoCancel = true,
                                    IconSmallName = { ResourceName = "pata.png" },
                                    VibrationPattern = new long[] { 0, 500, 200, 500 }
                                },
                                ReturningData = vacina.IdPet.ToString()
                            };
                            try
                            {
                                await LocalNotificationCenter.Current.Show(notification);
                                Debug.WriteLine($"📢 Notificação agendada para {notifyTime}");
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"Erro ao exibir notificação: {ex.Message}");
                            }
                        }
                    }
                    catch (Exception innerEx)
                    {
                        Debug.WriteLine($"Erro ao processar vacina {vacina.IdPet}: {innerEx.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao verificar vacinas próximas: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"Detalhes da exceção: {ex.InnerException.Message}");
                }
            }
        }
    }
}
