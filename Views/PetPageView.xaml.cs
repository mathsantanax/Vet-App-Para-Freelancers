using Vet_App_For_Freelancers.Models.ModelPetETutor;
using Vet_App_For_Freelancers.ViewModels;

namespace Vet_App_For_Freelancers.Views;

public partial class PetPageView : ContentPage
{
	public PetPageView(Pet pet)
	{
		InitializeComponent();
		BindingContext = new PetPageViewModel(pet);
	}
}