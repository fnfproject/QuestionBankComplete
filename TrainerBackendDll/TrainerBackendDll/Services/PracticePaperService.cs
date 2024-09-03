using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuestionBankDll.Trainer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBankDll.Trainer.Services
{
    public interface IPracticePaperService
    {
        public Task AddPracticePaper(string filePath, string fileName);
        public Task DeletePracticePaper(string practicePaper);
        public Task<byte[]> DownloadPracticePaperAsync(string paperName);

        public Task<List<string>> GetAllPracticePaper();
    }
    public class PracticePaperService : IPracticePaperService
    {
        private readonly QuestionDbContext _context;
        private readonly string _pdfFolderPath;
        public int paperId =6;
        public PracticePaperService(QuestionDbContext context)
        {
            _context = context;
            _pdfFolderPath = "C:\\Users\\6147960\\source\\repos\\ConsoleApp2\\QuestionBank\\TrainerBackendUI\\TrainerBackendUI\\TrainerBackendUI\\pdf\\";

            if (!Directory.Exists(_pdfFolderPath))
            {
                Directory.CreateDirectory(_pdfFolderPath);
            }
        }
        
        public async Task AddPracticePaper(string filePath, string fileName)
        {
            try
            {
                paperId++;
                string keyword = "\\pdf";
                    var startIndex = filePath.IndexOf(keyword);
                if (startIndex != -1)
                {
                    //string result = filePath.Substring(startIndex);
                    filePath = filePath.Substring(startIndex);
                }
                

                // Create a new PracticePaper entry
                var practicePaper = new PracticePaper
                {
                    //PaperId = paperId,
                    PaperName = fileName,
                    Subject = filePath,
                    CreatedBy = 1, // Example: Set the creator ID. Adjust as needed
                   CreatedAt = DateTime.UtcNow
                };
                _context.PracticePapers.Add(practicePaper);

               _context.SaveChanges();
            }
            catch ( Exception ex)
            {

                throw ex.InnerException ?? ex;

            }
           
          
        }

        public Task DeletePracticePaper(string practicePaper)
        {
            try
            {
                var found = _context.PracticePapers.FirstOrDefault(p => p.PaperName == practicePaper);
                _context.PracticePapers.Remove(found);
                _context.SaveChanges();
                return Task.CompletedTask;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public  Task<byte[]> DownloadPracticePaperAsync(string paperName)
        {
            var practicePaper =  _context.PracticePapers
                .Where(pp => pp.PaperName == paperName)
                .FirstOrDefaultAsync();

            if (practicePaper == null)
            {
                throw new Exception("No such file found"); // Or another appropriate response
            }

            var filePath = Path.Combine(_pdfFolderPath, paperName);

            if (!System.IO.File.Exists(filePath))
            {
                throw new Exception("No such file exists in the directory"); // Or another appropriate response
            }

            return  System.IO.File.ReadAllBytesAsync(filePath);
           
        }

        public  Task<List<string>> GetAllPracticePaper()
        {
            try
            {
                var paperNames =  _context.PracticePapers.Select(pp => pp.PaperName).ToListAsync();
                return paperNames;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
