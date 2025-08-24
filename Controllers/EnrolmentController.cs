using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApStudentEnrolment.Data;
using WebApStudentEnrolment.Models;
using WebApStudentEnrolment.Repositories;

namespace WebApStudentEnrolment.Controllers
{
    public class EnrolmentController : Controller
    {
        private readonly IEnrolments _enrolmentRepo;
        private readonly StudentEnrolmentContext _context;
        public EnrolmentController(IEnrolments enrolmentRepo, StudentEnrolmentContext context)
        {
            _enrolmentRepo = enrolmentRepo;
            _context = context;
        }
        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var enrolment = await _enrolmentRepo.GetAllEnrolments();

            return View(enrolment);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var enrolment = await _enrolmentRepo.GetEnrolmentById(id);
            if (enrolment == null)
            {
                return NotFound();
            }
            return View(enrolment);
        }

        // GET: Courses/Create

        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name");
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            return View();
        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,CourseId,EnrolmentDate")] Enrolment enrolment)
        {
            if (ModelState.IsValid)
            {
                await _enrolmentRepo.AddEnrolment(enrolment);
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", enrolment.Id);
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", enrolment.Id);
            return View(enrolment);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var enrolment = await _enrolmentRepo.GetEnrolmentById(id);
            if (enrolment == null)
            {
                return NotFound();
            }
            return View(enrolment);
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,CourseId,EnrolmentDate")] Enrolment enrolment)
        {
            if (id != enrolment.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _enrolmentRepo.UpdateEnrolment(id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _enrolmentRepo.GetEnrolmentById(id) == null)
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", enrolment.StudentId);
            ViewData["CourseId"] = new SelectList(_context.Students, "Id", "Name", enrolment.CourseId);
            return View(enrolment);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _enrolmentRepo.GetEnrolmentById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _enrolmentRepo.GetEnrolmentById(id);
            if (course != null)
            {
                await _enrolmentRepo.DeleteEnrolment(id);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
