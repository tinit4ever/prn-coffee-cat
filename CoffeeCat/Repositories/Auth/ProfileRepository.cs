using Entities;

namespace Repositories.Auth {
    public class ProfileRepository : IProfileRepository {
        private readonly CoffeeCatContext _context;

        public ProfileRepository(CoffeeCatContext context) {
            _context = context;
        }

        public void UpdateUser(User user) {
            try {
                _context.Update(user);
                _context.SaveChangesAsync();
            } catch (Exception ex) {
                Console.WriteLine($"An error occurred while creating the user: {ex.Message}");
                throw;
            }
        }
    }
}
