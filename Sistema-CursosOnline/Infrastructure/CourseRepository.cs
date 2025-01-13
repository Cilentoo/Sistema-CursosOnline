﻿using System.Data;
using Dapper;
using Sistema_CursosOnline.Data;
using Sistema_CursosOnline.Domain.Entities;
using Sistema_CursosOnline.Domain.IRepository;

namespace Sistema_CursosOnline.Infrastructure
{
    public class CourseRepository : ICourseRepository
    {
        private readonly PostgresConnection _dbConnection;

        public CourseRepository(PostgresConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            var query = "SELECT * FROM Courses";
            using(var connection = _dbConnection.GetConnection()) 
            {
                return await connection.QueryAsync<Course>(query);
            }
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Courses WHERE Id = @Id";
            using (var connection = _dbConnection.GetConnection()) 
            {
                return await connection.QuerySingleOrDefaultAsync<Course>(query, new { Id = id }) ?? throw new InvalidOperationException("Curso não encontrado");
            }
        }

        public async Task<IEnumerable<Course>> GetByStatusAsync(string status)
        {
            var query = "SELECT * FROM Courses WHERE Status = @Status";
            using (var connection = _dbConnection.GetConnection())
            {
                return await connection.QueryAsync<Course>(query, new { Status = status });
            }
            
        }

        public async Task AddAsync(Course course)
        {
            var query = "INSERT INTO Courses (Title, Description, Instructor_Id, Status, Creation_Date, PhotoData) " +
                        "VALUES (@Title, @Description, @InstructorId, @Status, @CreationDate, @PhotoData)";
            using (var connection = _dbConnection.GetConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }

        public async Task UpdateAsync(Course course)
        {
            var query = "UPDATE Courses SET Title = @Title, Description = @Description, InstructorId = @InstructorId, Status = @Status, PhotoData = @PhotoData WHERE Id = @Id";
            using (var connection = _dbConnection.GetConnection())
            {
                await connection.ExecuteAsync(query, course);
            }
        }

        public async Task InactiveCourseAsync(int id)
        {
            var query = "UPDATE Courses SET Status = 'Inativo' WHERE Id = @Id";
            using (var connection = _dbConnection.GetConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
                if(rowsAffected == 0)
                {
                    throw new InvalidOperationException("Curso não encontrado ou inativo");
                }
            }
        }

    }
}
