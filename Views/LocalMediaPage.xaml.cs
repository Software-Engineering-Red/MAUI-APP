using System.Collections.ObjectModel;
using UndacApp.Models;
using UndacApp.Services.LocalMedias;

namespace UndacApp.Views;

public partial class LocalMediaPage : ContentPage
{
    /* Define class-level variables*/
    LocalMedia selectedLocalMedia = null;
    ILocalMediaService localMediaService;
    ObservableCollection<LocalMedia> localMedias = new ObservableCollection<LocalMedia>();
    private ObservableCollection<LocalMedia> _localMedias = new ObservableCollection<LocalMedia>();
    private LocalMedia _selectedLocalMedia = null;
    /*Constructor for LocalMediaPage , initialize components and set the binding context and initialize the localMediaService and load local media asynchronously */
    public LocalMediaPage()
    {
        InitializeComponent();
        BindingContext = new LocalMedia();
        this.localMediaService = new LocalMediaService();
        Task.Run(async () => await LoadLocalMedias());
       
    }
    /*Asynchronously load local media*/
    private async Task LoadLocalMedias()
    { 
        localMedias = new ObservableCollection<LocalMedia>(await localMediaService.GetLocalMediaList());
        MediaListView.ItemsSource= localMedias;
    }
    /*Handle the "Add" button click event, create a new LocalMedia object and add it to the collection and update the selected local media if it already exists  then clear input fields and selection*/
    private void OnAddClicked(object sender, EventArgs e)
    {
        if (selectedLocalMedia == null)
        {
            var localMedia = new LocalMedia()
            {
                Name = NameEntry.Text,
                Email = EmailEntry.Text,
               
            };

            string selectedPickerItem = LocalMediaPicker.SelectedItem as string;
            localMedia.Media = selectedPickerItem;
            localMediaService.AddLocalMedia(localMedia);
            localMedias.Add(localMedia);
        }
        else
        {
            selectedLocalMedia.Name = NameEntry.Text;
            selectedLocalMedia.Email = EmailEntry.Text;
            string selectedPickerItem = LocalMediaPicker.SelectedItem as string;                      
            selectedLocalMedia.Media = selectedPickerItem;
            localMediaService.UpdateLocalMedia(selectedLocalMedia);
        }
        NameEntry.Text = null;
        EmailEntry.Text = null;
        LocalMediaPicker.SelectedItem = null;
        MediaListView.SelectedItem = null;

    }
    /*Handle the "Copy" button click event ,display an alert if no local media is selected for copying and copy the selected local media*/
    private void OnCopyClicked(object sender, EventArgs e)
    {
        try
        {
            if (MediaListView.SelectedItem == null)
            {
                DisplayAlert("No local media Selected", "Select local media you want to copy from the list", "OK");
                return;
            }
            LocalMedia selectedLocalMedia = (LocalMedia)MediaListView.SelectedItem;
            LocalMedia copiedLocalMedia = new LocalMedia()
            {
                Name = selectedLocalMedia.Name,
                Email = selectedLocalMedia.Email,
                Media = selectedLocalMedia.Media
            };     
            DisplayAlert("Copy Successful", "Selected local media has been copied", "OK");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error copying local media: {ex.Message}");
        }
    }
    /*Asynchronously handle the delete button click event,display an alert if no local media is selected for deletion, check if the LocalMediaPicker has a selected item and remove it and delete,remove or clear selected field */
    private async Task DeleteButton_Clicked()
{
    try
    {
        if (MediaListView.SelectedItem == null)
        {
            await DisplayAlert("No local media Selected", "Select local media you want to delete from the list", "OK");
            return;
        }
                
        if (LocalMediaPicker.SelectedItem != null)
        {
            
            string selectedPickerItem = LocalMediaPicker.SelectedItem as string;

            
            LocalMediaPicker.Items.Remove(selectedPickerItem);
        }
        await localMediaService.DeleteLocalMedia(selectedLocalMedia);                    
        localMedias.Remove(selectedLocalMedia);        
        MediaListView.SelectedItem = null;
        NameEntry.Text = null;
        EmailEntry.Text = null;
        LocalMediaPicker.SelectedItem = null;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error deleting local media: {ex.Message}");
    }
}
    /*Handle the delete button click event by triggering the asynchronous method*/
    private void DeleteButton_Clicker(object sender, EventArgs e)
    {
        _ = DeleteButton_Clicked();
    }
    /* Handle the selection of an item in the media list view and update the selected local media and display its details in the input fields*/
    private void MediaListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectedLocalMedia = e.SelectedItem as LocalMedia;
        if (selectedLocalMedia == null) return;

        NameEntry.Text = selectedLocalMedia.Name;
        EmailEntry.Text = selectedLocalMedia.Email;
        MediaListView.ItemsSource = localMedias;
    }
    /*Handle the text changed event for the filter inputs,get filter values,filter the local media based on the input filters and update the media list view with the filtered results*/
    private void Mediafilter_TextChanged(object sender, TextChangedEventArgs e)
    {
        string nameFilter = NameFilter.Text;
        string emailFilter = EmailFilter.Text;
        string mediaFilter = MediaFilter.Text;
        
        var filteredMedia = localMedias
            .Where(localMedia => PassNameFilter(localMedia, nameFilter))
            .Where(localMedia => PassEmailFilter(localMedia, emailFilter))
            .Where(localMedia => PassMediaFilter(localMedia, mediaFilter))
            .ToList();

        MediaListView.ItemsSource = new ObservableCollection<LocalMedia>(filteredMedia);
    }
    /*Helper method to check if a local media passes the name filter*/
    private bool PassNameFilter(LocalMedia localMedia, string nameFilter)
    {
        return string.IsNullOrEmpty(nameFilter) || localMedia.Name.Contains(nameFilter);
    }
    /*Helper method to check if a local media passes the email filter*/
    private bool PassEmailFilter(LocalMedia localMedia, string emailFilter)
    {
        return string.IsNullOrEmpty(emailFilter) || localMedia.Email.Contains(emailFilter);
    }
    /*Helper method to check if a local media passes the media filter*/
    private bool PassMediaFilter(LocalMedia localMedia, string mediaFilter)
    {
        return string.IsNullOrEmpty(mediaFilter) || localMedia.Media.Contains(mediaFilter);
    }
    /*Handle the clear filter button click event and clear filter inputs and update the media list view with the original collection*/
    private void ClearFilter_Clicked(object sender, EventArgs e)
    {
        
        NameFilter.Text = string.Empty;
        EmailFilter.Text = string.Empty;
        MediaFilter.Text = string.Empty;


        MediaListView.ItemsSource = _localMedias;
    }
}
