using Vet_App_For_Freelancers.ViewModels;

namespace Vet_App_For_Freelancers.Views;

public partial class RelatorioPageView : ContentPage
{
	public RelatorioPageView()
	{
		InitializeComponent();
		BindingContext = new RelatorioViewModel();
	}
}