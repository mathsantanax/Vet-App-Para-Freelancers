using Vet_App_For_Freelancers.Models.ModelPetETutor;
using Vet_App_For_Freelancers.ViewModels;

namespace Vet_App_For_Freelancers.Views;

public partial class PetPageView : ContentPage
{
	public PetPageView(Pet pet, Tutor tutor)
	{
		InitializeComponent();
		BindingContext = new PetPageViewModel(pet, tutor);
	}
}