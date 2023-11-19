using UndacApp.Models;
using UndacApp.Services;
using System.Collections.ObjectModel;

namespace UndacApp.Views;

public partial class ITSystemStatusPage : ContentPage
{
    ITSystemStatus selectedItem = null;
    ITSystemStatusService service;
    ObservableCollection<ITSystemStatus> itemList = new ObservableCollection<ITSystemStatus>();

    public ITSystemStatusPage()
    {
        InitializeComponent();
        this.BindingContext = this;
        this.service = new ITSystemStatusService();

        Task.Run(async () => await LoadTeamMembers());
    }


    //! Task to load teamMembers into a variable
    private async Task LoadTeamMembers()
    {
        itemList = new ObservableCollection<ITSystemStatus>(await service.GetAll());
        ltv_systemStatusItems.ItemsSource = itemList;
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        string name = txe_ITSystemStatusName.Text;
        string status = txe_ITSystemStatusStatus.Text;
        string avail = txe_ITSystemStatusAvali.Text;
    
        if (string.IsNullOrEmpty(status) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(avail)) return;

        avail = avail.ToLower();

        bool avaliable = false;
        if (avail == "y" | avail == "yes") {
            avaliable = true;
        }
        ITSystemStatus item = new ITSystemStatus
        {
            Name = name,
            Status = status,
            Avaliable = avaliable,
        };

        await service.Add(item);
        itemList.Add(item);

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

        ITSystemStatus item = (ITSystemStatus)ltv_systemStatusItems.SelectedItem;

        await service.Remove(item);
        itemList.Remove(item);

        resetFields();
    }


    /*!
    * Detect all items selected in list view
    * @param sender (Object) the sender object created by the event
    * @param e (SelectedItemChangedEventArgs) the arguments passed into the event
    */
    private void ltv_teamMembers_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        ITSystemStatus item = e.SelectedItem as ITSystemStatus;
        if (item == null) return;

        txe_ITSystemStatusAvali.Text = item.Avaliable.ToString();
        txe_ITSystemStatusName.Text = item.Name;
        txe_ITSystemStatusStatus.Text = item.Status;
    }

    private void resetFields()
    {
        txe_ITSystemStatusAvali.Text = "";
        txe_ITSystemStatusName.Text = "";
        txe_ITSystemStatusStatus.Text = "";
        ltv_systemStatusItems.SelectedItem = null;
    }

}