using AuthenticationApp.Models;

namespace AuthenticationApp.Repository
{
    public interface IUserRepository:IRepository<User>
    {
        Task<User?> GetUserByEmail(string email);
        Task<IEnumerable<User>> GetUsersAsync(string[] keys);
    }
}
