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
    public class TokenController : ControllerBase
    {
        private readonly IServiceManager _service;
        public TokenController(IServiceManager service)
        {
            _service = service;
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto) 
        { 
            var tokenDtoToReturn = await _service.AuthenticationService.RefreshToken(tokenDto); 
            return Ok(tokenDtoToReturn); 
        }
    }
}
