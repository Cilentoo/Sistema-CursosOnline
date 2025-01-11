using Dapper;
using Sistema_CursosOnline.Data;
using Sistema_CursosOnline.Domain.Entities;
using Sistema_CursosOnline.Domain.IRepository;

namespace Sistema_CursosOnline.Infrastructure
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly PostgresConnection _dbConnection;

        public EnrollmentRepository(PostgresConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task AddEnrollmentAsync(Enrollment enrollment)
        {
            var query = "INSERT INTO Enrollments (StudentId, CourseId) VALUES (@StudentId, @CourseId)";
            using (var connection = _dbConnection.GetConnection())
            {
                await connection.ExecuteAsync(query, new { enrollment.StudentId, enrollment.CourseId });
            }
        }

        public async Task<Enrollment> GetEnrollmentAsync(int studentId, int courseId)
        {
            var query = "SELECT * FROM Enrollments WHERE StudentId = @StudentId AND CourseId = @CourseId";
            using (var connection = _dbConnection.GetConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Enrollment>(query, new { studentId, courseId });
            }
        }

        public async Task RemoveEnrollmentAsync(int enrollmentId)
        {
            var query = "DELETE FROM Enrollments WHERE Id = @Id";
            using (var connection = _dbConnection.GetConnection())
            {
                await connection.ExecuteAsync(query, new { Id = enrollmentId });
            }
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentIdAsync(int studentId)
        {
            var query = "SELECT * FROM Enrollments WHERE StudentId = @StudentId";
            using (var connection = _dbConnection.GetConnection())
            {
                return await connection.QueryAsync<Enrollment>(query, new { StudentId = studentId });
            }
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByCourseIdAsync(int courseId)
        {
            var query = "SELECT * FROM Enrollments WHERE CourseId = @CourseId";
            using (var connection = _dbConnection.GetConnection())
            {
                return await connection.QueryAsync<Enrollment>(query, new { CourseId = courseId });
            }
        }
    }
}
