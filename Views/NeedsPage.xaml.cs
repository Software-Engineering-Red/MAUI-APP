using UndacApp.Models;
using UndacApp.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Immutable;

namespace UndacApp.Views
{
    public partial class NeedsPage : ContentPage
    {
        Need selectedNeed = null;
        INeedService needService;
        ObservableCollection<Need> needs = new ObservableCollection<Need>();

        public NeedsPage()
        {
            InitializeComponent();
            BindingContext = new Need();
            this.needService = new NeedService();

            Task.Run(async () => await LoadNeeds());
            txe_need.Text = "";
            txe_priority.Text = "";
        }

        private async Task LoadNeeds()
        {
            needs = new ObservableCollection<Need>(await needService.GetAllNeeds());
            ltv_needs.ItemsSource = needs;
            var groupedNeeds = needs.GroupBy(n => n.Priority).Select(group => new ObservableCollection<Need>(group)).ToList();
            ltv_needsBreakdown.ItemsSource = groupedNeeds;
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txe_need.Text)) return;
            if (String.IsNullOrEmpty(txe_priority.Text)) return;
            if (selectedNeed == null)
            {
                var need = new Need() { Name = txe_need.Text, Priority = txe_priority.Text };
                needService.AddNeed(need);
                needs.Add(need);
            }
            selectedNeed = null;
            ltv_needs.SelectedItem = null;
            txe_need.Text = "";
            txe_priority.Text = "";
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if (ltv_needs.SelectedItem == null)
            {
                await DisplayAlert("No Need Selected", "Select the need you want to delete from the list", "OK");
                return;
            }

            await needService.DeleteNeed(selectedNeed);
            needs.Remove(selectedNeed);

            ltv_needs.SelectedItem = null;
            txe_need.Text = "";
            txe_priority.Text = "";
        }

        private void ltv_needs_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            selectedNeed = e.SelectedItem as Need;
            if (selectedNeed == null) return;

            txe_need.Text = selectedNeed.Name;
            txe_priority.Text = selectedNeed.Priority;
        }
    }
}
