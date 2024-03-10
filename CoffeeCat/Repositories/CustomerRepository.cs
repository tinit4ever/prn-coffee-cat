using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CoffeeCatContext context; // Đối tượng DbContext để tương tác với cơ sở dữ liệu

        public CustomerRepository(CoffeeCatContext context)
        {
            this.context = context;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await context.Users.Include(u => u.Role).Include(u => u.Shop).FirstOrDefaultAsync(u => u.CustomerEmail == email);
        }
    }
}
