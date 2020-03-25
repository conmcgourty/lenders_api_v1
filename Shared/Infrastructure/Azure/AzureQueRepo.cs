using Shared.Interfaces;
using Shared.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using Shared.Models.Azure;
using Shared.Interfaces.Messages;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Azure
{
    public class AzureQueRepo : IQueueRepo, IStartUp
    {
        IConfiguration _configuration;
        CloudStorageAccount storageAccount;
        CloudQueueClient queueClient;
        CloudQueue queueCloud;


        public AzureQueRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //public void AddMessage(IMessageBase message, string Queue)
        //{
        //    storageAccount = CloudStorageAccount.Parse(this._configuration.GetConnectionString("Storage"));
        //    queueClient = storageAccount.CreateCloudQueueClient();
        //    CloudQueue queueCloud = queueClient.GetQueueReference(Queue);

        //    queueCloud.AddMessageAsync(new CloudQueueMessage(JsonConvert.SerializeObject(message))).Wait();
        //}

        public async Task<IMessage> AddMessage(IMessageBase message, string Queue)
        {
            storageAccount = CloudStorageAccount.Parse(this._configuration.GetConnectionString("Storage"));
            queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queueCloud = queueClient.GetQueueReference(Queue);

            await queueCloud.AddMessageAsync(new CloudQueueMessage(JsonConvert.SerializeObject(message)));

            return (IMessage)message;
        }

        public Task<IMessage> DeleteMessage(IMessageBase message, string Queue)
        {
            storageAccount = CloudStorageAccount.Parse(this._configuration.GetConnectionString("Storage"));
            queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queueCloud = queueClient.GetQueueReference(Queue);


            try
            {
                queueCloud.DeleteMessageAsync(message.Id, message.PopId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Threw an exception Trying to Delete Cloud Message ID: {message.Id}");
                throw ex;
            }
            

            //CloudQueueMessage cloudMessage = new CloudQueueMessage(JsonConvert.SerializeObject(message.Id));
                     
            //queueCloud.DeleteMessageAsync(cloudMessage);

            return null;
        }

        public void OnInIt()
        {
            var queues = _configuration.GetSection("Queues").GetChildren();

            storageAccount = CloudStorageAccount.Parse(this._configuration.GetConnectionString("Storage"));
            queueClient = storageAccount.CreateCloudQueueClient();

            // Create the Queue if it doesn't already exist. 
            foreach (var azureQueue in queues)
            {
                queueCloud = queueClient.GetQueueReference(azureQueue.Value.ToString());
                queueCloud.CreateIfNotExistsAsync();
            }


            //Console.WriteLine(configuration.GetSection("AzureQueue").GetChildren();)
        }

        //public IEnumerable<IMessageBase> ReadMessages(string Queue, int MessageCount)
        //{
        //    //List<IMessageBase> iMessagesCollection = new List<IMessageBase>();

        //    storageAccount = CloudStorageAccount.Parse(this._configuration.GetConnectionString("Storage"));
        //    queueClient = storageAccount.CreateCloudQueueClient();
        //    CloudQueue queueCloud = queueClient.GetQueueReference(Queue);

        //   var messages = queueCloud.GetMessagesAsync(MessageCount).Result;

        //    foreach (var cloudMessage in messages)
        //    {

        //        Console.WriteLine(cloudMessage.AsString);
        //        var message = JsonConvert.SerializeObject(cloudMessage.AsString);

        //        AzureMessage azureMessage = JsonConvert.DeserializeObject<AzureMessage>(cloudMessage.AsString);

        //        iMessagesCollection.Add(azureMessage);


        //       // IMessage order = new IMessage();
        //        //order = JsonConvert.DeserializeObject<AzureMessageOrder>(cloudMessage.AsString);

        //        //orders.Add(order);
        //        //queueCloud.DeleteMessageAsync(cloudMessage); // Removing message form Order Queue
        //    }

        //    return iMessagesCollection;
        //}

        //IEnumerable<IMessageBase> IQueueRepo.ReadMessages(string Queue, int MessageCount)
        //{
        //    throw new NotImplementedException();
        //}

        IEnumerable<IMessageBase> IQueueRepo.ReadMessages(string Queue, int MessageCount)
        {
            List<IMessageBase> iMessagesCollection = new List<IMessageBase>();

            storageAccount = CloudStorageAccount.Parse(this._configuration.GetConnectionString("Storage"));
            queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queueCloud = queueClient.GetQueueReference(Queue);

            var messages = queueCloud.GetMessagesAsync(MessageCount).Result;
            

            foreach (var cloudMessage in messages)
            {

                Console.WriteLine(cloudMessage.AsString);
                var message = JsonConvert.SerializeObject(cloudMessage.AsString);
                
                AzureMessage azureMessage = JsonConvert.DeserializeObject<AzureMessage>(cloudMessage.AsString);
                azureMessage.Id = cloudMessage.Id;
                azureMessage.PopId = cloudMessage.PopReceipt;
                
                iMessagesCollection.Add(azureMessage);

                #region MyRegion

                #endregion
                // IMessage order = new IMessage();
                //order = JsonConvert.DeserializeObject<AzureMessageOrder>(cloudMessage.AsString);

                //orders.Add(order);
                //queueCloud.DeleteMessageAsync(cloudMessage); // Removing message form Order Queue
            }

            return iMessagesCollection;
        }
    }
}
