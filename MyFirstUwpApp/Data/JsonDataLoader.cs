using MyFirstUwpApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyFirstUwpApp.Data
{
    public class JsonDataLoader
    {
        private string _fileName;

        public JsonDataLoader(string fileName)
        {
            _fileName = fileName;
        }

        public IEnumerable<Customer> LoadCustomers()
        {
            var json = System.IO.File.ReadAllText(_fileName);
            return JsonSerializer.Deserialize<List<Customer>>(json);
        }

        public void SaveCustomers(IEnumerable<Customer> customers)
        {
            var json = JsonSerializer.Serialize(customers);
            System.IO.File.WriteAllText(_fileName, json);
        }
    }
}
