

namespace UndacApp.Models
{
	public class Person : AModel
	{
		private string name;
		public string Name
		{
			get => name;
			set => SetField(ref name, value);
		}
		private string fieldname;
		public string Fieldname
		{
			get => fieldname;
			set => SetField(ref fieldname, value);
		}
	}
}
