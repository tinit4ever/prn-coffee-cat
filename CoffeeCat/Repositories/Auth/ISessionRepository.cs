using Entities;

namespace Repositories.Auth {
    public interface ISessionRepository {
        User GetUserById(int id);
        User GetUserByRole(int? userId);
    }
}
