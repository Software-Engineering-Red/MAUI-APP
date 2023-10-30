
using System.ComponentModel;

namespace MauiApp1.View
{
    public partial class CalenderPage : ContentPage
    {
        public DateTime EventDate { get; set; } = new DateTime();
        
        public TimeOnly EventTimeStartO { get; set; } = new TimeOnly();
        public TimeSpan EventTimeFinsihS { get; set; } = new TimeSpan();
        public TimeOnly EventTimeFinishO { get; set; } = new TimeOnly();

        public string EventName { get; set; }
        public string EventLocation { get; set; }

        public List<string> Particapents { get; set; }
        public List<string> OrganisationType { get; set; }
        public List<string> Continent { get; set; }

        public int EventDuration { get; set; }

        public CalenderPage()
        {
            
            InitializeComponent();
            
        }

        #region list of people/organisations and continets pickers
        private void P_ParticapentsSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            //to be writen due to no aditional code
        }

        private void P_PickOrganisation_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Organisation;
            //TB_EventOrganisation.Text = Organisation;
        }

        private void P_PickContinent_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Continent;
            //TB_EventContinent.Text = Continent;
        }
        #endregion

        #region date and time functions
        private void DP_EventDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            EventDate = e.NewDate;
            TB_EventDate.Text = EventDate.ToString();
        }

        private void TP_PickStart_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(TP_PickStart == null)
            {
                return;
            }
            TimeSpan EventTimeStartS = new TimeSpan(TP_PickStart.Time.Hours, TP_PickStart.Time.Minutes, TP_PickStart.Time.Seconds);
            TB_StartTimeDisplay.Text = EventTimeStartS.ToString();
        }
        private void TP_PickFinish_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(TP_PickFinish == null)
            {
                return;
            }
            TimeSpan EventTimeFinishS = new TimeSpan(TP_PickFinish.Time.Hours, TP_PickFinish.Time.Minutes, TP_PickFinish.Time.Seconds);
            TB_FinishTimeDisplay.Text = EventTimeFinishS.ToString();
        }
        #endregion

        #region text box
        private void TBX_EventName_TextChanged(object sender, TextChangedEventArgs e)
        {
            EventName = TBX_EventName.Text;
            TB_EventNameDisplay.Text = EventName;
        }

        private void TBX_EventLocation_TextChanged(object sender, TextChangedEventArgs e)
        {
            EventLocation = TBX_EventLocation.Text;
            TB_EventLoacation.Text = EventLocation;
        }

        private void TBX_EventDuration_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (EventDurationCheck() == true)
            {
                EventDuration = int.Parse(TBX_EventDuration.Text);
                TB_EventDuration.Text = EventDuration.ToString();
            }
            else if(TBX_EventDuration.Text == "")
            {
                return;
            }
            else
            {
                DisplayAlert("wrong digits", "please enter a whole number", "ok");
                TBX_EventDuration.Text = "";
            }
        }
        #endregion

        #region extra used functions
        private bool EventDurationCheck()
        {
            char[] charInt = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            foreach (char i in charInt)
               if (TBX_EventDuration.Text.Contains(i))
                { 
                    return true;
                }
                return false;
            
            
        }
        #endregion

    }
}