using Vet_App_For_Freelancers.ViewModels;

namespace Vet_App_For_Freelancers.Views;

public partial class ProdutosPageView : ContentPage
{
	public ProdutosPageView()
	{
		InitializeComponent();
		BindingContext = new ProdutosViewModel();
	}
}