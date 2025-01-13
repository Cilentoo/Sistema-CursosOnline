using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Dapper;
using Sistema_CursosOnline.Data;
using Sistema_CursosOnline.Domain.Entities;
using Sistema_CursosOnline.Domain.IRepository;

namespace Sistema_CursosOnline.Infrastructure
{
    public class UserRepository : IUserRepository
    {

        private readonly PostgresConnection _dbConnection;

        public UserRepository(PostgresConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<User>> GetUserAsync()
        {
            var query = "SELECT * FROM users";
            using (var connection = _dbConnection.GetConnection()) 
            {
                return await connection.QueryAsync<User>(query);
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM users Where Id = @Id";
            using (var connection = _dbConnection.GetConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<User>(query, new {Id = id}) ?? throw new InvalidOperationException("Usuário não encontrado");
            }
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var query = "SELECT Name, Email, Password_Hash AS PasswordHash, Role, Status FROM users WHERE Email = @Email";
            using (var connection = _dbConnection.GetConnection())
            {
                var user = await connection.QueryFirstOrDefaultAsync<User>(query, new { Email = email });
                return user;
            }
        }

        public async Task AddAsync(User user, string password)
        {
            user.SetPassword(password);
            var query = "INSERT INTO users (Name, Email, Password_Hash, Role, Status) VALUES (@Name, @Email, @PasswordHash, @Role, 'Ativo')";
            using (var connection = _dbConnection.GetConnection())
            {
                await connection.ExecuteAsync(query, user);
            }
        }

        public async Task UpdateAsync (User user)
        {
            var query = "UPDATE users SET Name = @Name, Email = @Email, PasswordHash = @PasswordHash, PasswordKey = @PasswordKey, Role = @Role, Status = @Status WHERE Id = @Id";
            using (var connection = _dbConnection.GetConnection())
            {
                await connection.ExecuteAsync(query, user);
            }
        }

        public async Task InactiveAsync(int id)
        {
            var query = "UPDATE users SET Status = 'Inativo' WHERE id = @Id";
            using (var connection = _dbConnection.GetConnection())
            {
                await connection.ExecuteAsync(query, new { Id = id, });
                 }
        }

        public async Task<User> AuthenticateAsync(string email, string password)
        {

            var user = await GetByEmailAsync(email);
            if (user == null || !user.ValidatePassword(password))
            {
                return null;
            }

            if (!user.ValidatePassword(password))
            {
                return null; 
            }
            return user;
        }

        public async Task<string> GetNameByIdAsync(int id)
        {
            var query = "SELECT Name FROM Users WHERE Id = @Id AND Role = @Role";
            using (var connection = _dbConnection.GetConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<string>(query, new { Id = id, Role = EType.Instructor });
            }
        }
    }
}
