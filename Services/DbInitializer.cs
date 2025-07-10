using DrugAbuseReportingSystem.Models;

namespace DrugAbuseReportingSystem.Services
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Check if database already seeded
            if (context.AdminUsers.Any())
            {
                return; // DB has been seeded
            }

            // Add more seed data if needed
            context.SaveChanges();
        }
    }
}