using Entities;

namespace Repositories.Auth {
    public interface ISessionRepository {
        Task<User?> getUserByIdAsync(int id);
    }
}
