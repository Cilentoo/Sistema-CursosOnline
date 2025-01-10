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

        public async Task<IEnumerable<Module>> GetByCourseIdAsync(int courseId)
        {
            var query = "SELECT * FROM Module WHERE courseId = @CourseId";
            using (var connection = _dbConnection.GetConnection())
            {
                return await connection.QueryAsync<Module>(query, new { CourseId = courseId });
            }
        }

        public async Task<Module> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Module WHERE Id = @Id";
            using (var connection = _dbConnection.GetConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Module>(query, new { Id = id }) ?? throw new InvalidOperationException("Curso não encontrado");
            }
        }

        public async Task AddAsync (Module module)
        {
            var query = "INSERT INTO Module(Name, Description, CourseId) VALUES (@Name, @Description, @CourseId)";
            using (var connection = _dbConnection.GetConnection())
            {
                await connection.ExecuteAsync(query, module);
            }
        }

        public async Task UpdateAsync(Module module)
        {
            if(module.Id > 0)
            {
                var query = "UPDATE Module SET Name = @Name, Description = @Description, CourseId = @CourseId";
                using (var connection = _dbConnection.GetConnection())
                {
                    await connection.ExecuteAsync(query, module);
                }
            }
            else
            {
                throw new InvalidOperationException("Módulo nao encontrado");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var query = "DELETE FROM Module WHERE Id = @Id";
            using (var connection = _dbConnection.GetConnection())
            {
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}
