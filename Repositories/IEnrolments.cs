using Microsoft.AspNetCore.Mvc;
using WebApStudentEnrolment.Models;

namespace WebApStudentEnrolment.Repositories
{
        public interface IEnrolments
        {
            int Count { get; }
            Task<Enrolment> AddEnrolment(Enrolment enrolment);
            Task<Enrolment?> GetEnrolmentById(int enrolmentId);
            Task<IEnumerable<Enrolment>> GetAllEnrolments();
            Task<Enrolment?> UpdateEnrolment(int enrolmentId);
            Task<Enrolment> DeleteEnrolment(int enrolmentId);
        }
}
