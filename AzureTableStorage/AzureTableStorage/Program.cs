using System;
using AzureTableStorage.Customer;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureTableStorage
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnection"));

            #region Table Storage Stuff

            //// Create Table
            //CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            //CloudTable table = tableClient.GetTableReference("customers");
            //table.CreateIfNotExists();

            //// Create Customer
            ////CreateCustomer(table, new CustomerUs.CustomerUs("todelete", "delete@icloud.com"));
            ////GetAll(table);

            //// Update
            ////CustomerUs.CustomerUs customerUsToUpdate = GetCustomer(table, "US", "rajhun@icloud.com");
            ////customerUsToUpdate.Name = "Updated Rajhun";
            ////UpdateCustomer(table, customerUsToUpdate);

            //// Delete
            ////var customerToDelete = GetCustomer(table, "US", "delete@icloud.com");
            ////DeleteCustomer(table, customerToDelete);
            ////GetAll(table);


            //// Batch Operation
            //TableBatchOperation batch = new TableBatchOperation();
            //var cus1 = new CustomerUs("vanisha", "vanisha@icloud.com");
            //var cus2 = new CustomerUs("niven", "niven@icloud.com");
            //var cus3 = new CustomerUs("heena", "heena@icloud.com");
            //batch.Insert(cus1);
            //batch.Insert(cus2);
            //batch.Insert(cus3);
            //table.ExecuteBatch(batch);

            //GetAll(table);

            //Console.ReadKey(); 

            #endregion

            // Create a queue
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference("tasks");
            queue.CreateIfNotExists();

            // Add a message with expiry date
            //CloudQueueMessage queueMessage = new CloudQueueMessage("Hello World Yet Again");
            //var time = new TimeSpan(24, 0, 0);
            //queue.AddMessage(queueMessage, time, null);

            // One way to retreive and delte
            CloudQueueMessage cloudQueueMessage = queue.GetMessage();
            Console.WriteLine(cloudQueueMessage.AsString);
            queue.DeleteMessage(cloudQueueMessage);

            // Peek message
            //CloudQueueMessage cloudQueueMessage = queue.PeekMessage();


            Console.WriteLine("Done");
            Console.ReadKey();
        }

        #region Table Storage

        public static void CreateCustomer(CloudTable table, CustomerUs customer)
        {
            var inserOperation = TableOperation.Insert(customer);
            table.Execute(inserOperation);
        }

        public static CustomerUs GetCustomer(CloudTable table, string partitionKey, string rowKey)
        {
            var retreive = TableOperation.Retrieve<CustomerUs>(partitionKey, rowKey);
            var tableResult = table.Execute(retreive);
            var customer = (CustomerUs) tableResult.Result;
            return customer;
        }

        public static void UpdateCustomer(CloudTable table, CustomerUs customer)
        {
            var update = TableOperation.Replace(customer);
            table.Execute(update);
        }

        public static void DeleteCustomer(CloudTable table, CustomerUs customer)
        {
            var delete = TableOperation.Delete(customer);
            table.Execute(delete);
        }

        public static void GetAll(CloudTable table)
        {
            var query = new TableQuery<CustomerUs>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "US"));


            foreach (var customer in table.ExecuteQuery(query))
                Console.WriteLine(customer.Name);
        }

        #endregion
    }
}