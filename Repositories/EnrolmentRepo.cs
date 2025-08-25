using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApStudentEnrolment.Data;
using WebApStudentEnrolment.Models;

namespace WebApStudentEnrolment.Repositories
{
    public class EnrolmentRepo : IEnrolments
    {
        public EnrolmentRepo() { }
        private readonly StudentEnrolmentContext _context;
        public EnrolmentRepo(StudentEnrolmentContext context)
        {
            _context = context;
        }

        public int Count { get; private set; }

        public async Task<Enrolment> AddEnrolment(Enrolment enrolment)
        {
            // Implementation for adding an enrolment
            await _context.Enrolments.AddAsync(enrolment);
            await _context.SaveChangesAsync();
            //return new OkResult(); // Return an appropriate result, e.g., Ok or Created
            return enrolment; // Return the added enrolment object
        }

        public async Task<Enrolment> GetEnrolmentById(int enrolmentId)
        {
            // Implementation for retrieving an enrolment by ID
            var enrolment = await _context.Enrolments.Include(e => e.Course).Include(e => e.Student).FirstOrDefaultAsync(m => m.Id == enrolmentId); ;//Enrolments.FindAsync(enrolmentId);
            if (enrolment == null)
            {
                return null; // Return 404 if not found
            }
            return enrolment; // Return the enrolment object
        }

        public async Task<IEnumerable<Enrolment>> GetAllEnrolments()
        {
            // Implementation for retrieving all enrolments
            //var enrolments = await _context.Enrolments.ToListAsync();
            //return enrolments; // Return the list of enrolments
            return await _context.Enrolments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToListAsync();
        }

        public async Task<Enrolment> UpdateEnrolment(int enrolmentId,Enrolment enrolment)
        {
            // Implementation for updating an enrolment
            var existingEnrolment = await _context.Enrolments.FindAsync(enrolmentId);
            if (existingEnrolment == null)
            {
                return existingEnrolment; // Return 404 if not found
            }
            existingEnrolment.EnrolmentDate = enrolment.EnrolmentDate;
            existingEnrolment.StudentId = enrolment.StudentId;
            existingEnrolment.CourseId = enrolment.CourseId;
            // Update properties as needed
            await _context.SaveChangesAsync();
            return existingEnrolment; // Return an appropriate result, e.g., Ok or NoContent
        }

        public async Task<Enrolment> DeleteEnrolment(int enrolmentId)
        {
            // Implementation for deleting an enrolment
            var enrolment = await _context.Enrolments.FindAsync(enrolmentId);
            if (enrolment == null)
            {
                return enrolment; // Return 404 if not found
            }
            _context.Enrolments.Remove(enrolment);
            await _context.SaveChangesAsync();
            return enrolment; // Return an appropriate result, e.g., Ok or NoContent
        }
        }
}
