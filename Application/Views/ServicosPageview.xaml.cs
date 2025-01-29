using Vet_App_For_Freelancers.ViewModels;

namespace Vet_App_For_Freelancers.Views;

public partial class ServicosPageview : ContentPage
{
	public ServicosPageview()
	{
		InitializeComponent();
		BindingContext = new ServicosModelView();
	}
    async void OnItemTapped(object sender, EventArgs e)
    {
        var frame = (Frame)sender;
        var originalColor = Colors.LightGray;
        frame.BackgroundColor = Colors.LightBlue;
        await Task.Delay(200);
        frame.BackgroundColor = originalColor;
    }
}