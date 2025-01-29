using Vet_App_For_Freelancers.ViewModels;

namespace Vet_App_For_Freelancers.Views;

public partial class ConfiguracoesPageView : ContentPage
{
	public ConfiguracoesPageView()
	{
		InitializeComponent();
		BindingContext = new ConfigModelView();
	}
}