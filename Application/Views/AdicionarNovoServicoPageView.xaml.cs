using Vet_App_For_Freelancers.Models.ModelPetETutor;
using Vet_App_For_Freelancers.ViewModels;

namespace Vet_App_For_Freelancers.Views;

public partial class AdicionarNovoServicoPageView : ContentPage
{
	public AdicionarNovoServicoPageView(Tutor tutor, Pet pet)
	{
		InitializeComponent();
		BindingContext = new AdicionarServicoViewModel(tutor, pet);
	}
}