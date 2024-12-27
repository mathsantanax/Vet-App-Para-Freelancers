using Vet_App_For_Freelancers.ViewModels;

namespace Vet_App_For_Freelancers.Views;

public partial class FinalizarNovoServicoPageView : ContentPage
{
	public FinalizarNovoServicoPageView()
	{
		InitializeComponent();
		BindingContext = new FinalizarNovoServicoModelView();
	}
}