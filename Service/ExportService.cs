// using System.Text;
// using Authservice.DTOs.Export;
// using Authservice.Repository;
// using OfficeOpenXml;
// using Authservice.Models;

// namespace Authservice.Service
// {
//     public class ExportService : IExportService
//     {
//         private readonly IFeedbackRepository _feedbackRepository;

//         public ExportService(IFeedbackRepository feedbackRepository)
//         {
//             _feedbackRepository = feedbackRepository;
//         }

//         public async Task<(byte[] fileContent,
//                            string contentType,
//                            string fileName)>
//             ExportFeedbackAsync(ExportDTO request)
//         {
//             var data = await _feedbackRepository
//                 .GetAnswersByDateAsync(
//                     request.FromDate,
//                     request.ToDate
//                 );

//             switch (request.format.ToLower())
//             {
//                 case "excel":
//                     return GenerateExcel(data);

//                 case "csv":
//                     return GenerateCsv(data);

//                 default:
//                     throw new Exception("Invalid format");
//             }
//         }

//         // ================= EXCEL =================

//         private (byte[], string, string)
//             GenerateExcel(List<Models.Answer> data)
//         {
//             ExcelPackage.LicenseContext =
//                 LicenseContext.NonCommercial;

//             using var package = new ExcelPackage();

//             var sheet =
//                 package.Workbook.Worksheets.Add("Feedback");

//             sheet.Cells[1, 1].Value = "Question";
//             sheet.Cells[1, 2].Value = "Answer";
//             sheet.Cells[1, 3].Value = "Date";

//             int row = 2;

//             foreach (var item in data)
//             {
//                 sheet.Cells[row, 1].Value =
//                     item.Question.Text;

//                 sheet.Cells[row, 2].Value =
//                     item.Response;

//                 sheet.Cells[row, 3].Value =
//                     item.CreatedAt.ToString("yyyy-MM-dd");

//                 row++;
//             }

//             var bytes = package.GetAsByteArray();

//             return (
//                 bytes,
//                 "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
//                 "feedback.xlsx"
//             );
//         }

//         // ================= CSV =================

//         private (byte[], string, string)
//             GenerateCsv(List<Models.Answer> data)
//         {
//             var builder = new StringBuilder();

//             builder.AppendLine("Question,Answer,Date");

//             foreach (var item in data)
//             {
//                 builder.AppendLine(
//                     $"\"{item.Question.Text}\"," +
//                     $"\"{item.Response}\"," +
//                     $"\"{item.CreatedAt:yyyy-MM-dd}\""
//                 );
//             }

//             return (
//                 Encoding.UTF8.GetBytes(builder.ToString()),
//                 "text/csv",
//                 "feedback.csv"
//             );
//         }
//     }
// }