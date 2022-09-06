using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entity
{
    public class ProductType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Product> Product { get; set; }
    }
}
