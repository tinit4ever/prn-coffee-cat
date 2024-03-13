using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Auth {
    public class SignInRepository : ISignInRepository {
        private readonly CoffeeCatContext _context;

        public SignInRepository(CoffeeCatContext context) {
            _context = context;
        }

        public async Task<User?> SignIn(string email, string password) {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.CustomerEmail == email);

            if (user != null && user.CustomerPassword == password) {
                return user;
            }

            return null;
        }
    }
}
