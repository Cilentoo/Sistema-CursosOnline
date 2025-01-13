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
            var query = "SELECT * FROM Assessments WHERE CourseId = @CourseId";
            using (var connection = _dbConnection.GetConnection())
            {
                return await connection.QueryAsync<Assessment>(query, new { CourseId = courseId });
            }
        }

        public async Task CreateAsync(Assessment assessment)
        {
            var query = "INSERT INTO Assessments (CourseId, StudentId, Rating, Comment) VALUES (@CourseId, @StudentId, @Rating, @Comment)";
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
