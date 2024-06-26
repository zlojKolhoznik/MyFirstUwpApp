using System.Text.Json.Serialization;

namespace MyFirstUwpApp.Models
{
    public class Customer : ObservableObject
    {
        private string firstName;
        private string lastName;

        [JsonPropertyName("firstName")]
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

        [JsonPropertyName("lastName")]
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
    }
}
