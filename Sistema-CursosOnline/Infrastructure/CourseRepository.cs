using Dapper;
using Sistema_CursosOnline.Data;
using Sistema_CursosOnline.Domain.Entities;
using Sistema_CursosOnline.Domain.IRepository;
using System.Data;

namespace Sistema_CursosOnline.Infrastructure
{
    public class CourseRepository : ICourseRepository
    {
        private readonly PostgresConnection _dbConnection;

        public CourseRepository(PostgresConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            var query = @"
                SELECT c.Id, c.Title, c.Description, c.CoverImage, c.CreatedAt, c.InstructorId, u.Name AS InstructorName 
                FROM Courses c
                INNER JOIN Users u ON u.Id = c.InstructorId
                WHERE c.Id = @Id";


            using (var connection = _dbConnection.GetConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<Course>(query, new { Id = id });

                if (result == null) return null;

                var instructorQuery = "SELECT * FROM Users WHERE Id = @InstructorId";
                var instructor = await connection.QueryFirstOrDefaultAsync<User>(instructorQuery, new { InstructorId = result.InstructorId });

                if (instructor != null)
                {
                    result.Instructor = instructor;
                }

                return result;
            }
        }

        public async Task<IEnumerable<Course>> GetByInstructorIdAsync(int instructorId)
        {
            var query = "SELECT * FROM Courses WHERE InstructorId = @InstructorId";
            using (var connection = _dbConnection.GetConnection())
            {
                return await connection.QueryAsync<Course>(query, new { InstructorId = instructorId });
            }
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            var query = @"
                SELECT c.Id, c.Title, c.Description, c.CoverImage, c.CreatedAt, c.InstructorId, u.Name AS InstructorName
                FROM Courses c
                INNER JOIN Users u ON u.Id = c.InstructorId";

            using (var connection = _dbConnection.GetConnection())
            {
                var courses = await connection.QueryAsync<CourseWithInstructor>(query);

                return courses.Select(course => new Course
                {
                    Id = course.Id,
                    Title = course.Title,
                    Description = course.Description,
                    CoverImage = course.CoverImage,
                    CreatedAt = course.CreatedAt,
                    Instructor = new User
                    {
                        Id = course.InstructorId,
                        Name = course.InstructorName
                    }
                });
            }
        }

        public async Task AddAsync(Course course)
        {
            var query = @"
                INSERT INTO Courses (Title, Description, CoverImage, InstructorId, CreatedAt)
                VALUES (@Title, @Description, @CoverImage, @InstructorId, @CreatedAt)";

            using (var connection = _dbConnection.GetConnection())
            {
                await connection.ExecuteAsync(query, course);
            }
        }

        public async Task UpdateAsync(Course course)
        {
            var query = @"
                INSERT INTO Courses (Title, Description, CoverImage, InstructorId, CreatedAt)
                VALUES (@Title, @Description, @CoverImage, @InstructorId, @CreatedAt)";

            using (var connection = _dbConnection.GetConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    course.Title,
                    course.Description,
                    course.CoverImage,
                    course.InstructorId,   
                    course.CreatedAt
                });
            }
        }

        public async Task DeleteAsync(int id)
        {
            var query = "DELETE FROM Courses WHERE Id = @Id";
            using (var connection = _dbConnection.GetConnection())
            {
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }

    }
}
