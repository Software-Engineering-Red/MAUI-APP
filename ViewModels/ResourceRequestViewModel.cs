using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UndacApp.Models;

namespace UndacApp.ViewModels
{
    public class ResourceRequestViewModel : BaseViewModel
    {
        private string _errorInput;
        private string _resourceInput;

        public string ErrorInput
        {
            get => _errorInput;
            set => SetProperty(ref _errorInput, value);
        }

        public string ResourceInput
        {
            get => _resourceInput;
            set => SetProperty(ref _resourceInput, value);
        }

        public ObservableCollection<ErrorResourcePair> ErrorResourcePairs { get; set; }
        public ICommand SubmitCommand { get; }

        public ResourceRequestViewModel()
        {
            ErrorResourcePairs = new ObservableCollection<ErrorResourcePair>();
            SubmitCommand = new Command(OnSubmit);
        }

        private void OnSubmit()
        {
            if (!string.IsNullOrWhiteSpace(ErrorInput) && !string.IsNullOrWhiteSpace(ResourceInput))
            {
                var errorResourcePair = new ErrorResourcePair
                {
                    Error = ErrorInput,
                    Resource = ResourceInput
                };

                ErrorResourcePairs.Add(errorResourcePair);

                // Clear the input fields after submission
                ErrorInput = string.Empty;
                ResourceInput = string.Empty;
            }
        }
    }
}
