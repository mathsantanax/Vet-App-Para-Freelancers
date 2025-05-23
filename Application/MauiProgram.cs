﻿using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;
using Vet_App_For_Freelancers.Data;
using Vet_App_For_Freelancers.DataAccess;
using Vet_App_For_Freelancers.Models.ModelPetETutor;
using Vet_App_For_Freelancers.Models.ModelServicos;
using Vet_App_For_Freelancers.Notification;
using Vet_App_For_Freelancers.ViewModels;
using Vet_App_For_Freelancers.Views;

namespace Vet_App_For_Freelancers
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseLocalNotification()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Material-Icon.ttf", "MaterialIcon");
                });

            // Registrando as dependências para a injeção
            builder.Services.AddSingleton<DatabaseConfig>();
            builder.Services.AddSingleton<NotificationService>();

            builder.Services.AddTransient<ConfigModelView>();

            builder.Services.AddTransient<Tutor>();
            builder.Services.AddTransient<Pet>();
            builder.Services.AddTransient<Raca>();
            builder.Services.AddTransient<Especie>();

            builder.Services.AddTransient<Atendimento>();
            builder.Services.AddTransient<ItemAtendimento>();
            builder.Services.AddTransient<ItemServico>();
            builder.Services.AddTransient<Lab>();
            builder.Services.AddTransient<Pagamento>();
            builder.Services.AddTransient<Produto>();
            builder.Services.AddTransient<Servico>();
            builder.Services.AddTransient<Vacinacao>();

            builder.Services.AddSingleton<TutorDataAccess>();
            builder.Services.AddSingleton<RacaDataAccess>();
            builder.Services.AddSingleton<PetDataAccess>();
            builder.Services.AddSingleton<EspecieDataAccess>();
            builder.Services.AddSingleton<ConfigModelView>();

            // Registrando os outros serviços e páginas
            builder.Services.AddTransient<ConfiguracoesPageView>();
#if DEBUG
            builder.Logging.AddDebug();
#endif  
            return builder.Build();
        }
    }
}
