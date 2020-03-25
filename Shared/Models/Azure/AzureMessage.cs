using Shared.Interfaces.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.Azure
{
    public class AzureMessage : IMessage
    {
        public AzureMessage()
        {         
          
        }

        public string Command { get; set; }
        public string PartKey { get; set ; }
        public string RowKey { get; set; }
        public string Payload { get; set; }
        public string Id { get; set; }
        public string PopId { get; set; }
    }
}
