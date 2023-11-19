using UndacApp.Models;
using UndacApp.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace UndacApp.Views
{
    /// <summary>
    /// Represents the RotaPage class, which is responsible for managing and displaying Rota data.
    /// </summary>
    public partial class RotaPage : ContentPage
    {
        private Rota _selectedRota = null;
        private IRotaService _rotaService;
        private ObservableCollection<Rota> _rotaCollection = new ObservableCollection<Rota>();

        /// <summary>
        /// Initializes a new instance of the RotaPage class.
        /// </summary>
        public RotaPage()
        {
            InitializeComponent();
            BindingContext = new Rota();
            this._rotaService = new RotaService();

            Task.Run(async () => await LoadRotas());
            RotaNameEditor.Text = "";
        }

        /// <summary>
        /// Loads the list of Rotas asynchronously.
        /// </summary>
        private async Task LoadRotas()
        {
            _rotaCollection = new ObservableCollection<Rota>(await _rotaService.GetAll());
            RotaListView.ItemsSource = _rotaCollection;
        }

        /// <summary>
        /// Handles the click event of the Save button.
        /// </summary>
        private void OnSaveButtonClicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(RotaNameEditor.Text)) return;

            if (_selectedRota == null)
            {
                AddNewRota();
            }
            else
            {
                UpdateExistingRota();
            }

            ClearUIComponents();
        }

        /// <summary>
        /// Adds a new Rota to the collection and the data service.
        /// </summary>
        private void AddNewRota()
        {
            var newRota = CreateRotaFromInput();
            _rotaService.Add(newRota);
            _rotaCollection.Add(newRota);
        }

        /// <summary>
        /// Updates an existing Rota in the collection and the data service.
        /// </summary>
        private void UpdateExistingRota()
        {
            _selectedRota.Name = RotaNameEditor.Text;
            _rotaService.Update(_selectedRota);
            var existingRota = _rotaCollection.FirstOrDefault(x => x.ID == _selectedRota.ID);
            existingRota.Name = RotaNameEditor.Text;
        }

        /// <summary>
        /// Creates a new Rota object from the input.
        /// </summary>
        /// <returns>A new Rota object.</returns>
        private Rota CreateRotaFromInput()
        {
            return new Rota() { Name = RotaNameEditor.Text };
        }

        /// <summary>
        /// Clears the UI components and resets the selected Rota.
        /// </summary>
        private void ClearUIComponents()
        {
            RotaListView.SelectedItem = null;
            RotaNameEditor.Text = "";
            _selectedRota = null;
        }

        /// <summary>
        /// Handles the click event of the Delete button.
        /// </summary>
        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (RotaListView.SelectedItem == null)
            {
                await DisplayAlert("No Rota Selected", "Select the rota you want to delete from the list", "OK");
                return;
            }

            await _rotaService.Remove(_selectedRota);
            _rotaCollection.Remove(_selectedRota);

            ClearUIComponents();
        }

        /// <summary>
        /// Handles the selection of a Rota item in the list.
        /// </summary>
        private void OnRotaItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _selectedRota = e.SelectedItem as Rota;
            if (_selectedRota == null) return;

            RotaNameEditor.Text = _selectedRota.Name;
        }
    }
}
