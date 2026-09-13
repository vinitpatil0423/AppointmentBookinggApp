using System.ComponentModel.DataAnnotations;

namespace AppointmentBookingApp.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Patient name is required")]
        [Display(Name = "Patient Name")]
        public string PatientName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mobile number is required")]
        [RegularExpression(@"^[6-9]\d{9}$",
            ErrorMessage = "Enter a valid 10-digit mobile number")]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Doctor name is required")]
        [Display(Name = "Doctor Name")]
        public string DoctorName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Reason for visit is required")]
        [Display(Name = "Reason for Visit")]
        public string ReasonForVisit { get; set; } = string.Empty;

        [Display(Name = "AI Summary")]
        public string? AiSummary { get; set; }

        [Required(ErrorMessage = "Appointment date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "Appointment time is required")]
        [Display(Name = "Appointment Time")]
        public TimeSpan AppointmentTime { get; set; }

        public string Status { get; set; } = "Scheduled";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}