using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Interfaces.Infrastructure
{
    public interface ITableRepo
    {
        public IAdvert AddEntity(string payload, string table);

        public void DeleteEntity(string payload, string table);


    }
}
