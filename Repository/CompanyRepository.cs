﻿using Contracts;
using Entities.Models;
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

        public IEnumerable<Company> GetAllCompanies(bool trackChanges)
        {
            var companies = FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToList();

            return companies;
        }

        public Company GetCompany(Guid companyId, bool trackChanges)
        {
            var company = FindByCondition(c => c.Id == companyId, trackChanges)
                .SingleOrDefault();
            return company;
        }
    }
}
