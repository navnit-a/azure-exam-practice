using Microsoft.WindowsAzure.Storage.Table;

namespace AzureTableStorage.Customer
{
    public class CustomerUs : TableEntity
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }

        public CustomerUs()
        {
                
        }

        public CustomerUs(string name, string email)
        {
            Name = name;
            EmailAddress = email;
            PartitionKey = "US"; // This comes from table entity
            RowKey = email; // This comes from table entity
        }
    }
}