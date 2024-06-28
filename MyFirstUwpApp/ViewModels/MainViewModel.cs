using MyFirstUwpApp.Models;
using MyFirstUwpApp.Services.MessageService;
using System;
using System.Collections.ObjectModel;
using System.Text.Json;
using Windows.Storage;

namespace MyFirstUwpApp.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private const string CustomersSettingsKey = "customers-JSON";
        private readonly IMessageService messageService;

        private ObservableCollection<Customer> customers;
        private Customer selectedCustomer;
        private Customer savedCustomer;
        private int savedCustomerIndex;
        private string firstName;
        private string lastName;

        public MainViewModel(IMessageService messageService)
        {
            customers = new ObservableCollection<Customer>();
            SelectedCustomer = new Customer();
            this.messageService = messageService;
        }

        public ObservableCollection<Customer> Customers
        {
            get => customers;
            set
            {
                if (customers != value)
                {
                    customers = value;
                    OnPropertyChanged();
                }
            }
        }

        public Customer SelectedCustomer
        {
            get => selectedCustomer;
            set
            {
                if (selectedCustomer != value)
                {
                    selectedCustomer = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FirstName
        {
            get => firstName;
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    OnPropertyChanged();
                }
            }
        }


        public string LastName
        {
            get => lastName;
            set 
            {
                if (lastName != value)
                {
                    lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand AddCustomerCommand => new RelayCommand(HandleAddCustomer);

        public RelayCommand RemoveCustomerCommand => new RelayCommand(HandleRemoveCustomer, CustomerExists);

        public RelayCommand ClearSelectionCommand => new RelayCommand(ClearSelection);

        public void LoadCustomers()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values[CustomersSettingsKey] is string customersJson)
            {
                Customers = JsonSerializer.Deserialize<ObservableCollection<Customer>>(customersJson);
            }
        }

        public void SaveCustomers()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values[CustomersSettingsKey] = JsonSerializer.Serialize(Customers);
        }

        public void SaveCustomerState(Customer customer)
        {
            savedCustomer = new Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };
            savedCustomerIndex = Customers.IndexOf(customer);
        }

        public void RestoreSavedCustomerState()
        {
            if (!(savedCustomer is null))
            {
                Customer toRestore = Customers[savedCustomerIndex];
                toRestore.FirstName = savedCustomer.FirstName;
                toRestore.LastName = savedCustomer.LastName;
                savedCustomer = null;
            }
        }

        public void ReleaseSavedCustomerState()
        {
            savedCustomer = null;
        }

        private void HandleAddCustomer(object value)
        {
            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
            {
                messageService.SendErrorMessage("First name and last name are required");
            }
            else
            {
                AddCustomer((Customer)value);
                ClearSelection();
            }
        }

        private void AddCustomer(Customer customer)
        {
            customer.FirstName = FirstName;
            customer.LastName = LastName;
            Customers.Add(customer);
        }

        private async void HandleRemoveCustomer(object value)
        {
            var response = await messageService.SendPromptAsync("Are you sure you want to remove the customer?");
            if (response == MessageResponse.Yes)
            {
                RemoveCustomer((Customer)value);
            }
        }

        private void RemoveCustomer(Customer customer)
        {
            Customers.Remove(customer);
            ClearSelection();
        }

        private bool CustomerExists(object value)
        {
            return Customers.Contains((Customer)value);
        }

        private void ClearSelection(object value = null)
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            SelectedCustomer = new Customer();
        }
    }
}
