using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Interfaces
{
    public interface IAdvert 
    {
        public string PartionKey { get; set; }
        public string RowKey { get; set; }

        public string Advert_ID { get; set; }
        public string Advert_Title { get; set; }
        public string Advert_Desc { get; set; }
        public string Advert_Thumbnail { get; set; }
        public string Advert_Category { get; set; }
        public string Advert_Locaton { get; set; }
        public string Advert_Contact { get; set; }
        public List<IImage> Images { get; set; }
    }
}
