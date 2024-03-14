using Entities;

namespace Repositories.Admin {
    public interface IAdminRepository {
        IEnumerable<User> GetAllAccount();
        Task Ban(int id);
        Task Unban(int id);
        Task CreateShopOwner(User user);
    }
}
