using Sistema_CursosOnline.Domain.Entities;

namespace Sistema_CursosOnline.Domain.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUserAsync();

        Task<User> GetByIdAsync(int id);

        Task<User> GetByEmailAsync(string email);

        Task AddAsync(User user, string password);

        Task UpdateAsync(User user);

        Task InactiveAsync(int id);

        Task<User> AuthenticateAsync(string email, string password);
    }
}
