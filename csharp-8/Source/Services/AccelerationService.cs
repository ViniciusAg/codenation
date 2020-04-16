using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class AccelerationService : IAccelerationService
    {
        private readonly CodenationContext _context;
        public AccelerationService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Acceleration> FindByCompanyId(int companyId)
        {
            return _context.Candidates
                           .Where(x => x.Company.Id == companyId)
                           .Select(x => x.Acceleration)
                           .Distinct()
                           .ToList();
        }

        public Acceleration FindById(int id)
        {
            return _context.Accelerations.FirstOrDefault(x => x.Id == id);
        }

        public Acceleration Save(Acceleration acceleration)
        {
            if (acceleration.Id > 0 && _context.Accelerations.Any(x => x.Id == acceleration.Id))
            {
                _context.Accelerations.Update(acceleration);
            }
            else
            {
                _context.Accelerations.Add(acceleration);
            }
            _context.SaveChanges();
            return acceleration;
        }
    }
}
