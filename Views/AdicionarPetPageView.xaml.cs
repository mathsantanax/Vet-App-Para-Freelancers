using Vet_App_For_Freelancers.Models.ModelPetETutor;
using Vet_App_For_Freelancers.ViewModels;

namespace Vet_App_For_Freelancers.Views;

public partial class AdicionarPetPageView : ContentPage
{
	public AdicionarPetPageView(Tutor tutor)
	{
		InitializeComponent();
		BindingContext = new AdicionarPetViewModel(tutor);
	}
}