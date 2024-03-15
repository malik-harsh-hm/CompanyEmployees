using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    // Concrete repository
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateCompany(Company company)
        {
           Create(company);
        }

        public void DeleteCompany(Company company)
        {
            Delete(company);
        }

        public async Task<IEnumerable<Company>> GetAllCompanies(bool trackChanges)
        {
            var companies = await FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToListAsync();

            return companies;
        }

        public async Task<Company> GetCompany(Guid companyId, bool trackChanges)
        {
            var company = await FindByCondition(c => c.Id == companyId, trackChanges)
                .SingleOrDefaultAsync();
            return company;
        }
    }
}
