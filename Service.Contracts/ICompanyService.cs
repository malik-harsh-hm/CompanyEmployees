

using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
        CompanyDto GetCompany(Guid companyId, bool trackChanges);
        CompanyDto CreateCompany(CompanyForCreationDto companyForCreation);
        void DeleteCompany(Guid companyId, bool trackChanges);
        void UpdateCompany(Guid companyId, CompanyForUpdationDto companyForUpdation, bool trackChanges);
    }
}
