using Entities;

namespace Repositories.Auth {
    public interface ISignInRepository {
        Task<User?> SignIn(string email, string password);
    }
}
