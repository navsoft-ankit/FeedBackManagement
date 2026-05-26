using System.Globalization;
using System.Text;
using Authservice.DTOs.Export;
using Authservice.Repository;
using Authservice.Models;
using CsvHelper;

namespace Authservice.Service
{
    public class ExportService : IExportService
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public ExportService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task<(byte[] fileContent, string contentType, string fileName)>
            ExportFeedbackAsync(ExportDTO request)
        {
            var data = await _feedbackRepository.GetAnswersByDateAsync(
                request.FromDate,
                request.ToDate
            );

            return GenerateCsv(data);
        }

        // ================= CSV ONLY =================

        private (byte[], string, string) GenerateCsv(List<Answer> data)
        {
            using var memoryStream = new MemoryStream();
            using var writer = new StreamWriter(memoryStream);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            // optional: better column control

            csv.WriteField("Question");
            csv.WriteField("Answer");
            csv.WriteField("Feedback");
            csv.WriteField("Date");
            csv.NextRecord();

            foreach (var item in data)
            {
                csv.WriteField(item.Question?.Text);
                csv.WriteField(item.Response);
                csv.WriteField(item.Feedback?.title + " - " + item.Feedback?.FinalNote);
                csv.WriteField(item.CreatedAt.ToString("yyyy-MM-dd"));
                csv.NextRecord();
            }

            writer.Flush();

            return (
                memoryStream.ToArray(),
                "text/csv",
                "feedback.csv"
            );
        }
    }
}