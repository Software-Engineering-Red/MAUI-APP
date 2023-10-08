using MauiApp1.Models;
using MauiApp1.Services;
using System.Collections.ObjectModel;

namespace MauiApp1.Views;


public partial class RoomTypePage : ContentPage
{
	RoomType selectedRoomType = null;
	IRoomTypeService roomTypeService;
    ObservableCollection<RoomType> roomTypes = new ObservableCollection<RoomType>();
    public RoomTypePage()
	{
        InitializeComponent();

        this.BindingContext = this;
        this.roomTypeService = new RoomTypeService();

        Task.Run(async () => await LoadContinents());
        txe_roomtype.Text = "";


    }

    private async Task LoadContinents()
    {
        roomTypes = new ObservableCollection<RoomType>(await roomTypeService.GetRoomTypeList());
        ltv_roomtype.ItemsSource = roomTypes;
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txe_roomtype.Text)) return;

        if (selectedRoomType == null)
        {
            var newRoom = new RoomType() { Name = txe_roomtype.Text };
            roomTypeService.AddRoomType(newRoom);
            roomTypes.Add(newRoom);
        }
        else
        {
            selectedRoomType.Name = txe_roomtype.Text;
            roomTypeService.UpdateRoomType(selectedRoomType);
            var continent = roomTypes.FirstOrDefault(x => x.ID == selectedRoomType.ID);
            continent.Name = txe_roomtype.Text;
        }


        selectedRoomType = null;
        ltv_roomtype.SelectedItem = null;
        txe_roomtype.Text = "";
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_roomtype.SelectedItem == null)
        {
            await Shell.Current.DisplayAlert("No Room Type Selected", "Select the room type you want to delete from the list", "OK");
            return;
        }

        await roomTypeService.DeleteRoomType(selectedRoomType);
        roomTypes.Remove(selectedRoomType);

        ltv_roomtype.SelectedItem = null;
        txe_roomtype.Text = "";
    }

    private void ltv_roomtypes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectedRoomType = e.SelectedItem as RoomType;
        if (selectedRoomType == null) return;

        txe_roomtype.Text = selectedRoomType.Name;
    }


}