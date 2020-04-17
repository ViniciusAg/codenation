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
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _service;
        private readonly IMapper _mapper;
        public CandidateController(ICandidateService service, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }
        // GET api/candidate
        [HttpGet]
        public ActionResult<IEnumerable<CandidateDTO>> GetAll(int? accelerationId = null, int? companyId = null)
        {
            if (accelerationId.HasValue && companyId is null)
            {
                return Ok(_mapper.Map<List<CandidateDTO>>(_service.FindByAccelerationId(accelerationId.Value)));
            }
            else if (companyId.HasValue && accelerationId is null)
            {
                return Ok(_mapper.Map<List<CandidateDTO>>(_service.FindByCompanyId(companyId.Value)));
            }
            else
            {
                return NoContent();
            }
        }

        // GET api/candidate/{userid}/{accelerationId}/{companyId}
        [HttpGet("{userId}/{accelerationId}/{companyId}")]
        public ActionResult<CandidateDTO> Get(int userId, int accelerationId, int companyId)
        {
            return Ok(_mapper.Map<CandidateDTO>(_service.FindById(userId, accelerationId, companyId)));
        }

        // POST api/user
        [HttpPost]
        public ActionResult<CandidateDTO> Post([FromBody] CandidateDTO candidateDTO)
        {
            var candidate = _service.Save(_mapper.Map<Candidate>(candidateDTO));
            return Ok(_mapper.Map<CandidateDTO>(candidate));
        }
    }
}
