using UndacApp.Models;

namespace MauiApp1.Models
{
    /// <summary>
    /// Represents a RotaPool object.
    /// </summary>
    public class RotaPool : AModel
    {
        private string _rota = string.Empty;
        public string Rota {
            get => _rota;
            set => SetField(ref _rota, value);
        }

        private string _member = string.Empty;
        public string Member {
            get => _member;
            set => SetField(ref _member, value);
        }

        private bool _assigned = false;
        public bool Assigned {
            get => _assigned;
            set => SetField(ref _assigned, value);
        }

        private string _start_date = string.Empty;
        public string StartDate {
            get => _start_date;
            set => SetField(ref _start_date, value);
        }

        private string _end_date = string.Empty;
        public string EndDate {
            get => _end_date;
            set => SetField(ref _end_date, value);
        }
    }
}
