using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Codenation.Challenge.Models;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;

namespace Codenation.Challenge.Services
{
    public class SubmissionService : ISubmissionService
    {
        private readonly CodenationContext _context;
        public SubmissionService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Submission> FindByChallengeIdAndAccelerationId(int challengeId, int accelerationId)
        {
            return (from s in _context.Submissions
                    join u in _context.Users on s.UserId equals u.Id
                    join c in _context.Candidates on u.Id equals c.UserId
                    where s.ChallengeId == challengeId && c.AccelerationId == accelerationId
                    select s).Distinct().ToList();
        }

        public decimal FindHigherScoreByChallengeId(int challengeId)
        {
            return _context.Submissions
                           .Where(x => x.ChallengeId == challengeId)
                           .Max(x => x.Score);
        }

        public Submission Save(Submission submission)
        {
            var submissionFind = _context.Submissions.FirstOrDefault(s => s.UserId == submission.UserId && s.ChallengeId == submission.ChallengeId);
            if (submissionFind is null)
            {
                submissionFind = submission;
                _context.Submissions.Add(submission);
            }
            else
            {
                submissionFind.CreatedAt = submission.CreatedAt;
                submissionFind.Score = submission.Score;
                _context.Submissions.Update(submissionFind);
            }

            _context.SaveChanges();

            return submissionFind;
        }
    }
}
