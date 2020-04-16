using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class UserService : IUserService
    {
        private readonly CodenationContext _context;
        public UserService(CodenationContext context)
        {
            _context = context;
        }

        public IList<User> FindByAccelerationName(string name)
        {
           return _context.Candidates
                          .Where(x => x.Acceleration.Name == name)
                          .Select(x => x.User)
                          .ToList();
        }

        public IList<User> FindByCompanyId(int companyId)
        {
            return _context.Candidates
                           .Where(x => x.Company.Id == companyId)
                           .Select(x => x.User)
                           .Distinct()
                           .ToList();
        }

        public User FindById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public User Save(User user)
        {
            if (user.Id > 0 && _context.Users.Any(x => x.Id == user.Id))
            {
                _context.Users.Update(user);
            }
            else
            {
                _context.Users.Add(user);
            }
            
            _context.SaveChanges();
            return user;
        }
    }
}
