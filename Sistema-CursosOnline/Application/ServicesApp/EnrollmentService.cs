﻿using Sistema_CursosOnline.Application.DTO;
using Sistema_CursosOnline.Domain.Entities;
using Sistema_CursosOnline.Domain.IRepository;
using Sistema_CursosOnline.Domain.IServices;

namespace Sistema_CursosOnline.Application.ServicesApp
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task EnrollStudentAsync(EnrollmentDTO enrollmentDto)
        {
            var existingEnrollment = await _enrollmentRepository.GetByStudentAndCourseIdAsync(enrollmentDto.StudentId, enrollmentDto.CourseId);
            if (existingEnrollment != null)
            {
                throw new Exception("O aluno já está inscrito neste curso.");
            }

            var enrollment = new Enrollment
            {
                StudentId = enrollmentDto.StudentId,
                CourseId = enrollmentDto.CourseId,
                EnrollmentDate = DateTime.UtcNow
            };

            await _enrollmentRepository.CreateAsync(enrollment);
        }

        public async Task UnenrollStudentAsync(int studentId, int courseId)
        {
            var existingEnrollment = await _enrollmentRepository.GetByStudentAndCourseIdAsync(studentId, courseId);
            if (existingEnrollment == null)
            {
                throw new Exception("O aluno não está inscrito neste curso.");
            }

            await _enrollmentRepository.RemoveAsync(existingEnrollment.Id);
        }

        public async Task<bool> IsEnrolledAsync(int studentId, int courseId)
        {
            var existingEnrollment = await _enrollmentRepository.GetByStudentAndCourseIdAsync(studentId, courseId);
            return existingEnrollment != null;
        }
    }
}
