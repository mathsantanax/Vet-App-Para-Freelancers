using Vet_App_For_Freelancers.Views;

namespace Vet_App_For_Freelancers
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Registrando Rota no Shell
            Routing.RegisterRoute("petpageview", typeof(PetPageView));
        }
    }
}
