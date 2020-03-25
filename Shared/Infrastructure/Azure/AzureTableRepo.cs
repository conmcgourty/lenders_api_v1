using Newtonsoft.Json;
using Shared.Interfaces;
using Shared.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using Shared.Models.DomainModels;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Table;

namespace Shared.Infrastructure.Azure
{
    public class AzureTableRepo : IStartUp, ITableRepo
    {

        IConfiguration _configuration;
        CloudStorageAccount storageAccount;
        CloudTableClient tableClient;
        CloudTable tableCloud;

        public AzureTableRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IAdvert AddEntity(string payload, string table)
        {
            AdvertDomain advert = new Models.DomainModels.AdvertDomain(JsonConvert.DeserializeObject<AdvertDTO>(payload));

            storageAccount = CloudStorageAccount.Parse(this._configuration.GetConnectionString("Storage"));
            tableClient = storageAccount.CreateCloudTableClient();
            tableCloud = tableClient.GetTableReference(table);

            advert.PartitionKey = advert.Advert_Category.ToUpper();
            advert.RowKey = advert.Advert_Title.ToUpper().Trim() + "^" + advert.Advert_Contact + "^" + DateTime.Now.Millisecond.ToString();

            //AzureMessageConfirmation confirmation = new AzureMessageConfirmation(message.AgentID, message.OrderStatus, message.OrderId);

            TableOperation operation = TableOperation.InsertOrReplace(advert);
            
            try
            {
                tableCloud.ExecuteAsync(operation).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception thrown @ {DateTime.Now} : Where: {ex.StackTrace}");
                throw ex;
            }
          

            return advert;

        }

        public void DeleteEntity(string payload, string table)
        {
            throw new NotImplementedException();
        }

        public void OnInIt()
        {
            var azureTables = _configuration.GetSection("Tables").GetChildren();

            storageAccount = CloudStorageAccount.Parse(this._configuration.GetConnectionString("Storage"));
            tableClient = storageAccount.CreateCloudTableClient();

            // Create the Table if it doesn't already exist. 
            foreach (var azureTable in azureTables)
            {
                tableCloud = tableClient.GetTableReference(azureTable.Value.ToString());
                tableCloud.CreateIfNotExistsAsync();
            }
        }
    }
}
