using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class CompanyService : ICompanyService
    {
        private CodenationContext _context;
        public CompanyService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Company> FindByAccelerationId(int accelerationId)
        {
            return _context.Candidates
                           .Where(x => x.Acceleration.Id == accelerationId)
                           .Select(x => x.Company)
                           .ToList();
        }

        public Company FindById(int id)
        {
            return _context.Companies.FirstOrDefault(x => x.Id == id);
        }

        public IList<Company> FindByUserId(int userId)
        {
            return _context.Candidates
                           .Where(x => x.User.Id == userId)
                           .Select(x => x.Company)
                           .Distinct()
                           .ToList();
        }

        public Company Save(Company company)
        {
            if ((company.Id > 0) && (_context.Companies.Any(x => x.Id == company.Id)))
            {
                _context.Companies.Update(company);
            }
            else
            {
                _context.Companies.Add(company);
            }
            _context.SaveChanges();
            return company;
        }
    }
}