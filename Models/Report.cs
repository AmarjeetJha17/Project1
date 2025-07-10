using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugAbuseReportingSystem.Models
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }

        [Required]
        public DateTime ReportDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(100)]
        public string Location { get; set; }

        [Required]
        public DrugType DrugType { get; set; }

        [Required]
        public SeverityLevel Severity { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(100)]
        public string? AnonymousContact { get; set; }

        [StringLength(255)]
        public string? ImagePath { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public ReportStatus Status { get; set; } = ReportStatus.Pending;

        [StringLength(500)]
        public string? AdminNotes { get; set; }
    }

    public enum DrugType
    {
        Cannabis,
        Cocaine,
        Heroin,
        Methamphetamine,
        MDMA,
        PrescriptionDrugs,
        SyntheticDrugs,
        Other
    }

    public enum SeverityLevel
    {
        Low,
        Medium,
        High,
        Critical
    }

    public enum ReportStatus
    {
        Pending,
        UnderReview,
        Resolved,
        Dismissed
    }
}