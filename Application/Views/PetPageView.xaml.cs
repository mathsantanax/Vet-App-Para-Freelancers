using Vet_App_For_Freelancers.Models.ModelPetETutor;
using Vet_App_For_Freelancers.ViewModels;

namespace Vet_App_For_Freelancers.Views;

[QueryProperty(nameof(PetId), "petId")]
public partial class PetPageView : ContentPage
{
    public int PetId { get; set; }
    public PetPageView()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (PetId > 0)
        {
            BindingContext = new PetPageViewModel(PetId);
        }
    }
}