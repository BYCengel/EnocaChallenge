using EnocaChallenge.Application.Repositories.ProductRep;
using EnocaChallenge.Domain.Entities;
using EnocaChallenge.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnocaChallenge.Persistence.Repositories.ProductConcrete
{
    public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
    {
        public ProductWriteRepository(ShopDbContext context) : base(context)
        {
        }
    }
}
