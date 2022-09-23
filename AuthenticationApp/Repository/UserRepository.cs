using AuthenticationApp.Context;
using AuthenticationApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationApp.Repository
{
    public class UserRepository : IUserRepository
    {   
        private readonly ApplicationContext db;

        public UserRepository(ApplicationContext _appContext)
        {
            db= _appContext;    
        }

        public void Create(User item)
        {
            db.Users.Add(item);
        }

        public void Delete(User item)
        {
            db.Users.Remove(item);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await db.Users.ToListAsync();
        }

        public async  Task<User?> GetElementAsync(int id)
        {
            return await db.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await db.Users.FirstOrDefaultAsync(u => u.UserEmail == email);
        }

        public async Task SaveChangesAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Update(User item)
        {
            db.Users.Update(item);
        }
    }
}
