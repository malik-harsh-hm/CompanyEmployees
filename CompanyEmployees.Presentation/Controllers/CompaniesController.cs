using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
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
        private readonly IServiceManager _service;
        public CompaniesController(IServiceManager service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult GetCompanies()
        {

            throw new Exception("zxcvbnbsdgf");


            var companies = _service.CompanyService.GetAllCompanies(trackChanges: false);
            return Ok(companies);

            // no try - catch as its handled globally
        }
    }
}
