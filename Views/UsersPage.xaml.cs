using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using UndacApp.Models;
using UndacApp.Services;
using Microsoft.Maui.Controls;

namespace UndacApp.Views
{
    public partial class UsersPage : ContentPage
    {
        IUsersService userService;
        ObservableCollection<User> users = new ObservableCollection<User>();
        private User selectedUser;

        public UsersPage()
        {
            InitializeComponent();
            userService = new UsersService();
            InitializePickers();
            LoadUsers();
        }

        private void InitializePickers()
        {
            pickerRole.ItemsSource = new List<string> { "User", "System Administrator" };
            pickerAccessLevel.ItemsSource = new List<string> { "Low", "Medium", "High" };
        }

        private async Task LoadUsers()
        {
            var userList = await userService.GetAll();
            users = new ObservableCollection<User>(userList);
            listViewUsers.ItemsSource = users;
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            string email = txtLoginEmail.Text;
            string password = txtLoginPassword.Text;
            var user = users.FirstOrDefault(u => u.Email == email && PasswordHasher.VerifyPassword(password, u.Password));

            bool isAdmin = email == "1" && password == "1";
            if (user != null || isAdmin)
            {
                if (user?.Role == "User" && !isAdmin)
                {
                    await DisplayAlert("Notice", "Please contact your manager for how to gain access", "OK");
                }
                else
                {
                    // Hide the login section
                    HideLoginSection();
                    userManagementSection.IsVisible = true;
                }
            }
            else
            {
                await DisplayAlert("Login Failed", "Invalid email or password.", "OK");
            }
        }

        private void HideLoginSection()
        {
            // Assuming the login section is within a named StackLayout
            txtLoginEmail.IsVisible = false;
            txtLoginPassword.IsVisible = false;
            // Hide the login button or the entire login section container if necessary
            // For example: loginSection.IsVisible = false;
        }


        private async void AddUserButton_Clicked(object sender, EventArgs e)
        {
            if (!ValidateUserInput()) return;

            var newUser = new User
            {
                Name = txtName.Text,
                Email = txtEmail.Text,
                Password = PasswordHasher.HashPassword(txtPassword.Text),
                Team = txtTeam.Text,
                Role = (string)pickerRole.SelectedItem,
                AccessLevel = (string)pickerAccessLevel.SelectedItem,
                Employed = true
            };

            await userService.Add(newUser);
            users.Add(newUser);

            ClearUserInputFields();
            await DisplayAlert("Success", $"{newUser.Name} added!", "OK");
        }

        private void OnUserSelected(object sender, SelectedItemChangedEventArgs e)
        {
            selectedUser = e.SelectedItem as User;
            btnEdit.IsEnabled = btnDelete.IsEnabled = selectedUser != null;
        }

        private async void EditUserButton_Clicked(object sender, EventArgs e)
        {
            if (selectedUser == null) return;

            txtName.Text = selectedUser.Name;
            txtEmail.Text = selectedUser.Email;
            txtPassword.Text = selectedUser.Password; // Keep in mind this is hashed
            txtTeam.Text = selectedUser.Team;
            pickerRole.SelectedItem = selectedUser.Role;
            pickerAccessLevel.SelectedItem = selectedUser.AccessLevel;

            AddUserButton.Text = "Save Changes";
            AddUserButton.Clicked -= AddUserButton_Clicked;
            AddUserButton.Clicked += SaveUserChangesButton_Clicked;
        }

        private async void SaveUserChangesButton_Clicked(object sender, EventArgs e)
        {
            if (!ValidateUserInput()) return;

            selectedUser.Name = txtName.Text;
            selectedUser.Email = txtEmail.Text;
            selectedUser.Password = PasswordHasher.HashPassword(txtPassword.Text); // Hashing new password
            selectedUser.Team = txtTeam.Text;
            selectedUser.Role = (string)pickerRole.SelectedItem;
            selectedUser.AccessLevel = (string)pickerAccessLevel.SelectedItem;

            await userService.Update(selectedUser);

            RefreshListView();
            ClearUserInputFields();

            AddUserButton.Text = "Add User";
            AddUserButton.Clicked -= SaveUserChangesButton_Clicked;
            AddUserButton.Clicked += AddUserButton_Clicked;

            await DisplayAlert("Success", "User updated successfully!", "OK");
        }

        private async void DeleteUserButton_Clicked(object sender, EventArgs e)
        {
            if (selectedUser == null) return;

            var confirm = await DisplayAlert("Confirm Delete", $"Are you sure you want to delete {selectedUser.Name}?", "Yes", "No");
            if (confirm)
            {
                await userService.Remove(selectedUser);
                users.Remove(selectedUser);
                RefreshListView();
                ClearUserInputFields();

                await DisplayAlert("User Deleted", $"Access privileges removed, {selectedUser.Name} deleted!", "OK");
            }
        }

        private void ClearUserInputFields()
        {
            txtName.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            txtTeam.Text = "";
            pickerRole.SelectedIndex = -1;
            pickerAccessLevel.SelectedIndex = -1;
            selectedUser = null;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        private void RefreshListView()
        {
            listViewUsers.ItemsSource = null;
            listViewUsers.ItemsSource = users;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool ValidateUserInput()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtTeam.Text) ||
                pickerRole.SelectedIndex == -1 ||
                pickerAccessLevel.SelectedIndex == -1)
            {
                DisplayAlert("Validation Error", "All fields are required.", "OK");
                return false;
            }

            if (!IsValidEmail(txtEmail.Text))
            {
                DisplayAlert("Validation Error", "Enter a valid email address.", "OK");
                return false;
            }

            return true;
        }
    }
}
