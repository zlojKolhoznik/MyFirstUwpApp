using MyFirstUwpApp.Models;
using MyFirstUwpApp.Services.MessageService;
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
        private int savedCustomerIndex;
        private string firstName;
        private string lastName;

        public MainViewModel(IMessageService messageService)
        {
            customers = new ObservableCollection<Customer>();
            this.messageService = messageService;
            SavedCustomerIndex = -1;
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
        public int SavedCustomerIndex
        {
            get => savedCustomerIndex;
            set
            {
                if (savedCustomerIndex != value)
                {
                    savedCustomerIndex = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(EditingCustomer));
                }
            }
        }

        public Customer EditingCustomer => SavedCustomerIndex >= 0 ? Customers[SavedCustomerIndex] : new Customer();

        public RelayCommand AddCustomerCommand => new RelayCommand(HandleAddCustomer);

        public RelayCommand EnterEditingModeCommand => new RelayCommand(o => EnterEditingMode((Customer)o), CustomerExists);

        public RelayCommand ExitEditingModeCommand => new RelayCommand(_ => ExitEditingMode(), _ => SavedCustomerIndex >= 0);

        public RelayCommand RemoveCustomerCommand => new RelayCommand(HandleRemoveCustomer, CustomerExists);

        public RelayCommand ResetInputCommand => new RelayCommand(_ => ResetInput());

        private void HandleAddCustomer(object value)
        {
            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
            {
                messageService.SendErrorMessage("First name and last name are required");
            }
            else
            {
                CreateAndAddCustomer();
                ResetInput();
            }
        }

        private void CreateAndAddCustomer()
        {
            var customer = new Customer { FirstName = FirstName, LastName = LastName };
            Customers.Add(customer);
        }

        private void ResetInput()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
        }

        private void EnterEditingMode(Customer customer)
        {
            SavedCustomerIndex = Customers.IndexOf(customer);
        }

        private bool CustomerExists(object value)
        {
            return Customers.Contains((Customer)value);
        }

        private void ExitEditingMode()
        {
            SavedCustomerIndex = -1;
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
        }

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
    }
}
