using OS.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace OS.Domain.Models
{
    public class ProductCategory : DefaultTable
    {
        public bool? IsNew { get; set; }

    }
}
