using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Auth {
    public class SessionRepository : ISessionRepository {
        private readonly CoffeeCatContext _context;

        public SessionRepository(CoffeeCatContext context) {
            _context = context;
        }
        public User GetUserById(int id) {
            try {
                User? user = _context.Users
                    .Include(u => u.Role)
                    .Include(u => u.Shop)
                    .FirstOrDefault(u => u.CustomerId == id);
                if (user == null) {
                    throw new InvalidOperationException();
                } else {
                    return user;
                }

            } catch (InvalidOperationException ex) {
                Console.WriteLine(ex.StackTrace);
                throw;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public User GetUserByRole( int? userId)
        {
            return _context.Users.FirstOrDefault(u => u.CustomerId == userId);
        }
    }
}
