using Sistema_CursosOnline.Data;
using Sistema_CursosOnline.Domain.Entities;
using Dapper;

using Sistema_CursosOnline.Domain.IRepository;

namespace Sistema_CursosOnline.Infrastructure
{
    public class EnrollmentRepository :IEnrollmentRepository
    {
        private readonly PostgresConnection _dbConnection;

        public EnrollmentRepository(PostgresConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task CreateAsync(Enrollment enrollment)
        {
            var query = "INSERT INTO Enrollments (CourseId, StudentId, EnrollmentDate) VALUES (@CourseId, @StudentId, @EnrollmentDate)";
            using (var connection = _dbConnection.GetConnection())
            {
                await connection.ExecuteAsync(query, enrollment);
            }
        }

        public async Task<IEnumerable<Enrollment>> GetByStudentIdAsync(int studentId)
        {
            var query = "SELECT * FROM Enrollments WHERE StudentId = @StudentId";

            using (var connection = _dbConnection.GetConnection())
            {
                return await connection.QueryAsync<Enrollment>(query, new { StudentId = studentId });
            }
               
        }

        public async Task<Enrollment> GetByStudentAndCourseIdAsync(int studentId, int courseId)
        {
            var query = "SELECT * FROM Enrollments WHERE StudentId = @StudentId AND CourseId = @CourseId";
            using (var connection = _dbConnection.GetConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Enrollment>(query, new { StudentId = studentId, CourseId = courseId });
            }
        }

        public async Task RemoveAsync(int enrollmentId)
        {
            var query = "DELETE FROM Enrollments WHERE Id = @Id";
            using (var connection = _dbConnection.GetConnection())
            {
                await connection.ExecuteAsync(query, new { Id = enrollmentId });
            }
              
        }
    }
}
