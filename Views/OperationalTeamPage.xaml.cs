using UndacApp.ViewModels;

namespace UndacApp.Views;

public partial class OperationalTeamPage : ContentPage
{
	public OperationalTeamPage()
	{
		InitializeComponent();


		BindingContext = new OperationalTeamViewModel();
	}
}