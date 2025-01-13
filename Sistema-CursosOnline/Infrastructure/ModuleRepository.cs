using Dapper;
using Sistema_CursosOnline.Data;
using Sistema_CursosOnline.Domain.Entities;
using Sistema_CursosOnline.Domain.IRepository;

namespace Sistema_CursosOnline.Infrastructure
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly PostgresConnection _dbConnection;

        public ModuleRepository(PostgresConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Module> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Modules WHERE Id = @Id";
            using (var connection = _dbConnection.GetConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Module>(query, new { Id = id });
            }
        }

        public async Task<IEnumerable<Module>> GetByCourseIdAsync(int courseId)
        {
            var query = "SELECT * FROM Modules WHERE CourseId = @CourseId";
            using (var connection = _dbConnection.GetConnection())
            {
                return await connection.QueryAsync<Module>(query, new { CourseId = courseId });
            }
        }

        public async Task CreateAsync(Module module)
        {
            var query = "INSERT INTO Modules (CourseId, Title, Description, LessonCount) VALUES (@CourseId, @Title, @Description, @LessonCount)";
            using (var connection = _dbConnection.GetConnection())
            {
                await connection.ExecuteAsync(query, module);
            }
        }

        public async Task UpdateAsync(Module module)
        {
            var query = "UPDATE Modules SET Title = @Title, Description = @Description, LessonCount = @LessonCount WHERE Id = @Id";
            using (var connection = _dbConnection.GetConnection())
            {
                await connection.ExecuteAsync(query, module);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var query = "DELETE FROM Modules WHERE Id = @Id";
            using (var connection = _dbConnection.GetConnection())
            {
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}
