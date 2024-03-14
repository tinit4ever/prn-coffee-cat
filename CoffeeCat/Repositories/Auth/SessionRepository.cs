using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Auth {
    public class SessionRepository : ISessionRepository {
        private readonly CoffeeCatContext _context;

        public SessionRepository(CoffeeCatContext context) {
            _context = context;
        }
        public async Task<User?> getUserByIdAsync(int id) {
            try {
                User? user = await _context.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.CustomerId == id);
                return user;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
