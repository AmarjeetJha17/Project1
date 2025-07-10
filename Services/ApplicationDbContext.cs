using DrugAbuseReportingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DrugAbuseReportingSystem.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Report> Reports { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<DrugInformation> DrugInformation { get; set; }
        public DbSet<RehabilitationCenter> RehabilitationCenters { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed initial admin user
            modelBuilder.Entity<AdminUser>().HasData(
                new AdminUser
                {
                    AdminId = 1,
                    Username = "admin",
                    PasswordHash = "admin123",
                    Email = "admin@drugabusereporting.com",
                    FullName = "System Administrator",
                    LastLogin = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            // Seed drug information
            modelBuilder.Entity<DrugInformation>().HasData(
                new DrugInformation
                {
                    Id = 1,
                    Name = "Cannabis",
                    Description = "Cannabis, also known as marijuana among other names, is a psychoactive drug from the Cannabis plant.",
                    Effects = "Euphoria, relaxation, altered perception of time and space",
                    Risks = "Impaired memory, lung damage (from smoking), potential for addiction",
                    ImagePath = "/images/drugs/cannabis.jpg"
                },
                new DrugInformation
                {
                    Id = 2,
                    Name = "Cocaine",
                    Description = "Cocaine is a powerful stimulant drug made from the leaves of the coca plant native to South America.",
                    Effects = "Increased energy, euphoria, talkativeness, mental alertness",
                    Risks = "Heart attack, stroke, seizures, respiratory failure, addiction",
                    ImagePath = "/images/drugs/cocaine.jpg"
                },
                new DrugInformation
                {
                    Id = 4,
                    Name = "Heroin",
                    Description = "Heroin is an opioid drug made from morphine, a natural substance taken from the seed pod of the various opium poppy plants grown in Southeast and Southwest Asia, Mexico, and Colombia.",
                    Effects = "People who use heroin report feeling a rush (a surge of pleasure). And then they may feel other effects, such as a warm flushing of the skin, dry mouth, and a heavy feeling in the arms and legs. They may also have severe itching, nausea, and vomiting.",
                    Risks = "People who use heroin over the long term may develop many different health problems. These problems could include liver, kidney, and lung disease, mental disorders, and abscesses.",
                    ImagePath = "/images/drugs/heroine.jpg"
                },
                new DrugInformation
                {
                    Id = 5,
                    Name = "Methamphetamine",
                    Description = "Methamphetamines are stimulants, a type of drug that lets people stay awake and do continuous activity with less need for sleep",
                    Effects = "typically include feelings of euphoria and increased alertness and energy. It can also cause serious negative health effects, including paranoia, anxiety, rapid heart rate, irregular heartbeat, stroke, or even death",
                    Risks = "use may lead to insomnia, memory loss, development of a substance use disorder, and other health problems.",
                    ImagePath = "/images/drugs/metham.jpg"
                },
                new DrugInformation
                {
                    Id = 6,
                    Name = "MDMA",
                    Description = "MDMA, commonly known as ecstasy or molly, is an empathogen-entactogenic drug with stimulant and minor psychedelic properties. ",
                    Effects = "MDMA’s effects may include feeling more energetic and alert and having an increased sense of well-being, warmth, and openness toward others.",
                    Risks = "insomnia, confusion, irritability, anxiety, and depression, impulsiveness and aggression, decreased interest in sex, memory and attention problems, reduced appetite",
                    ImagePath = "/images/drugs/MDMA.jpg"
                },
                new DrugInformation
                {
                    Id = 7,
                    Name = "PrescriptionDrugs",
                    Description = "Prescription drugs are medications that require a written order from a licensed healthcare professional (like a doctor) to be dispensed by a pharmacist",
                    Effects = "Anxiety, Confusion, Dizziness, Drowsiness, Euphoria, Excessive sleep or inability to sleep, Hyperactivity, Mood swings, Nausea, Unusual muscle movements",
                    Risks = "Anxiety, Changes in appearance (weight loss or weight gain), Changes in physical health, Damage to the heart, kidneys, liver and brain, Depression, Drug addiction, Drug dependence, Mental health decline",
                    ImagePath = "/images/drugs/prescriptionDrugs.jpg"
                },
                new DrugInformation
                {
                    Id = 8,
                    Name = "SyntheticDrugs",
                    Description = "Synthetic drugs are substances that are created or modified in a laboratory setting, as opposed to being naturally occurring. They can have both therapeutic and psychoactive effects",
                    Effects = "aim to mimic the effects of existing illicit drugs (such as cannabis, cocaine, MDMA and LSD).Euphoria\r\nfeelings of wellbeing\r\nspontaneous laughter and excitement\r\nincreased appetite\r\ndry mouth\r\nquiet and reflective mood",
                    Risks = "rapid heart rate and rapid breathing (tachypnoea)\r\nhypertension (high blood pressure)\r\nheart palpitations\r\nchest pain\r\nvomiting\r\nkidney problems\r\npsychosis\r\nseizures\r\nstroke\r\ndeath.",
                    ImagePath = "/images/drugs/SyntheticDrugs.jpg"
                }
            );

            // Seed rehabilitation centers
            modelBuilder.Entity<RehabilitationCenter>().HasData(
                new RehabilitationCenter
                {
                    Id = 1,
                    Name = "Hope Recovery Center",
                    Location = "123 Main St, Anytown",
                    ContactInfo = "555-123-4567",
                    Services = "Inpatient, Outpatient, Counseling",
                    Description = "A comprehensive rehabilitation center offering various treatment programs.",
                    ImagePath = "/images/rehab/hope-center.jpg"
                },
                new RehabilitationCenter
                {
                    Id = 2,
                    Name = "New Beginnings Clinic",
                    Location = "456 Oak Ave, Somewhere",
                    ContactInfo = "555-987-6543",
                    Services = "Detox, Therapy, Aftercare",
                    Description = "Specialized in helping individuals overcome substance abuse.",
                    ImagePath = "/images/rehab/new-beginnings.jpg"
                }
            );
        }
    }
}