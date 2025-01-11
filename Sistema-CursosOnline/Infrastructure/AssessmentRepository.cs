using Dapper;
using Sistema_CursosOnline.Data;
using Sistema_CursosOnline.Domain.Entities;
using Sistema_CursosOnline.Domain.IRepository;

namespace Sistema_CursosOnline.Infrastructure
{
    public class AssessmentRepository : IAssessmentRepository
    {
        private readonly PostgresConnection _dbConnection;

        public AssessmentRepository(PostgresConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Assessment>> GetByCourseIdAsync(int courseId)
        {
            var query = "SELECT * FROM Assessments WHERE courseId = @CourseId";
            using (var connection = _dbConnection.GetConnection())
            {
                return await connection.QueryAsync<Assessment>(query, new { CourseId = courseId });
            }
        }

        public async Task<IEnumerable<Assessment>> GetByUserIdAsync(int userId)
        {
            var query = "SELECT * FROM Assessments WHERE UserId = @UserId";
            using (var connection = _dbConnection.GetConnection())
            {
                return await connection.QueryAsync<Assessment>(query, new { UserId = userId });
            }
        }

        public async Task AddAsync(Assessment assessment)
        {
            var query = "INSERT INTO Assessments (courseId, userId, note, comment) " +
                           "VALUES (@CourseId, @UserId, @Note, @Comment)";
            using (var connection = _dbConnection.GetConnection())
            {
                await connection.ExecuteAsync(query, assessment);
            }
        }

        public async Task UpdateAsync (Assessment assessment)
        {
            var query ="UPDATE Assessments SET notes = @Notes, comment = @Comment " +
                            "WHERE id = @Id";
            using (var connection = _dbConnection.GetConnection())
            {
                await connection.ExecuteAsync(query, assessment);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var query = "DELETE FROM Assessments WHERE Id = @Id";
            using (var connection = _dbConnection.GetConnection())
            {
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}
