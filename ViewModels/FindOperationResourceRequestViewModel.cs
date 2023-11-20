﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Xml.Linq;
using UndacApp.Models;
using UndacApp.Services;

namespace UndacApp.ViewModels
{
	public class FindOperationResourceRequestViewModel : INotifyPropertyChanged
	{
		private readonly IOperationService operationService;

		public ObservableCollection<HighlightedOperation> OperationList { get; set; }

		private HighlightedOperation selectedOperation;

		private string filterType;
		private string filterPurpose;
		private string filterLocation;
		private string filterCreatedBy;
		private string filterName;



		public string FilterType
		{
			get => filterType;
			set
			{
				if (filterType != value)
				{
					filterType = value;
					OnPropertyChanged(nameof(FilterType));
					ApplyFilter();
				}
			}
		}

		public string FilterPurpose
		{
			get => filterPurpose;
			set
			{
				if (filterPurpose != value)
				{
					filterPurpose = value;
					OnPropertyChanged(nameof(FilterPurpose));
					ApplyFilter();
				}
			}
		}

		public string FilterLocation
		{
			get => filterLocation;
			set
			{
				if (filterLocation != value)
				{
					filterLocation = value;
					OnPropertyChanged(nameof(FilterLocation));
					ApplyFilter();
				}
			}
		}

		public string FilterCreatedBy
		{
			get => filterCreatedBy;
			set
			{
				if (filterCreatedBy != value)
				{
					filterCreatedBy = value;
					OnPropertyChanged(nameof(FilterCreatedBy));
					ApplyFilter();
				}
			}
		}

		public string FilterName
		{
			get => filterName;
			set
			{
				if (filterName != value)
				{
					filterName = value;
					OnPropertyChanged(nameof(FilterName));
					ApplyFilter();
				}
			}
		}

		public HighlightedOperation SelectedOperation
		{
			get => selectedOperation;
			set
			{
				if (selectedOperation != value)
				{
					selectedOperation = value;
					OnPropertyChanged(nameof(SelectedOperation));
				}
			}
		}


		public FindOperationResourceRequestViewModel()
		{
			operationService = new OperationService();

			Task.Run(async () => await LoadOperations());
		}

		private async void ApplyFilter()
		{
			await LoadOperations();
		}
		private async Task LoadOperations()
		{
			ObservableCollection<HighlightedOperation> operationData = new ObservableCollection<HighlightedOperation>(await GetFilteredOperations());
			OperationList = operationData;
			OnPropertyChanged(nameof(OperationList));
		}

		private async Task<List<HighlightedOperation>> GetFilteredOperations()
		{
			var allOperations = await operationService.GetAll();

			var filteredOperations = allOperations.Where(operation =>
				(string.IsNullOrWhiteSpace(FilterType) || operation.Type?.IndexOf(FilterType, StringComparison.OrdinalIgnoreCase) >= 0) &&
				(string.IsNullOrWhiteSpace(FilterPurpose) || operation.Purpose?.IndexOf(FilterPurpose, StringComparison.OrdinalIgnoreCase) >= 0) &&
				(string.IsNullOrWhiteSpace(FilterLocation) || operation.Location?.IndexOf(FilterLocation, StringComparison.OrdinalIgnoreCase) >= 0) &&
				(string.IsNullOrWhiteSpace(FilterCreatedBy) || operation.CreatedBy?.IndexOf(FilterCreatedBy, StringComparison.OrdinalIgnoreCase) >= 0) &&
				(string.IsNullOrWhiteSpace(FilterName) || operation.Name?.IndexOf(FilterName, StringComparison.OrdinalIgnoreCase) >= 0)
			);

			var highlightedOperations = filteredOperations
				.Select(operation => new HighlightedOperation
				{
					Operation = operation,
					IsHighlighted = checkForRequest(operation)
				})
				.ToList();

			return highlightedOperations;
		}

		private bool checkForRequest(Operation operation)
		{
			if (operation.OperationalTeams == null)
				return false;
			if (operation.OperationalTeams.Any(team => team.OperationResourceRequests == null))
				return operation.OperationalTeams.Any(team => team.OperationResourceRequests.Count != 0);
			return false;
		}



		public event PropertyChangedEventHandler? PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}


	public class HighlightedOperation
	{
		public Operation Operation { get; set; }
		public bool IsHighlighted { get; set; }
	}
}
