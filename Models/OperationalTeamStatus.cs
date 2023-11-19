using SQLite;
namespace UndacApp.Models
{
    /// <summary>
    /// Class representing the Status of OperationalTeam extending INotifyPropertyChanged.
    /// Data will be stored in SQLite Database "operational_team_status".
    /// </summary>
    [Table("operational_team_status")]
    public class OperationalTeamStatus : AModel
    {

    }
}
