using Entities;

namespace Repositories.Auth {
    public class RegisterRepository : IRegisterRepository {
        private readonly CoffeeCatContext _context;

        public RegisterRepository(CoffeeCatContext context) {
            _context = context;
        }

        public async Task RegisterAsync(User user) {
            try {
                await _context.AddAsync(user);
                await _context.SaveChangesAsync();
            } catch (Exception ex) {
                Console.WriteLine($"An error occurred while creating the user: {ex.Message}");
                throw;
            }
        }
    }
}
