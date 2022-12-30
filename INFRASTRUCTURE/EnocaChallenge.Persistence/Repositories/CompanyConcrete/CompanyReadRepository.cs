using EnocaChallenge.Application.Repositories.CompanyRep;
using EnocaChallenge.Domain.Entities;
using EnocaChallenge.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnocaChallenge.Persistence.Repositories.CompanyConcrete
{
    public class CompanyReadRepository : ReadRepository<Company>, ICompanyReadRepository
    {
        public CompanyReadRepository(ShopDbContext context) : base(context)
        {

        }
    }
}
