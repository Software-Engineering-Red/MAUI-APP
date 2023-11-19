using System.Runtime.CompilerServices;
using System.ComponentModel;
using SQLite;



namespace UndacApp.Models.Temp_Models
{
    [Table("Temp-Resource_Table")]
    class Temp_Resource : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int RId { get; set; }

        private string resourcename;
        public string Resourcename
        {
            get => resourcename;
            set => SetField(ref resourcename, value);
        }

        private string resourceType;
        public string ResourceType
        {
            get => resourceType;

            set => SetField(ref resourceType, value);
        }

        private int current_Stock;
        public int CurrentStock
        {
            get => current_Stock;

            set => SetField(ref  current_Stock, value);
        }

        private int reOrder_Level;
        public int ReOrder_Level
        {
            get => reOrder_Level;

            set => SetField(ref reOrder_Level, value);
        }

        private int supplier_Id;
        public int Supplier_Id
        {
            get => supplier_Id;

            set => SetField(ref  supplier_Id, value);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = " ")
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
