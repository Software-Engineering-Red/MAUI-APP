
using UndacApp.Models;
using UndacApp.Services;
using System.Collections.ObjectModel;

namespace UndacApp.Views;

public partial class MethodologicalDocumentationPage : ContentPage
{

    private MethodologicalDocumentation? selectedDocument = null;

    MethodologicalDocumentationService methodologicalDocumentationService;

    ObservableCollection<MethodologicalDocumentation> methodologicalDocuments = new ObservableCollection<MethodologicalDocumentation>();

    public MethodologicalDocumentationPage()
    {
        InitializeComponent();
        this.BindingContext = new MethodologicalDocumentation();
        this.methodologicalDocumentationService = new MethodologicalDocumentationService();

        Task.Run(async () => await LoadMethodologicalDocuments());
        txe_methodological_documentation.Text = "";
    }

    /*! <summary>
            Private method loading the Continent list using ContinentService getter.
        </summary> 
        <returns>Task promise, informing about the status of its' completion.</returns> */
    private async Task LoadMethodologicalDocuments()
    {
        methodologicalDocuments = new ObservableCollection<MethodologicalDocumentation>(await methodologicalDocumentationService.GetAll());
        ltv_methodological_documents.ItemsSource = methodologicalDocuments;
    }

    /*! <summary>
            Method responsible for saving Continent into SQLite database, triggered by selection of save button.
        </summary> 
        <param name="sender">Details about the element that triggered the event.</param>
        <param name="e">Event details, passed by eventHandler due to clicking event button.</param> */
    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(NameEntry.Text)) return;
        if (String.IsNullOrEmpty(AuthorEntry.Text)) return;
        if (CreatedAtEntry.Date == default) return;
        if (LastModifiedAtEntry.Date == default) return;
        if (String.IsNullOrEmpty(txe_methodological_documentation.Text)) return;

        if (selectedDocument == null)
        {
            var methodologicalDocument = new MethodologicalDocumentation()
            {
                Name = NameEntry.Text,
                Author = AuthorEntry.Text,
                CreatedAt = CreatedAtEntry.Date,
                LastModifiedAt = LastModifiedAtEntry.Date,
                Contents = txe_methodological_documentation.Text,
                
            };
            await methodologicalDocumentationService.Add(methodologicalDocument);
            methodologicalDocuments.Add(methodologicalDocument);
        }
        else
        {
            selectedDocument.Name = txe_methodological_documentation.Text;
            selectedDocument.Author = AuthorEntry.Text;
            selectedDocument.CreatedAt = CreatedAtEntry.Date;
            selectedDocument.LastModifiedAt = LastModifiedAtEntry.Date;
            selectedDocument.Contents = txe_methodological_documentation.Text;
            await methodologicalDocumentationService.Update(selectedDocument);
            var methodologicalDocument = methodologicalDocuments.FirstOrDefault(x => x.ID == selectedDocument.ID);
            methodologicalDocument.Name = txe_methodological_documentation.Text;
            methodologicalDocument.Author = AuthorEntry.Text;
            methodologicalDocument.CreatedAt = CreatedAtEntry.Date;
            methodologicalDocument.LastModifiedAt = LastModifiedAtEntry.Date;
            methodologicalDocument.Contents = txe_methodological_documentation.Text;
        }


        selectedDocument = null;
        ltv_methodological_documents.SelectedItem = null;
        txe_methodological_documentation.Text = "";
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_methodological_documents.SelectedItem == null)
        {
            await Shell.Current.DisplayAlert("No Methodological Document Selected", "Select the continent you want to delete from the list", "OK");
            return;
        }

        await methodologicalDocumentationService.Remove(selectedDocument);
        methodologicalDocuments.Remove(selectedDocument);

        ltv_methodological_documents.SelectedItem = null;
        txe_methodological_documentation.Text = "";
    }

    private void ltv_methodological_documents_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectedDocument = e.SelectedItem as MethodologicalDocumentation;
        if (selectedDocument == null) return;

        txe_methodological_documentation.Text = selectedDocument.Name;
        AuthorEntry.Text = selectedDocument.Author;
        CreatedAtEntry.Date = selectedDocument.CreatedAt.GetValueOrDefault();
        LastModifiedAtEntry.Date = selectedDocument.LastModifiedAt.GetValueOrDefault();
        txe_methodological_documentation.Text = selectedDocument.Contents;

    }
}