using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkHelpLine.Model
{
    public class CategoryProduct
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        //relationship

        public Product Product { get; set; }
        public Category Category { get; set; }
    }
}
