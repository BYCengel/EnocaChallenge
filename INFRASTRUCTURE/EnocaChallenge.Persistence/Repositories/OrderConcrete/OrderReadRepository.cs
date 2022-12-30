using EnocaChallenge.Application.Repositories.OrderRep;
using EnocaChallenge.Domain.Entities;
using EnocaChallenge.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnocaChallenge.Persistence.Repositories.OrderConcrete
{
    public class OrderReadRepository : ReadRepository<Order>, IOrderReadRepository
    {
        public OrderReadRepository(ShopDbContext context) : base(context)
        {
        }
    }
}
