using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using UndacApp.Models;
using UndacApp.Services;

namespace UndacApp.ViewModels
{
	internal interface IStadardViewModel : INotifyPropertyChanged
	{

		void Save();
		void Delete();
		Task LoadData();
	}
}
