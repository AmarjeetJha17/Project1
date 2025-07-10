using System.ComponentModel.DataAnnotations;

namespace DrugAbuseReportingSystem.Models
{
    public class AdminUser
    {
        [Key]
        public int AdminId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string FullName { get; set; }

        public DateTime LastLogin { get; set; }
    }
}