using Microsoft.WindowsAzure.Storage.Table;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.DomainModels
{
    public class AdvertDomain : TableEntity, IAdvert
    {        
        public string Advert_ID { get; set; }
        public string Advert_Title { get; set; }
        public string Advert_Desc { get; set; }
        public string Advert_Thumbnail { get; set; }
        public string Advert_Category { get; set; }
        public string Advert_Locaton { get; set; }
        public string Advert_Contact { get; set; }
        public List<IImage> Images { get; set; }
        public string PartionKey { get; set; }
        public string Advert_OneWordDescription { get; set; }

        public AdvertDomain() { }

        public AdvertDomain(AdvertDTO advert)
        {
            this.Advert_Category = advert.Category;
            this.Advert_Contact = advert.Contact;
            this.Advert_Desc = advert.Description;
            this.Advert_Locaton = advert.Location;
            this.Advert_Title = advert.Title;
            this.Advert_OneWordDescription = advert.OneWordDescription;
        }

    }

    public class AdvertDTO
    {
        public string Category { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string OneWordDescription { get; set; }
        public string Contact { get; set; }
        public bool Check { get; set; }
    }

    
}
