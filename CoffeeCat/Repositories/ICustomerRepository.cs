using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ICustomerRepository<T> where T : class
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<IQueryable<Shop>> GetShopEnableAsync();
    }
}
