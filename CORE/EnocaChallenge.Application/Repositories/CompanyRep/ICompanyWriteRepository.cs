using EnocaChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnocaChallenge.Application.Repositories.CompanyRep
{
    public interface ICompanyWriteRepository : IWriteRepository<Company>
    {
        public bool UpdateIsApproved(Company company, bool isApproved);
        /*public Task<bool> UpdateStartTimeAsync(Company company, string newStartTime);
        public Task<bool> UpdateEndTimeAsync(Company company, string newEndTime);*/
        public bool UpdateBusinessHours(Company company, DateTime newStartTime, DateTime newEndTime);
    }
}
