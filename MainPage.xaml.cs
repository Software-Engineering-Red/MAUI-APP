using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        private readonly DatabaseOperations _dbOps;
        private Dictionary<int, string> _currentRecords;
        private Dictionary<int, string> _currentFilteredRecords;

        public MainPage()
        {
            Console.WriteLine("Application entry point reached.");

            InitializeComponent();

            // Initialize database operations
            // Ideally, this connection string should come from a config or environment setting.
            _dbOps = new DatabaseOperations($"Data Source={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "reference_values5.sqlite")}");

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

        private void PopulateRowPicker(String table)
        {
            try
            {
                var rows = _dbOps.GetAllRowNames(table);
                rows.Insert(0, "All");
                foreach (var row in rows)
                {
                    Console.WriteLine(row);
                }
                RowPicker.ItemsSource = rows;
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine(ex.Message);
                // Display error message or handle the exception
                DisplayAlert("Error", "Failed to load rows.", "OK");
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
                PopulateRowPicker(selectedTable);
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine(ex.Message);
                // Handle the exception
                DisplayAlert("Error", $"Failed to load records for table {selectedTable}.", "OK");
            }
        }

        private void OnFilterRecord(object sender, EventArgs e)
        {
            var selectedTable = GetSelectedTable();
            var filterValue = AddRecordFilter.Text;
            var columnToFilter = GetSelectedColumn();
            string mediaTypeFilter = (string)MediaTypePicker.SelectedItem;
            if (string.IsNullOrWhiteSpace(selectedTable) || string.IsNullOrWhiteSpace(filterValue) || string.IsNullOrWhiteSpace(columnToFilter) || string.IsNullOrWhiteSpace(mediaTypeFilter))
                return;

            try
            {
                _currentFilteredRecords = _dbOps.GetAllRecordsWithIdsAndFilter(selectedTable, columnToFilter, filterValue, mediaTypeFilter);
                RecordsListView.ItemsSource = _currentFilteredRecords;
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine(ex.Message);
                // Handle the exception
                DisplayAlert("Error", $"Failed to filter and load records for table {selectedTable}.", "OK");
            }
        }

        private string GetSelectedColumn() => RowPicker.SelectedItem?.ToString();

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
        /// <summary>
        /// Method to copy all stored data to the clipboard for a given table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCopyAllData(object sender, EventArgs e)
        {
            var selectedTable = GetSelectedTable();
            if (string.IsNullOrWhiteSpace(selectedTable))
                return;

            try
            {
                var allRecords = _dbOps.GetAllRecords(selectedTable);

                // Create a string containing all the records
                var recordsText = string.Join(Environment.NewLine, allRecords);

                // Copy the records to the clipboard
                Clipboard.SetTextAsync(recordsText);

                DisplayAlert("Success", "All data copied to clipboard.", "OK");
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine(ex.Message);
                // Handle the exception
                DisplayAlert("Error", $"Failed to copy data to clipboard for table {selectedTable}.", "OK");
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
