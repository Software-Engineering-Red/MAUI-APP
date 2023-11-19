using SQLite;

namespace UndacApp.Models {
    [Table("it_system_status")]
    public class ITSystemStatus : AModel
    {

        /// <summary>
        /// status stored for ITStatus
        /// </summary>
        public string status;

        /// <summary>
        /// The Public getter and setter for status. 
        /// </summary>
        public string Status
        {
            get => status;
            set => SetField(ref status, value);
        }

        /// <summary>
        /// avaliable stored for ITStatus
        /// </summary>
        public bool avaliable;

        /// <summary>
        /// The Public getter and setter for avaliable. 
        /// </summary>
        public bool Avaliable
        {
            get => avaliable;
            set => SetField(ref avaliable, value);
        }
    }

}