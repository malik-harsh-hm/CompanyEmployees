using Application.Commands.Company;
using Application.Queries.Company;
using CompanyEmployees.Filters.ActionFilters;
using Entities.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployees.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CompaniesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        // [Authorize]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _mediator.Send(new GetCompaniesQuery(trackChanges: false));
            return Ok(companies);
        }
        [HttpGet]
        [Route("{companyId:guid}", Name = "GetCompany")]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetCompany(Guid companyId)
        {
            var company = await _mediator.Send(new GetCompanyQuery(id: companyId, trackChanges: false));
            return Ok(company);
        }

        [HttpPost]
        [ValidationActionFilter]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDto company)
        {
            if (company is null)
                return BadRequest("CompanyForCreationDto object is null");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var createdCompany = await _mediator.Send(new CreateCompanyCommand(company));

            return CreatedAtRoute("GetCompany", new
            {
                companyId = createdCompany.Id
            }, createdCompany);
        }

        [HttpDelete("{companyId:guid}")]
        public async Task<IActionResult> DeleteCompany(Guid companyId)
        {
            await _mediator.Send(new DeleteCompanyCommand(companyId, trackChanges: false));
            return NoContent();
        }

        [HttpPut("{companyId:guid}")]
        [ValidationActionFilter]
        public async Task<IActionResult> UpdateCompany(Guid companyId, [FromBody] CompanyForUpdationDto company)
        {
            if (company is null)
                return BadRequest("CompanyForUpdateDto object is null");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _mediator.Send(new UpdateCompanyCommand(companyId, company, trackChanges: true));
            return NoContent();
        }
    }
}
