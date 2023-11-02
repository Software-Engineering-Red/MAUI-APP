using UndacApp.Models;
using UndacApp.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

/*! The RolesPage class allows for the basic functions 
 *  implemented in the RoleService class to be used within
 *  the UI
 */

namespace UndacApp.Views;

public partial class RolesPage : ContentPage
{
    Role selectedRole = null;
    IRoleService roleService;
    ObservableCollection<Role> roles = new ObservableCollection<Role>();

    //! public initialisation of components
    public RolesPage()
	{
        InitializeComponent();
        this.BindingContext = this;
        this.roleService = new RoleService();

        Task.Run(async () => await LoadRoles());
        txe_role.Text = "";
    }

    //! Task to load roles into a variable
    private async Task LoadRoles()
    {
        roles = new ObservableCollection<Role>(await roleService.GetRoleList());
        ltv_roles.ItemsSource = roles;
    }

    /*!
    * Detect save button being clicked
    * @param sender (Object) the sender object created by the event
    * @param e (EventArgs) the arguments passed into the event
    */
    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txe_role.Text)) return;

        if (selectedRole == null)
        {
            var role = new Role() { Name = txe_role.Text };
            roleService.AddRole(role);
            roles.Add(role);
        }
        else
        {
            selectedRole.Name = txe_role.Text;
            roleService.UpdateRole(selectedRole);
            var role = roles.FirstOrDefault(x => x.ID == selectedRole.ID);
            role.Name = txe_role.Text;
        }

        selectedRole = null;
        ltv_roles.SelectedItem = null;
        txe_role.Text = "";

    }

    /*!
    * Detect delete button being clicked
    * @param sender (Object) the sender object created by the event
    * @param e (EventArgs) the arguments passed into the event
    */
    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_roles.SelectedItem == null)
        {
            await DisplayAlert("No Role Selected", "Select the role you want to delete from the list", "OK");
            return;
        }

        await roleService.DeleteRole(selectedRole);
        roles.Remove(selectedRole);

        ltv_roles.SelectedItem = null;
        txe_role.Text = "";
    }

    /*!
    * Detect all items selected in list view
    * @param sender (Object) the sender object created by the event
    * @param e (SelectedItemChangedEventArgs) the arguments passed into the event
    */
    private void ltv_roles_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectedRole = e.SelectedItem as Role;
        if (selectedRole == null) return;

        txe_role.Text = selectedRole.Name;
    }
}