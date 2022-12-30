using EnocaChallenge.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnocaChallenge.Domain.Entities
{
    public class Product : BaseEntity
    {
        public int Stock { get; set; }
        public double Price { get; set; }
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
