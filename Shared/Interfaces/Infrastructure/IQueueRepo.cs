using Shared.Interfaces.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces.Infrastructure
{
    public interface IQueueRepo
    {
        Task<IMessage> AddMessage(IMessageBase message, string Queue);
        IEnumerable<IMessageBase> ReadMessages(string Queue, int MessageCount);

        Task<IMessage> DeleteMessage(IMessageBase message, string Queue);

    
    }
}
