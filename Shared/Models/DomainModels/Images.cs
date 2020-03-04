using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.DomainModels
{
    public class Images : IImage
    {
        public string Image_Name { get; set; }
        public string Image_Content { get; set; }
    }
}
