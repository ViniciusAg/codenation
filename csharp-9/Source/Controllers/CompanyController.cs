using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _service;
        private readonly IMapper _mapper;
        public CompanyController(ICompanyService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/company
        [HttpGet]
        public ActionResult<IEnumerable<CompanyDTO>> GetAll(int? accelerationId = null, int? userId = null)
        {
            if (accelerationId.HasValue && userId is null)
                return Ok(_mapper.Map<List<CompanyDTO>>(_service.FindByAccelerationId(accelerationId.Value)));
            else if (userId.HasValue && accelerationId is null)
                return Ok(_mapper.Map<List<CompanyDTO>>(_service.FindByUserId(userId.Value)));
            else
                return NoContent();
        }

        // GET: api/company/5
        [HttpGet("{id}")]
        public ActionResult<CompanyDTO> Get(int id)
        {
            return Ok(_mapper.Map<CompanyDTO>(_service.FindById(id)));
        }

        // POST: api/company
        [HttpPost]
        public ActionResult<CompanyDTO> Post([FromBody] CompanyDTO companyDTO)
        {
            var company = _service.Save(_mapper.Map<Company>(companyDTO));
            return Ok(_mapper.Map<CompanyDTO>(company));
        }
    }
}
