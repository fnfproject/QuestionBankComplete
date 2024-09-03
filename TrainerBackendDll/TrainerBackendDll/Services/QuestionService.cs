using iText.Forms.Form.Element;
using OfficeOpenXml.Drawing;
using OfficeOpenXml;
using QuestionBankDll.Trainer.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using iText.Layout;
using QuestionBankDll.Trainer.Dtos;
using Org.BouncyCastle.Pqc.Crypto.Lms;

namespace QuestionBankDll.Trainer.Services
{
    public interface IQuestionService
    {
        public void AddQuestion(Question question);
        //public Task AddQuestionsInBulk(IFormFile file);
        public string ProcessOption(ExcelRange cell, string _imageFolder);
        public Task<List<Question>> GetAllQuestions();
        public Task DeleteQuestion(int questionId);
        public Task<List<Question>> FindQuestionByText(string text);
        public Task<byte[]> GeneratePdf(List<int> questionIds);
        public Task UpdateQue(Question question);
        public Task<Question> FindQuestion(int questionId);
        public List<Question> GenerateTestAsync(List<int> questions);
    }
    public class QuestionService : IQuestionService
    {
        private readonly QuestionDbContext _context;
        private static readonly string _imageFolder = "C:\\Users\\6147954\\Documents\\QuestionBank.Trainer\\QuestionBankApi.Trainer\\Images\\";
        private static readonly string _imageUIFolder = "C:\\Users\\6147954\\Documents\\QuestionBank.Trainer\\QuestionBankUI.Trainer\\QuestionBankUI.Trainer\\wwwroot\\Images\\";

