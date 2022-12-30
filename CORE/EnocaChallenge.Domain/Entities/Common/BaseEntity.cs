using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnocaChallenge.Domain.Entities.Common
{
    public class BaseEntity
    {
        public int Id { get; set; } //Id is an int for simplicities sake. I would use GUID in real life conditions.
        public string Name { get; set; }
    }
}
