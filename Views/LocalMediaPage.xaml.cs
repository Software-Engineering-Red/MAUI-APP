using System.Collections.ObjectModel;
using UndacApp.Models;
using UndacApp.Services.LocalMedias;

namespace UndacApp.Views;

public partial class LocalMediaPage : ContentPage
{
    LocalMedia selectedLocalMedia = null;
    ILocalMediaService localMediaService;
    ObservableCollection<LocalMedia> localMedias = new ObservableCollection<LocalMedia>();
    private ObservableCollection<LocalMedia> _localMedias = new ObservableCollection<LocalMedia>();
    private LocalMedia _selectedLocalMedia = null;
   
    public LocalMediaPage()
    {
        InitializeComponent();
        BindingContext = new LocalMedia();
        this.localMediaService = new LocalMediaService();
        Task.Run(async () => await LoadLocalMedias());
       
    }
    private async Task LoadLocalMedias()
    { 
        localMedias = new ObservableCollection<LocalMedia>(await localMediaService.GetLocalMediaList());
        MediaListView.ItemsSource= localMedias;
    }
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
    private async Task DeleteButton_Clicked()
{
    try
    {
        if (MediaListView.SelectedItem == null)
        {
            await DisplayAlert("No local media Selected", "Select local media you want to delete from the list", "OK");
            return;
        }

        // Check if the LocalMediaPicker has a selected item
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


    private void DeleteButton_Clicker(object sender, EventArgs e)
    {
        _ = DeleteButton_Clicked();
    }
    private void MediaListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectedLocalMedia = e.SelectedItem as LocalMedia;
        if (selectedLocalMedia == null) return;

        NameEntry.Text = selectedLocalMedia.Name;
        EmailEntry.Text = selectedLocalMedia.Email;
        MediaListView.ItemsSource = localMedias;
    }
    private void Mediafilter_TextChanged(object sender, TextChangedEventArgs e)
    {
        string nameFilter = NameFilter.Text;
        string emailFilter = EmailFilter.Text;
        string mediaFilter = MediaFilter.Text;
        
        var filteredMedia = localMedias
            .Where(localMedia => PassNameFilter(localMedia, nameFilter))
            .Where(localMedia => PassEmailFilter(localMedia, emailFilter))
            .Where(localMedia => PassSkillFilter(localMedia, mediaFilter))
            .ToList();

        MediaListView.ItemsSource = new ObservableCollection<LocalMedia>(filteredMedia);
    }
    private bool PassNameFilter(LocalMedia localMedia, string nameFilter)
    {
        return string.IsNullOrEmpty(nameFilter) || localMedia.Name.Contains(nameFilter);
    }

    private bool PassEmailFilter(LocalMedia localMedia, string emailFilter)
    {
        return string.IsNullOrEmpty(emailFilter) || localMedia.Email.Contains(emailFilter);
    }

    private bool PassSkillFilter(LocalMedia localMedia, string skillFilter)
    {
        return string.IsNullOrEmpty(skillFilter) || localMedia.Media.Contains(skillFilter);
    }
    private void ClearFilter_Clicked(object sender, EventArgs e)
    {
        
        NameFilter.Text = string.Empty;
        EmailFilter.Text = string.Empty;
        MediaFilter.Text = string.Empty;


        MediaListView.ItemsSource = _localMedias;
    }
}
