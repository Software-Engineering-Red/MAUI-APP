using System.ComponentModel;

namespace UndacApp.Models
{
    public class PrivilegeRequest : AModel
    {
        private string requestType;
        private int memberID;
        private string privilegeLevel;
        private bool approved;
        private string systemType;

        public bool Approved
        {
            get => approved;
            set => SetField(ref approved, value);
        }

        public string RequestType
        {
            get => requestType;
            set => SetField(ref requestType, value);
        }

        public string PrivilegeLevel
        {
            get => privilegeLevel;
            set => SetField(ref privilegeLevel, value);
        }

        public int MemberID
        {
            get => memberID;
            set => SetField(ref  memberID, value);
        }
        public string SystemType
        {        get => systemType;
                 set => SetField(ref systemType, value);
        }
    }
}
