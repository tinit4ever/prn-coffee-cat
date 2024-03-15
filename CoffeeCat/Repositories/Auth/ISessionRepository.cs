using Entities;

namespace Repositories.Auth {
    public interface ISessionRepository {
        User GetUserById(int id);
    }
}
