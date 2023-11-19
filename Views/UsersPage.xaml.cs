using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UndacApp.Models;
using UndacApp.Services;
using Microsoft.Maui.Controls;
using System.Xml.Serialization;

namespace UndacApp.Views
{
    public partial class UsersPage : ContentPage
    {
        IUsersService userService;
        ObservableCollection<User> users = new ObservableCollection<User>();

        public UsersPage()
        {
            InitializeComponent();
            userService = new UsersService();
            LoadUsers();
        }

        private async Task LoadUsers()
        {
            var userList = await userService.GetAll();
            users = new ObservableCollection<User>(userList);
            // Bind this collection to a ListView if you want to display the users
        }

        private void AddUserButton_Clicked(object sender, EventArgs e)
        {
            // Show admin credential fields
            adminCredentialsLayout.IsVisible = true;
        }

        private void ClearFilters()
        {
            txtName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            pickerAccessLevel.SelectedIndex = -1;
            adminCredentialsLayout.IsVisible = false;
        }
        private async void CheckCredentialsButton_Clicked(object sender, EventArgs e)
        {
            // Check credentials
            if (txtAdminUsername.Text == "systemAdmin" && txtAdminPassword.Text == "password123")
            {
                var newUser = new User
                {
                    Name = txtName.Text,
                    Email = txtEmail.Text,
                    AccessLevel = pickerAccessLevel.SelectedItem.ToString()
                };

                userService.Add(newUser);
                users.Add(newUser);

                // Clear the input fields
                ClearFilters();

                await DisplayAlert("Success", $"User {newUser.Name} added!", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Invalid credentials.", "OK");
            }
        }

    }
}
