using MauiApp1.Models;
using MauiApp1.Services;
using System.Collections.ObjectModel;

namespace MauiApp1.Views;

public partial class OrganisationPage : ContentPage
{
    Organisation selectedOrg = null;
    readonly IOrganisationService organisationService;
    ObservableCollection<Organisation> orgs = new();
    public OrganisationPage()
    {
        InitializeComponent();
        this.BindingContext = this;
        this.organisationService = new OrganisationService();

        Task.Run(async () => await LoadOrganisations());
        txe_organisation.Text = "";
    }

    private async Task LoadOrganisations()
    {
        orgs = new ObservableCollection<Organisation>(await organisationService.GetOrganisationList());
        ltv_organisations.ItemsSource = orgs;
    }

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

    private void Ltv_organisations_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectedOrg = e.SelectedItem as Organisation;
        if (selectedOrg == null) return;
        txe_organisation.Text = selectedOrg.Name;
        txe_organisation.Focus();
    }
}