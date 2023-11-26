using UndacApp.Models;
using UndacApp.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

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
        }

        private async Task LoadNeeds()
        {
            needs = new ObservableCollection<Need>(await needService.GetAllNeeds());
            ltv_needs.ItemsSource = needs;
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txe_need.Text)) return;

            if (selectedNeed == null)
            {
                var need = new Need() { Description = txe_need.Text };
                needService.AddNeed(need);
                needs.Add(need);
            }
            else
            {
                selectedNeed.Description = txe_need.Text;
                needService.UpdateNeed(selectedNeed);
                var need = needs.FirstOrDefault(x => x.Id == selectedNeed.Id);
                need.Description = txe_need.Text;
            }

            selectedNeed = null;
            ltv_needs.SelectedItem = null;
            txe_need.Text = "";
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
        }

        private void ltv_needs_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            selectedNeed = e.SelectedItem as Need;
            if (selectedNeed == null) return;

            txe_need.Text = selectedNeed.Description;
        }
    }
}
