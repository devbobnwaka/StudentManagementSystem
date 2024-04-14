using System.IO;
using Microsoft.AspNetCore.Mvc;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using StudentManagementSystem.Models;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Kernel.Colors;



namespace StudentManagementSystem.Controllers
{
    public class FileController : Controller
    {
        private readonly string _active = "active";
        private readonly IResultRepository _resultRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ISubjectRepository _subjectRepository;

        public FileController(IResultRepository resultRepository,ISubjectRepository subjectRepository, IStudentRepository studentRepository)
        {
            _resultRepository = resultRepository;
            _subjectRepository = subjectRepository;
            _studentRepository = studentRepository;
        }
        public async Task<IActionResult> GeneratePDF(Class? klass, string? name, string? subject)
        {

            List<Subject> subjects = await _subjectRepository.GetAllSubject();
            List<Result> results = await _resultRepository.GetAllResult();
            //IEnumerable<Student> students = await _studentRepository.GetAllStudent();
            int numberOfSubject = subjects.Count;
            ViewData["result"] = _active;
            ViewData["name"] = name;
            byte[] pdfBytes;

            IEnumerable<Result>? model;
            if (klass != null)
            {
                List<Student> students = await _studentRepository.GetClassStudent((Class)klass);
                model = await _resultRepository.GetClassResult((Class)klass);
                // Generate PDF using iText 7
                using (MemoryStream ms = new MemoryStream())
                {
                    PdfWriter writer = new PdfWriter(ms);
                    PdfDocument pdf = new PdfDocument(writer);
                    Document document = new Document(pdf);
                    Paragraph header =new Paragraph("Class Result")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(20);
                    Paragraph studentClass = new Paragraph(klass.ToString())
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(10);
                    LineSeparator ls = new LineSeparator(new SolidLine());


                    // Table
                    Table table = new Table(numberOfSubject+1, false);
                    Cell cell11 = new Cell(1, 1)
                       .SetBackgroundColor(ColorConstants.GRAY)
                       .SetTextAlignment(TextAlignment.CENTER)
                       .Add(new Paragraph("Name"));
                    table.AddCell(cell11);
                    foreach(var subj in subjects)
                    {                      
                        table.AddCell(new Cell(1, 1)
                           .SetBackgroundColor(ColorConstants.GRAY)
                           .SetTextAlignment(TextAlignment.CENTER)
                           .Add(new Paragraph(subj.Name)));
                    }

                    foreach (var student in students)
                    {
                        table.AddCell(new Cell(1, 1)
                           //.SetBackgroundColor(ColorConstants.GRAY)
                           .SetTextAlignment(TextAlignment.CENTER)
                           .Add(new Paragraph(student.Name)));
                        foreach (var sub in subjects)
                        {
                            Result res = await _resultRepository.GetResultByStudentbySubject(student, sub);
                            string mark = res != null ? res.Mark.ToString() : String.Empty;
                            table.AddCell(new Cell(1, 1)
                              //.SetBackgroundColor(ColorConstants.GRAY)
                              .SetTextAlignment(TextAlignment.CENTER)
                              .Add(new Paragraph(mark)));
                        }
                    }

                    document.Add(header);
                    document.Add(ls);
                    document.Add(studentClass);
                    document.Add(table);
                    document.Close();
                    pdfBytes = ms.ToArray();
                }

                return File(pdfBytes, "application/pdf");

            }
            else if (name != null)
            {
                List<Result> studentResults = await _resultRepository.GetStudentResult(name);
                Result result = studentResults.FirstOrDefault();

                using (MemoryStream ms = new MemoryStream())
                {
                    PdfWriter writer = new PdfWriter(ms);
                    PdfDocument pdf = new PdfDocument(writer);
                    Document document = new Document(pdf);
                    Paragraph header = new Paragraph($"{result.Student.Name} Results")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(20);
                    Paragraph studentKlass = new Paragraph($"Class: {result.Student.Class}")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(20);
                    LineSeparator ls = new LineSeparator(new SolidLine());


                    document.Add(header);
                    document.Add(studentKlass);
                    document.Add(ls);
                    foreach (var res in studentResults)
                    {
                        document.Add( new Paragraph($"{res.Subject.Name}: {res.Mark}")
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFontSize(15));
                    }
                    document.Close();
                    pdfBytes = ms.ToArray();
                }
                return File(pdfBytes, "application/pdf");
            }

            model = await _resultRepository.GetAllResult();
            return View("ListResult", model);
        }

    }
}
