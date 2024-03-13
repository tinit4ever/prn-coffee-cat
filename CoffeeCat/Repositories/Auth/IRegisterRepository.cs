using Entities;

namespace Repositories.Auth {
    public interface IRegisterRepository {
        Task RegisterAsync(User user);

        bool IsExistedEmail(string email);
    }
}
