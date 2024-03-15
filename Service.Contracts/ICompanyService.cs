

using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDto>> GetAllCompanies(bool trackChanges);
        Task<CompanyDto> GetCompany(Guid companyId, bool trackChanges);
        Task<CompanyDto> CreateCompany(CompanyForCreationDto companyForCreation);
        Task DeleteCompany(Guid companyId, bool trackChanges);
        Task UpdateCompany(Guid companyId, CompanyForUpdationDto companyForUpdation, bool trackChanges);
    }
}
