using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class ChallengeService : IChallengeService
    {
        private readonly CodenationContext _context;
        public ChallengeService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Models.Challenge> FindByAccelerationIdAndUserId(int accelerationId, int userId)
        {
           return _context.Candidates.Include(x => x.Acceleration).ThenInclude(x => x.Challenge)
                          .Where(x => x.User.Id == userId && x.Acceleration.Id == accelerationId)
                          .Select(x => x.Acceleration)
                          .Select(x => x.Challenge)
                          .Distinct()
                          .ToList();
        }

        public Models.Challenge Save(Models.Challenge challenge)
        {
            if (challenge.Id > 0 && _context.Challenges.Any(x => x.Id == challenge.Id))
            {
                _context.Challenges.Update(challenge);
            }
            else
            {
                _context.Challenges.Add(challenge);
            }
            
            _context.SaveChanges();
            return challenge;
        }
    }
}