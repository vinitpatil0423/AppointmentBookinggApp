
using AppointmentBookingApp.Data;
using AppointmentBookingApp.Models;
using AppointmentBookingApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBookingApp.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly GeminiService _geminiService;

        public AppointmentsController(
            ApplicationDbContext context,
            GeminiService geminiService)
        {
            _context = context;
            _geminiService = geminiService;
        }

        public async Task<IActionResult> Index()
        {
            var appointments = await _context.Appointments
                .OrderByDescending(a => a.AppointmentDate)
                .ThenBy(a => a.AppointmentTime)
                .ToListAsync();

            return View(appointments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var appointment = new Appointment
            {
                AppointmentDate = DateTime.Today
            };

            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            if (appointment.AppointmentDate.Date < DateTime.Today)
            {
                ModelState.AddModelError(
                    "AppointmentDate",
                    "Appointment date cannot be in the past."
                );
            }

            if (!ModelState.IsValid)
            {
                return View(appointment);
            }

            appointment.Status = "Scheduled";
            appointment.CreatedAt = DateTime.Now;

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Appointment booked successfully.";

            return RedirectToAction(nameof(Index));
        }

        // Generate AI summary
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerateSummary(
            string reasonForVisit)
        {
            if (string.IsNullOrWhiteSpace(reasonForVisit))
            {
                return Json(new
                {
                    success = false,
                    message = "Please enter the reason for visit first."
                });
            }

            try
            {
                var summary =
                    await _geminiService.GenerateSummaryAsync(
                        reasonForVisit);

                return Json(new
                {
                    success = true,
                    summary = summary
                });
            }
            catch
            {
                return Json(new
                {
                    success = false,
                    message = "Unable to generate AI summary. Please try again."
                });
            }
        }

        // Mark appointment as completed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Complete(int id)
        {
            var appointment =
                await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                TempData["Error"] = "Appointment not found.";
                return RedirectToAction(nameof(Index));
            }

            appointment.Status = "Completed";

            await _context.SaveChangesAsync();

            TempData["Success"] =
                "Appointment marked as completed.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            var appointment =
                await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                TempData["Error"] = "Appointment not found.";
                return RedirectToAction(nameof(Index));
            }

            appointment.Status = "Cancelled";

            await _context.SaveChangesAsync();

            TempData["Success"] =
                "Appointment cancelled.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var appointment =
                await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                TempData["Error"] = "Appointment not found.";
                return RedirectToAction(nameof(Index));
            }

            _context.Appointments.Remove(appointment);

            await _context.SaveChangesAsync();

            TempData["Success"] =
                "Appointment deleted.";

            return RedirectToAction(nameof(Index));
        }
    }
}
