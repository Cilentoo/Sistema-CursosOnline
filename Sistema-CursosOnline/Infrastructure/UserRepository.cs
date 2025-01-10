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

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var query = "SELECT * FROM Users";
            using (var connection = _dbConnection.GetConnection()) 
            {
                return await connection.QueryAsync<User>(query);
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Users Where Id = @Id";
            using (var connection = _dbConnection.GetConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<User>(query, new {Id = id}) ?? throw new InvalidOperationException("Usuário não encontrado");
            }
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var query = "SELECT * FROM Users WHERE Email = @Email";
            using (var connection = _dbConnection.GetConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<User>(query, new { Email = email }) ?? throw new InvalidOperationException("Usuário não encontrado"); ;
            }
        }

        public async Task AddAsync(User user)
        {
            var query = "INSERT INTO Users (Name, Email, PasswordHash, Role) VALUES (@Name, @Email, @PasswordHash, @Role)";
            using (var connection = _dbConnection.GetConnection())
            {
                await connection.ExecuteAsync(query, user);
            }
        }

        public async Task UpdateAsync (User user)
        {
            var query = "UPDATE Users SET Name = @Name, Email = @Email, PasswordHash = @PasswordHash, Role = @Role WHERE Id = @Id";
            using (var connection = _dbConnection.GetConnection())
            {
                await connection.ExecuteAsync(query, user);
            }
        }

        public async Task InactiveAsync(int id)
        {
            var query = "UPDATE Users SET Status = 'Inativo' WHERE id = @Id";
            using (var connection = _dbConnection.GetConnection())
            {
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }

        public async Task<User> AuthenticateAsync(string email, string password)
        {
            var user = await GetByEmailAsync(email);
            if(user == null)
            {
                return null;
            }

            if(!VerifyPasswordHash (password, user.PasswordHash))
            {
                return null;
            }
            return user;
        }

        public static string CreatePasswordHash(string password)
        {
            using (var hmac = new HMACSHA512()){
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var hash = hmac.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hash);
            }
        }

        public static bool VerifyPasswordHash(string password, string storedHash)
        {
            var hashBytes = Convert.FromBase64String(storedHash);
            using(var hmac = new HMACSHA512())
            {
                var passwordBytes = Encoding.UTF8.GetBytes (password);
                var computedHash = hmac.ComputeHash(passwordBytes);

                return computedHash.SequenceEqual(hashBytes);
            }
        }
    }
}
