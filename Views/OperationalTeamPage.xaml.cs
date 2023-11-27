using UndacApp.Models;
using UndacApp.Services;
using System.Collections.ObjectModel;
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