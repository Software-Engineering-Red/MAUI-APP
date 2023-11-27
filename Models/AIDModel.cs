using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UndacApp.Models
{
	public abstract class AIDModel : INotifyPropertyChanged
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }


		public event PropertyChangedEventHandler? PropertyChanged;

		protected void OnPropertyChanged(string propertyName) =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

		protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
		{
			if (EqualityComparer<T>.Default.Equals(field, value)) return false;
			field = value;
			OnPropertyChanged(propertyName);
			return true;
		}
	}
}
