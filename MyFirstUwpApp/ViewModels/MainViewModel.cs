using MyFirstUwpApp.Models;
using MyFirstUwpApp.Services.MessageService;
using System;
using System.Collections.ObjectModel;
using Windows.Foundation.Collections;

namespace MyFirstUwpApp.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly IMessageService _messageService;

        private ObservableCollection<Customer> _customers;
        private Customer _selectedCustomer;
        private string _firstName;
        private string _lastName;

        public MainViewModel(IMessageService messageService)
        {
            _customers = new ObservableCollection<Customer>();
            SelectedCustomer = Customer.Empty;
            _messageService = messageService;
        }

        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set
            {
                if (_customers != value)
                {
                    _customers = value;
                    OnPropertyChanged();
                }
            }
        }

        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                if (_selectedCustomer != value)
                {
                    _selectedCustomer = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged();
                }
            }
        }


        public string LastName
        {
            get => _lastName;
            set 
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged();
                }
            }
        }


        public RelayCommand AddCustomerCommand => new RelayCommand(AddCustomer);

        public RelayCommand RemoveCustomerCommand => new RelayCommand(RemoveCustomer, CustomerExists);

        public RelayCommand ClearSelectionCommand => new RelayCommand(ClearSelection);

        public RelayCommand SelectCustomerCommand => new RelayCommand(SelectCustomer, CustomerExists);

        private void AddCustomer(object value)
        {
            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
            {
                _messageService.ShowErrorMessage("First name and last name are required");
                return;
            }
            if (!(value is Customer))
            {
                _messageService.ShowErrorMessage("Couldn't add a new customer due to unknown error");
                return;
            }

            var customer = (Customer)value;
            customer.FirstName = FirstName;
            customer.LastName = LastName;
            if (!Customers.Contains(customer))
            {
                Customers.Add(customer);
            }
            ClearSelection();
        }

        private async void RemoveCustomer(object value)
        {
            var response = await _messageService.ShowPrompt("Are you sure you want to remove the customer?", PromptType.YesNo);
            if (response != MessageResponse.Yes)
            {
                return;
            }

            if (!(value is Customer))
            {
                _messageService.ShowErrorMessage("Couldn't remove the customer due to unknown error");
                return;
            }
            var customer = (Customer)value;
            Customers.Remove(customer);
            ClearSelection();
        }

        private bool CustomerExists(object value)
        {
            return value is Customer customer && Customers.Contains(customer);
        }

        private void ClearSelection(object value = null)
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            SelectedCustomer = Customer.Empty;
        }

        private void SelectCustomer(object value)
        {
            if (!(value is Customer))
            {
                _messageService.ShowErrorMessage("Couldn't select the customer due to unknown error");
                return;
            }
            SelectedCustomer = (Customer)value;
            FirstName = SelectedCustomer.FirstName;
            LastName = SelectedCustomer.LastName;
        }
    }
}
