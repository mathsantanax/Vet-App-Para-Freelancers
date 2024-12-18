using Microsoft.Extensions.Logging;
using SQLite;
using Vet_App_For_Freelancers.Data;
using Vet_App_For_Freelancers.DataAccess;
using Vet_App_For_Freelancers.Models.ModelPetETutor;
using Vet_App_For_Freelancers.Models.ModelServicos;

namespace Vet_App_For_Freelancers
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Material-Icon.ttf", "MaterialIcon");
                });

            builder.Services.AddSingleton<DatabaseConfig>();

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

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
