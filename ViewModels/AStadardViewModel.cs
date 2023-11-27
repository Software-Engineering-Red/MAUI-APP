using System.ComponentModel;
using System.Windows.Input;

namespace UndacApp.ViewModels
{
	public abstract class AStandardViewModel : INotifyPropertyChanged

	{

		public ICommand SaveCommand => new Command(Save);
		public ICommand DeleteCommand => new Command(Delete);
		protected abstract void Save();
		protected abstract void Delete();

		protected abstract Task LoadData();

		protected abstract void add();

		protected abstract void update();
		protected abstract void resetValues();

		protected abstract void remove();

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public event PropertyChangedEventHandler? PropertyChanged;
	}
}
