using UndacApp.Models;
using UndacApp.Services;
using System.Collections.ObjectModel;

namespace UndacApp.Views;

/*! <summary>
        OrganisationPage class extending ContentPage, responsible for functionality on OrganisationPage view.
    </summary> */
public partial class OrganisationPage : ContentPage
{
/*! <summary>
        A reference pointer for storing currently selected Organisation.
     </summary> */
    Organisation selectedOrg = null;

/*! <summary>
        An instance of IOrganisationService
     </summary> */
    readonly IOrganisationService organisationService;

/*! <summary>
        Collection of current Organisations.
    </summary> */
    ObservableCollection<Organisation> orgs = new();

/*! <summary>
        Constructor class, setting the binding context and initiating the organisation serrvice, as well as loading the organisation list.
    </summary> */
    public OrganisationPage()
    {
        InitializeComponent();
        BindingContext = new Organisation();
        this.organisationService = new OrganisationService();

        Task.Run(async () => await LoadOrganisations());
        txe_organisation.Text = "";
    }

    /*! <summary>
            Private method loading the Organisation list using organisationService getter.
        </summary> 
        <returns>Task promise, informing about the status of its' completion.</returns> */
    private async Task LoadOrganisations()
    {
        orgs = new ObservableCollection<Organisation>(await organisationService.GetOrganisationList());
        ltv_organisations.ItemsSource = orgs;
    }

    /*! <summary>
            Method responsible for saving organisation into SQLite database, triggered by selection of save button.
        </summary> 
        <param name="sender">Details about the element that triggered the event.</param>
        <param name="e">Event details, passed by eventHandler due to clicking event button.</param> */
    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txe_organisation.Text)) return;

        if (selectedOrg == null)
        {
            var org = new Organisation() { Name = txe_organisation.Text };
            organisationService.AddOrganisation(org);
            orgs.Add(org);
        }
        else
        {
            selectedOrg.Name = txe_organisation.Text;
            organisationService.UpdateOrganisation(selectedOrg);

            var org = orgs.FirstOrDefault(x => x.id == selectedOrg.id);
            org.Name = txe_organisation.Text;

        }
        selectedOrg = null;
        ltv_organisations.SelectedItem = null;
        txe_organisation.Text = "";
        txe_organisation.Focus();
    }

    /*! <summary>
             Method responsible for removing organisation from SQLite database, triggered by selection of delete button.
             Note: If no Organisation is selected, no organisation will be removed.
        </summary> 
        <param name="sender">Details about the element that triggered the event.</param>
        <param name="e">Event details, passed by eventHandler due to clicking event button.</param> */
    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_organisations.SelectedItem == null)
        {
            await DisplayAlert("No Org Selected", "Select organisation you want to delete from the list", "OK");
            return;
        }

        await organisationService.DeleteOrganisation(selectedOrg);
        orgs.Remove(selectedOrg);

        ltv_organisations.SelectedItem = null;
        txe_organisation.Text = "";
        txe_organisation.Focus();
    }

    /*! <summary>
            Method responsible for updating currently selected item, integrating UI and Backend functionality.
        </summary> 
        <param name="sender">Details about the element that triggered the event.</param>
        <param name="e">Event details, passed by eventHandler due to clicking event button.</param> */
    private void Ltv_organisations_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectedOrg = e.SelectedItem as Organisation;
        if (selectedOrg == null) return;
        txe_organisation.Text = selectedOrg.Name;
        txe_organisation.Focus();
    }
}