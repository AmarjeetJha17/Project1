using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DrugAbuseReportingSystem.Migrations
{
    /// <inheritdoc />
    public partial class InsertData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DrugInformation",
                columns: new[] { "Id", "Description", "Effects", "ImagePath", "Name", "Risks" },
                values: new object[,]
                {
                    { 4, "Heroin is an opioid drug made from morphine, a natural substance taken from the seed pod of the various opium poppy plants grown in Southeast and Southwest Asia, Mexico, and Colombia.", "People who use heroin report feeling a rush (a surge of pleasure). And then they may feel other effects, such as a warm flushing of the skin, dry mouth, and a heavy feeling in the arms and legs. They may also have severe itching, nausea, and vomiting.", "/images/drugs/heroine.jpg", "Heroin", "People who use heroin over the long term may develop many different health problems. These problems could include liver, kidney, and lung disease, mental disorders, and abscesses." },
                    { 5, "Methamphetamines are stimulants, a type of drug that lets people stay awake and do continuous activity with less need for sleep", "typically include feelings of euphoria and increased alertness and energy. It can also cause serious negative health effects, including paranoia, anxiety, rapid heart rate, irregular heartbeat, stroke, or even death", "/images/drugs/metham.jpg", "Methamphetamine", "use may lead to insomnia, memory loss, development of a substance use disorder, and other health problems." },
                    { 6, "MDMA, commonly known as ecstasy or molly, is an empathogen-entactogenic drug with stimulant and minor psychedelic properties. ", "MDMA’s effects may include feeling more energetic and alert and having an increased sense of well-being, warmth, and openness toward others.", "/images/drugs/MDMA.jpg", "MDMA", "insomnia, confusion, irritability, anxiety, and depression, impulsiveness and aggression, decreased interest in sex, memory and attention problems, reduced appetite" },
                    { 7, "Prescription drugs are medications that require a written order from a licensed healthcare professional (like a doctor) to be dispensed by a pharmacist", "Anxiety, Confusion, Dizziness, Drowsiness, Euphoria, Excessive sleep or inability to sleep, Hyperactivity, Mood swings, Nausea, Unusual muscle movements", "/images/drugs/prescriptionDrugs.jpg", "PrescriptionDrugs", "Anxiety, Changes in appearance (weight loss or weight gain), Changes in physical health, Damage to the heart, kidneys, liver and brain, Depression, Drug addiction, Drug dependence, Mental health decline" },
                    { 8, "Synthetic drugs are substances that are created or modified in a laboratory setting, as opposed to being naturally occurring. They can have both therapeutic and psychoactive effects", "aim to mimic the effects of existing illicit drugs (such as cannabis, cocaine, MDMA and LSD).Euphoria\r\nfeelings of wellbeing\r\nspontaneous laughter and excitement\r\nincreased appetite\r\ndry mouth\r\nquiet and reflective mood", "/images/drugs/SyntheticDrugs.jpg", "SyntheticDrugs", "rapid heart rate and rapid breathing (tachypnoea)\r\nhypertension (high blood pressure)\r\nheart palpitations\r\nchest pain\r\nvomiting\r\nkidney problems\r\npsychosis\r\nseizures\r\nstroke\r\ndeath." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DrugInformation",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DrugInformation",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DrugInformation",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DrugInformation",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "DrugInformation",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