        public QuestionService(QuestionDbContext context)
        {
            _context = context;
        }
        public void AddQuestion(Question question)
        {
            try
            {

                question.CreatedBy = 1;
                question.CreatedAt = DateTime.Now;
                
                
                _context.Questions.Add(question);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }
        async Task<Question> IQuestionService.FindQuestion(int questionId)
        {
            try
            {
                var question = await _context.Questions.FirstOrDefaultAsync(q => q.QuestionId == questionId);
                if (question == null)
                {
                    throw new Exception("Question record not found");
                }
                return new Question
                {
                    QuestionId = questionId,
                    Subject = question.Subject,
                    Topic = question.Topic,
                    DifficultyLevel = question.DifficultyLevel,
                    QuestionText = question.QuestionText,
                    OptionA = question.OptionA,
                    OptionB = question.OptionB,
                    OptionC = question.OptionC,
                    OptionD = question.OptionD,
                    CorrectAnswer = question.CorrectAnswer,
                    Explaination = question.Explaination,
                    CreatedBy = (int)(question?.CreatedBy),
                    CreatedAt = (DateTime)(question?.CreatedAt),
                    //UpdatedAt  = (DateTime)(question?.UpdatedAt)
                };
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }
       
        public string ProcessOption(ExcelRange cell, string _imageFolder)
        {


            if (cell.Value is string)
            {
                return cell.Text;
            }
            else
            {
                int row = cell.Start.Row;
                int column = cell.Start.Column;
                var worksheet = cell.Worksheet;
                if (worksheet.Drawings.Count > 0)
                {
                    foreach (var drawing in worksheet.Drawings)
                    {
                        if (drawing is ExcelPicture picture && picture.From.Row == row - 1 && picture.From.Column == column - 1)
                        {
                            // Generate a unique file name for the image
                            var imageName = Guid.NewGuid().ToString() + ".jpg";
                            var imageFullPath = Path.Combine(_imageFolder, imageName);
                            picture.Image.Save(imageFullPath, ImageFormat.Jpeg);

                            // Console.WriteLine(imageFullPath);
                            //return imageFullPath;
                            return $"/Images/{imageName}";

                        }
                    }
                }
                return cell.Text;
            }

        }

        public Task DeleteQuestion(int questionId)
        {
            try
            {
                var question = _context.Questions.Find(questionId);
                _context.Questions.Remove(question);
                _context.SaveChanges();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        async Task<List<Question>> IQuestionService.GetAllQuestions()
        {
            try
            {
                return await _context.Questions.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        async Task<List<Question>> IQuestionService.FindQuestionByText(string text)
        {
            try
            {
                var questions = await _context.Questions.Where(q =>
                q.QuestionText.Contains(text) ||
                q.OptionA.Contains(text) ||
                q.OptionB.Contains(text) ||
                q.OptionC.Contains(text) ||
                q.OptionD.Contains(text) ||
                q.CorrectAnswer.Contains(text)).ToListAsync();
                if (questions == null || questions.Count == 0)
                {
                    throw new Exception("Question record not found");
                }
                return questions;

            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        async Task<byte[]> IQuestionService.GeneratePdf(List<int> questionIds)
        {
            try
            {
                var questions = await _context.Questions.Where(q => questionIds.Contains(q.QuestionId)).ToListAsync();
                if (questions == null || questions.Count == 0)
                {
                    throw new ArgumentException("No questions found for the provided IDs.");
                }
                using (var ms = new MemoryStream())
                {
                    PdfWriter writer = new PdfWriter(ms);
                    PdfDocument pdf = new PdfDocument(writer);
                    Document document = new Document(pdf);

                    // Add questions to the PDF
                    foreach (var question in questions)
                    {
                        AddContentToPdf(document, "Question", question.QuestionText);
                        AddContentToPdf(document, "A", question.OptionA);
                        AddContentToPdf(document, "B", question.OptionB);
                        AddContentToPdf(document, "C", question.OptionC);
                        AddContentToPdf(document, "D", question.OptionD);
                        document.Add(new Paragraph(" ")); // Empty line
                    }

                    document.Close();

                    return ms.ToArray();

                }
            }
            catch (Exception pdfEx)
            {
                throw new Exception(pdfEx.Message);
            }
        }
        private static void AddContentToPdf(Document document, string optionLabel, string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                document.Add(new Paragraph($"{optionLabel}: N/A"));
            }
            else if (IsImagePath(content))
            {
                // Assuming the content is a relative image path
                var imagePath = Path.Combine("C:\\Users\\6147954\\source\\repos\\ProjectAPI\\", content);

                if (File.Exists(imagePath))
                {
                    ImageData imageData = ImageDataFactory.Create(imagePath);
                    iText.Layout.Element.Image image = new iText.Layout.Element.Image(imageData);
                    float imageHeightInPoints = 10 * 12f; // Approx. 12 points per line, so 15 lines = 15 * 12 = 180 points
                    float imageAspectRatio = image.GetImageWidth() / image.GetImageHeight(); // Width/Height
                    float imageWidthInPoints = imageHeightInPoints * imageAspectRatio;

                    image.ScaleToFit(imageWidthInPoints, imageHeightInPoints);
                    document.Add(new Paragraph($"{optionLabel}:"));
                    document.Add(image);
                }
                else
                {
                    document.Add(new Paragraph($"{optionLabel}: [Image not found]"));
                }
            }
            else
            {
                // Assuming the content is plain text
                document.Add(new Paragraph($"{optionLabel}: {content}"));
            }
        }


        //Task IQuestionService.AddQuestionsInBulk(IFormFile file)
        //{

        //    _context.Add(question);
        //    _context.SaveChanges();


        //}





        private static bool IsImagePath(string content)
        {
            // Check if the content is a relative path (e.g., "Images\file.png" or "Images/file.jpg")
            return content.StartsWith("Images\\") || content.StartsWith("Images/");
        }





        public Task UpdateQue(Question question)
        {
            try
            {
                var found = _context.Questions.SingleOrDefault(q => q.QuestionId == question.QuestionId);
                //found.QuestionId = question.QuestionId;
                found.Subject = question.Subject;
                found.Topic = question.Topic;
                found.DifficultyLevel = question.DifficultyLevel;
                found.QuestionText = question.QuestionText;
                found.OptionA = question.OptionA;
                found.OptionB = question.OptionB;
                found.OptionC = question.OptionC;
                found.OptionD = question.OptionD;
                found.Explaination = question.Explaination;
                found.CreatedBy = 1;
                found.CreatedAt = DateTime.Now;
                return _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        public List<Question> GenerateTestAsync(List<int> questions)
        {
            try
            {
                var quizQuestions = _context.Questions.Where(q => questions.Contains(q.QuestionId)).ToList();
                return quizQuestions;
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }
    }
}
