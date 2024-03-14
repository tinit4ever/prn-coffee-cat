using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Admin {
    public class AdminRepository : IAdminRepository {
        private readonly CoffeeCatContext _context;

        public AdminRepository(CoffeeCatContext context) {
            _context = context;
        }

        public IEnumerable<User> GetAllAccount() {
            try {
                return _context.Users
                .Where(u => u.RoleId != 1)
                .Include(u => u.Role)
                .Include(u => u.Shop);
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task Ban(int id) {
            try {
                var user = await _context.Users.FindAsync(id);

                if (user != null) {
                    user.CustomerEnabled = false;

                    await _context.SaveChangesAsync();
                } else {
                    throw new ArgumentException($"User with ID {id} not found.");
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task Unban(int id) {
            try {
                var user = await _context.Users.FindAsync(id);

                if (user != null) {
                    user.CustomerEnabled = true;

                    await _context.SaveChangesAsync();
                } else {
                    throw new ArgumentException($"User with ID {id} not found.");
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task CreateShopOwner(User user) {
            try {
                await _context.AddAsync(user);
                await _context.SaveChangesAsync();
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
