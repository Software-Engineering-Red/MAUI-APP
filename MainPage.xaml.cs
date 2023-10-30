using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        private readonly DatabaseOperations _dbOps;
        private Dictionary<int, string> _currentRecords;
        

        public MainPage()
        {
            Console.WriteLine("Application entry point reached.");

            InitializeComponent();

            // Initialize database operations
            // Ideally, this connection string should come from a config or environment setting.
            _dbOps = new DatabaseOperations($"Data Source={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "reference_values.sqlite")}");

            // Populate table picker
            PopulateTablePicker();
        }

        private async void OnTestCrudOperations(object sender, EventArgs e)
        {
            var tableName = GetSelectedTable();
            var testRecordName = "TestRecord";
            var updatedTestRecordName = "UpdatedTestRecord";

            if (string.IsNullOrWhiteSpace(tableName))
            {
                await DisplayAlert("Warning", "No table selected for CRUD operations test.", "OK");
                return;
            }

            // Insert new record
            try
            {
                _dbOps.AddRecord(tableName, testRecordName);
                await DisplayAlert("Success", $"Successfully inserted record '{testRecordName}' into {tableName}.", "OK");
                
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to insert record into {tableName}. Error: {ex.Message}", "OK");
                return;
            }

            // Retrieve the inserted record's ID
            var insertedRecordId = _dbOps.GetRecordIdByName(tableName, testRecordName);

            // Update the inserted record
            try
            {
                _dbOps.UpdateRecord(tableName, insertedRecordId, updatedTestRecordName);
                await DisplayAlert("Success", $"Successfully updated record in {tableName} to '{updatedTestRecordName}'.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to update record in {tableName}. Error: {ex.Message}", "OK");
                return;
            }

            // Delete the updated record
            try
            {
                _dbOps.DeleteRecord(tableName, insertedRecordId);
                await DisplayAlert("Success", $"Successfully deleted record from {tableName}.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to delete record from {tableName}. Error: {ex.Message}", "OK");
                return;
            }

            RefreshRecordsList();
            
        }



        private void PopulateTablePicker()
        {
            try
            {
                var tables = _dbOps.GetAllTableNames();
                foreach (var table in tables)
                {
                    Console.WriteLine(table);
                }
                tables.Remove("sqlite_sequence");
                TablePicker.ItemsSource = tables;
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine(ex.Message);
                // Display error message or handle the exception
                DisplayAlert("Error", "Failed to load tables.", "OK");
            }
        }

        private void OnTableSelected(object sender, EventArgs e)
        {
            var selectedTable = GetSelectedTable();
            if (string.IsNullOrWhiteSpace(selectedTable))
                return;

            try
            {
                _currentRecords = _dbOps.GetAllRecordsWithIds(selectedTable);
                RecordsListView.ItemsSource = _currentRecords;
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine(ex.Message);
                // Handle the exception
                DisplayAlert("Error", $"Failed to load records for table {selectedTable}.", "OK");
            }
        }

        private void OnRecordSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is KeyValuePair<int, string> selectedRecord)
            {
                UpdateRecordEntry.Text = selectedRecord.Value;
            }
        }


        private void OnAddRecord(object sender, EventArgs e)
        {
            var tableName = GetSelectedTable();
            var recordName = AddRecordEntry.Text;

            if (IsValidInput(tableName, recordName))
            {
                try
                {
                    _dbOps.AddRecord(tableName, recordName);
                    RefreshRecordsList();
                    
                }
                catch (Exception ex)
                {
                    // Log error
                    Console.WriteLine(ex.Message);
                    // Handle the exception
                    DisplayAlert("Error", $"Failed to add record to table {tableName}.", "OK");
                }

            }
        }

        private void OnUpdateRecord(object sender, EventArgs e)
        {
            if (RecordsListView.SelectedItem is KeyValuePair<int, string> selectedRecord)
            {
                var tableName = GetSelectedTable();
                var newName = UpdateRecordEntry.Text;

                if (IsValidInput(tableName, newName))
                {
                    try
                    {
                        _dbOps.UpdateRecord(tableName, selectedRecord.Key, newName);
                        RefreshRecordsList();
                        
                    }
                    catch (Exception ex)
                    {

                        // Log error
                        Console.WriteLine(ex.Message);
                        // Handle the exception
                        DisplayAlert("Error", $"Failed to update record in table {tableName}.", "OK");
                    }
                }
            }
        }

        private void OnDeleteRecord(object sender, EventArgs e)
        {
            if (RecordsListView.SelectedItem is KeyValuePair<int, string> selectedRecord)
            {
                var tableName = GetSelectedTable();

                if (IsValidInput(tableName))
                {
                    try
                    {
                        _dbOps.DeleteRecord(tableName, selectedRecord.Key);
                        RefreshRecordsList();
                        
                    }
                    catch (Exception ex)
                    {
                        // Log error
                        Console.WriteLine(ex.Message);
                        // Handle the exception
                        DisplayAlert("Error", $"Failed to delete record from table {tableName}.", "OK");
                    }
                }
            }
        }

        private string GetSelectedTable() => TablePicker.SelectedItem?.ToString();

        private bool IsValidInput(params string[] inputs)
        {
            foreach (var input in inputs)
            {
                if (string.IsNullOrWhiteSpace(input))
                    return false;
            }
            return true;
        }

        private void RefreshRecordsList() => OnTableSelected(null, EventArgs.Empty);

    }
}
