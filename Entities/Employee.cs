using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HRISSystemBackend.Entities
{
    [Table("employee")]
    public class Employee
    {
        [Key]
        [Required(ErrorMessage="Employee code is required")]
        public string emp_code { get; set; }

        [Required(ErrorMessage = "Employee Name is required")]
        public string name { get; set; }

        public string email { get; set; }

        public string address { get; set; } = null;

        public string phone { get; set; } = null;

        public string employment_type { get; set; } = null;

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime join_date { get; set; }

        public int job_position { get; set; }

        public string manager { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime last_updated { get; set; } = DateTime.UtcNow;

        public string position_name { get; } = string.Empty;
        public string manager_name { get; } = string.Empty;

    }
}