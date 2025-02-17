﻿using Sistema_CursosOnline.Application.DTO;
using Sistema_CursosOnline.Domain.Entities;

namespace Sistema_CursosOnline.Domain.IServices
{
    public interface ICourseService
    {
        Task<CourseDTO> GetCourseByIdAsync(int id);
        Task<IEnumerable<CourseDTO>> GetAllCoursesAsync();
        Task AddCourseAsync(CourseDTO courseDto);
        Task UpdateCourseAsync(CourseDTO courseDto);
        Task DeleteCourseAsync(int id);

        Task<IEnumerable<CourseDTO>> GetCoursesByInstructorIdAsync(int instructorId);
    }
}
