
using MauiApp1.Models;
using MauiApp1.Services;
using System.Collections.ObjectModel;

namespace MauiApp1.Views;

public partial class RolePage : ContentPage
{
    Role selectedRole = null;
    IRoleService roleService;
    ObservableCollection<Role> roles = new ObservableCollection<Role>();

    public RolePage()
    {
        InitializeComponent();
        this.BindingContext = this;
        this.roleService = new RoleService();

        Task.Run(async () => await LoadRoles());
        txe_role.Text = "";
    }

    private async Task LoadRoles()
    {
        roles = new ObservableCollection<Role>(await roleService.GetRoleList());
        ltv_roles.ItemsSource = roles;
    }

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

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_roles.SelectedItem == null)
        {
            await Shell.Current.DisplayAlert("No Role Selected", "Select the role you want to delete from the list", "OK");
            return;
        }

        await roleService.DeleteRole(selectedRole);
        roles.Remove(selectedRole);

        ltv_roles.SelectedItem = null;
        txe_role.Text = "";
    }

    private void ltv_roles_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectedRole = e.SelectedItem as Role;
        if (selectedRole == null) return;

        txe_role.Text = selectedRole.Name;
    }
}