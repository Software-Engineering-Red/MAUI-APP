namespace UndacApp.Models
{
	public class Person : AModel
	{
		private string? fieldname = String.Empty;
		public string? Fieldname
		{
			get => fieldname;
			set => SetField(ref fieldname, value);
		}
	}
}
