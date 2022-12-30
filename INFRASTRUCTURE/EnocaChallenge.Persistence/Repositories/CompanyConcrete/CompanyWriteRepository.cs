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
    public class CompanyWriteRepository : WriteRepository<Company>, ICompanyWriteRepository
    {
        private readonly ICompanyReadRepository _companyReadRepository;
        public CompanyWriteRepository(ShopDbContext context, ICompanyReadRepository companyReadRepository) : base(context)
        {
            _companyReadRepository = companyReadRepository;
        }

        /// <summary>
        /// Updates the IsApproved property of the given company.
        /// </summary>
        /// <param name="company"></param>
        /// <param name="isApproved"></param>
        /// <returns></returns>
        public bool UpdateIsApproved(Company company, bool isApproved)
        {
            company.IsApproved = isApproved;
            return Update(company);
        }
        /// <summary>
        /// Updates the Business hours of the company with given new times.
        /// </summary>
        /// <param name="company"></param>
        /// <param name="newStartTime"></param>
        /// <param name="newEndTime"></param>
        /// <returns></returns>
        public bool UpdateBusinessHours(Company company, DateTime newStartTime, DateTime newEndTime)
        {
            company.OrderStartTime = newStartTime;
            company.OrderEndTime = newEndTime;

            return Update(company);
        }
    }
}
