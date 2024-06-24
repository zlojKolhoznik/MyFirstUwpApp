using System.Text.Json.Serialization;

namespace MyFirstUwpApp.Models
{
    public class Customer : ObservableObject
    {
        private string _firstName;
        private string _lastName;

        [JsonPropertyName("firstName")]
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

        [JsonPropertyName("lastName")]
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

        public static Customer Empty => new Customer { FirstName = string.Empty, LastName = string.Empty };
    }
}
