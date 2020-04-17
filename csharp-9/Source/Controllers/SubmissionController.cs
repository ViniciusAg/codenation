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
    public class SubmissionController : ControllerBase
    {
        private readonly ISubmissionService _service;
        private readonly IMapper _mapper;
        public SubmissionController(ISubmissionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        // GET: api/submission
        [HttpGet]
        public ActionResult<IEnumerable<SubmissionDTO>> GetAll(int? accelerationId = null, int? challengeId = null)
        {
            if (accelerationId.HasValue && challengeId.HasValue)
                return Ok(_mapper.Map<List<SubmissionDTO>>(_service.FindByChallengeIdAndAccelerationId(accelerationId.Value, challengeId.Value)));
            else
                return NoContent();
        }

        // GET: api/submission/higherScore
        [HttpGet("{higherScore}")]
        public ActionResult<decimal> GetHigherScore(int? challengeId = null)
        {
            if (challengeId.HasValue)
                return Ok(_service.FindHigherScoreByChallengeId(challengeId.Value));
            else
                return NoContent();
            
        }

        // POST: api/submission
        [HttpPost]
        public ActionResult<SubmissionDTO> Post([FromBody] SubmissionDTO submissionDTO)
        {
            var submission = _service.Save(_mapper.Map<Submission>(submissionDTO));
            return Ok(_mapper.Map<SubmissionDTO>(submission));
        }
    }
}
