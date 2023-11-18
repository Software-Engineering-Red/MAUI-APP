using SQLite;
using System.ComponentModel;

namespace UndacApp.Models
{
	public abstract class ANameModel : INotifyPropertyChanged
	{
		private string name;

		[PrimaryKey]
		public string Name
		{
			get => name;
			set
			{
				if (name != value)
				{
					name = value;
					OnPropertyChanged(nameof(Name));
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
