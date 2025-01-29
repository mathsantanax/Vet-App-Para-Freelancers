using Vet_App_For_Freelancers.Views;

namespace Vet_App_For_Freelancers
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("PetPageView", typeof(PetPageView));
        }
    }
}
