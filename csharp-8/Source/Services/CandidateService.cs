using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly CodenationContext _context;
        public CandidateService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Candidate> FindByAccelerationId(int accelerationId)
        {
            return _context.Candidates
                           .Where(x => x.Acceleration.Id == accelerationId)
                           .ToList();
        }

        public IList<Candidate> FindByCompanyId(int companyId)
        {
            return _context.Candidates.Where(x => x.Company.Id == companyId).ToList();
        }

        public Candidate FindById(int userId, int accelerationId, int companyId)
        {
            return _context.Candidates
            .FirstOrDefault(x => x.AccelerationId == accelerationId && x.CompanyId == companyId && x.UserId == userId);
        }

        public Candidate Save(Candidate candidate)
        {
            var candidateFind = FindById(candidate.UserId, candidate.AccelerationId, candidate.CompanyId);
            if (candidateFind is null)
            {
                candidateFind = candidate;
                _context.Candidates.Add(candidate);
            }
            else
            {
                candidateFind.CreatedAt = candidate.CreatedAt;
                candidateFind.Status = candidate.Status;
                _context.Candidates.Update(candidateFind);
            }

            _context.SaveChanges();

            return candidateFind;
        }
    }
}
