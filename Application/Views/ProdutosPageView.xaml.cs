using Vet_App_For_Freelancers.ViewModels;

namespace Vet_App_For_Freelancers.Views;

public partial class ProdutosPageView : ContentPage
{
	public ProdutosPageView()
	{
		InitializeComponent();
		BindingContext = new ProdutosViewModel();
	}
    async void OnItemTapped(object sender, EventArgs e)
    {
        var frame = (Frame)sender;
        var originalColor = Colors.White;
        frame.BackgroundColor = Colors.LightBlue;
        await Task.Delay(500);
        frame.BackgroundColor = originalColor;
    }
}