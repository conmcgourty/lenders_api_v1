using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Interfaces.Messages
{
    public interface IMessageBase
    {
        public string Command { get; set; }
        public string PartKey { get; set; }
        public string RowKey { get; set; }
        public string Payload { get; set; }
        public string Id { get; set; }
        public string PopId { get; set; }

    }
}
