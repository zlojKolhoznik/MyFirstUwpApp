using System.ComponentModel;
using System.Text.Json.Serialization;

namespace MyFirstUwpApp.Models
{
    public class Customer : ObservableObject, IEditableObject
    {
        private string firstName;
        private string lastName;
        private string guid;

        private string savedFirstName;
        private string savedLastName;

        public Customer()
        {
            guid = System.Guid.NewGuid().ToString();
        }

        public string Guid
        {
            get => guid;
            set
            {
                if (guid != value)
                {
                    guid = value;
                    OnPropertyChanged();
                }
            }
        }

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

        public void BeginEdit()
        {
            savedFirstName = FirstName;
            savedLastName = LastName;
        }

        public void CancelEdit()
        {
            FirstName = savedFirstName;
            LastName = savedLastName;
        }

        public void EndEdit()
        {
            savedFirstName = null;
            savedLastName = null;
        }
    }
}
