using Vet_App_For_Freelancers.ViewModels;

namespace Vet_App_For_Freelancers.Views;

public partial class HomePageView : ContentPage
{
	public HomePageView()
	{
		InitializeComponent();
        BindingContext = new HomePageViewModel();
    }
    async void OnItemTapped(object sender, EventArgs e)
    {
        var frame = (Frame)sender;
        var originalColor = Colors.White;
        frame.BackgroundColor = Color.FromArgb("#D3D3D3");
        await Task.Delay(200);
        frame.BackgroundColor = originalColor;
    }
}