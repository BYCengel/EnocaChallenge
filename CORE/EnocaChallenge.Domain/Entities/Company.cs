using EnocaChallenge.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnocaChallenge.Domain.Entities
{
    public class Company : BaseEntity
    {
        public bool IsApproved { get; set; }
        public DateTime OrderStartTime { get; set; }
        public DateTime OrderEndTime { get; set; }
    }
}
