using UndacApp.Models;
using UndacApp.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UndacApp.Views
{
    public partial class SubTeamsStatusPage : ContentPage
    {
        SubTeamStatus selectedItem = null;
        ISubTeamStatusService service;
        ObservableCollection<SubTeamStatus> itemList = new ObservableCollection<SubTeamStatus>();

        public SubTeamsStatusPage()
        {
            InitializeComponent();
            BindingContext = new SubTeamStatus();
            this.service = new ISubTeamStatusService(); 

            Task.Run(async () => await LoadSubTeams());

        }

        //! Task to load sub-teams into a variable
        private async Task LoadSubTeams()
        {
            itemList = new ObservableCollection<SubTeamStatus>(await service.GetSubTeamStatuses());
            ltv_systemStatusItems.ItemsSource = itemList;
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            string name = txe_SubTeamName.Text;
            string location = txe_SubTeamLocation.Text;
            string personnel = txe_SubTeamPersonnel.Text;
            string resources = txe_SubTeamResources.Text;
            string leaderCommunication = txe_SubTeamLeaderCommunication.Text;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(location)) return;

            SubTeamStatus subTeam = new SubTeamStatus
            {
                Name = name,
                Location = location,
                Personnel = personnel,
                Resources = resources,
                LeaderCommunication = leaderCommunication
            };

            service.AddSubTeamStatus(subTeam);
            itemList.Add(subTeam);

            // Reset fields
            resetFields();
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if (ltv_systemStatusItems.SelectedItem == null)
            {
                await DisplayAlert("No item Selected", "Please select a valid item!", "OK");
                return;
            }

            SubTeamStatus subTeam = (SubTeamStatus)ltv_systemStatusItems.SelectedItem;

            await service.DeleteSubTeamStatus(subTeam);
            itemList.Remove(subTeam);

            resetFields();
        }

        /*!
        * Detect all items selected in list view
        * @param sender (Object) the sender object created by the event
        * @param e (SelectedItemChangedEventArgs) the arguments passed into the event
        */
        private void ltv_systemStatusItems_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            SubTeamStatus subTeam = e.SelectedItem as SubTeamStatus;
            if (subTeam == null) return;

            txe_SubTeamName.Text = subTeam.Name;
            txe_SubTeamLocation.Text = subTeam.Location;
            txe_SubTeamPersonnel.Text = subTeam.Personnel;
            txe_SubTeamResources.Text = subTeam.Resources;
            txe_SubTeamLeaderCommunication.Text = subTeam.LeaderCommunication;
        }

        private void resetFields()
        {
            txe_SubTeamName.Text = "";
            txe_SubTeamLocation.Text = "";
            txe_SubTeamPersonnel.Text = "";
            txe_SubTeamResources.Text = "";
            txe_SubTeamLeaderCommunication.Text = "";
            ltv_systemStatusItems.SelectedItem = null;
        }
    }
}
