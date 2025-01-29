using System.Collections.ObjectModel;
using Vet_App_For_Freelancers.Models.ModelPetETutor;
using Vet_App_For_Freelancers.Models.ModelServicos;
using Vet_App_For_Freelancers.ViewModels;

namespace Vet_App_For_Freelancers.Views;

public partial class FinalizarNovoServicoPageView : ContentPage
{
	public FinalizarNovoServicoPageView(Tutor tutor, Pet pet, decimal amout, decimal desconto, ObservableCollection<ItemAtendimento> itemAtendimentos, ObservableCollection<ItemServico> servicosItem)
	{
		InitializeComponent();
		BindingContext = new FinalizarNovoServicoModelView(tutor, pet, amout, desconto, itemAtendimentos, servicosItem);
	}
}