using Entities.Exceptions;
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
    [Route("api/companies/{companyId}/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public EmployeesController(IServiceManager service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult GetEmployees(Guid companyId)
        {
            var employees = _service.EmployeeService.GetEmployees(companyId, trackChanges: false);
            return Ok(employees);

            // no try - catch as its handled globally
        }
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployee(Guid companyId, Guid id)
        {
            var employee = _service.EmployeeService.GetEmployee(companyId, id, trackChanges: false);
            return Ok(employee);

            // no try - catch as its handled globally
        }
    }
}
