using UndacApp.Models;
using UndacApp.Services;
using System.Collections.ObjectModel;

namespace UndacApp.Views;


public partial class RoomTypePage : ContentPage
{
	RoomType selectedRoomType = null;
	IRoomTypeService roomTypeService;
    ObservableCollection<RoomType> roomTypes = new ObservableCollection<RoomType>();
    public RoomTypePage()
	{
        InitializeComponent();

        // Set the binding context to the RoomTypePage
        this.BindingContext = this;
        this.roomTypeService = new RoomTypeService();

        // Load room types and initialize the text input 
        Task.Run(async () => await LoadRoomTypes());
        txe_roomtype.Text = "";
    }

    // Load room types from the database
    private async Task LoadRoomTypes()
    {
        roomTypes = new ObservableCollection<RoomType>(await roomTypeService.GetRoomTypeList());
        ltv_roomtype.ItemsSource = roomTypes;
    }
    // Save button click event handler 
    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txe_roomtype.Text)) return;

        if (selectedRoomType == null)
        {
            // Add a new room type to the database/collection
            var newRoom = new RoomType() { Name = txe_roomtype.Text };
            roomTypeService.AddRoomType(newRoom);
            roomTypes.Add(newRoom);
        }
        else
        {
            // Update existing room type in the database 
            selectedRoomType.Name = txe_roomtype.Text;
            roomTypeService.UpdateRoomType(selectedRoomType);

            // Update the room type in the collection
            var room = roomTypes.FirstOrDefault(x => x.ID == selectedRoomType.ID);
            room.Name = txe_roomtype.Text;
        }

        // Reset selectedRoomType and input field
        selectedRoomType = null;
        ltv_roomtype.SelectedItem = null;
        txe_roomtype.Text = "";
    }

    // Delete button click event handler
    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_roomtype.SelectedItem == null)
        {
            await Shell.Current.DisplayAlert("No Room Type Selected", "Select the room type you want to delete from the list", "OK");
            return;
        }
        // Delete the selected room type from the database/collection
        await roomTypeService.DeleteRoomType(selectedRoomType);
        roomTypes.Remove(selectedRoomType);

        // Reset selectedRoomType and input field
        ltv_roomtype.SelectedItem = null;
        txe_roomtype.Text = "";
    }

    // Handle selection of an room type item
    private void ltv_roomtypes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectedRoomType = e.SelectedItem as RoomType;
        if (selectedRoomType == null) return;
        // Populate the input field with the selected room types name
        txe_roomtype.Text = selectedRoomType.Name;
    }


}