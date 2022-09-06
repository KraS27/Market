using Market.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entity
{
    public class Product
    {
        public int Id { get; set; }

        public string Caption { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

        public int ProductTypeId { get; set; }

        public ProductType ProductType { get; set; }
     
        public byte[] Image { get; set; }
    }
}
