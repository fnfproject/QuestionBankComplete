
using QuestionBankDll.Trainer.Services;

namespace QuestionBankApi.Trainer.Services
{
    public interface IPracticePaperDataService
    {
        public Task AddPracticePaper(string filePath, string fileName);
        public Task DeletePracticePaper(string practicePaper);
        public Task<byte[]> DownloadPracticePaperAsync(string paperName);

        public Task<List<string>> GetAllPracticePaper();
    }
    public class PracticePaperDataService : IPracticePaperDataService
    {
        private readonly IPracticePaperService _practicePaperService;

        public PracticePaperDataService(IPracticePaperService service)
        {
            _practicePaperService = service;
        }
        public Task AddPracticePaper(string filePath, string fileName)
        {
            try
            {
               return Task.Run(()=> _practicePaperService.AddPracticePaper(filePath, fileName));

            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        public Task DeletePracticePaper(string practicePaper)
        {
            try
            {
                return Task.Run(() => _practicePaperService.DeletePracticePaper(practicePaper));
            }
            catch (Exception ex)
            {

                throw ex.InnerException ?? ex;

            }
        }

        public Task<byte[]> DownloadPracticePaperAsync(string paperName)
        {
            try
            {
                return Task.Run(() => _practicePaperService.DownloadPracticePaperAsync(paperName));
            }
            catch (Exception ex)
            {

                throw ex.InnerException ?? ex;

            }
        }

        public Task<List<string>> GetAllPracticePaper()
        {
            try
            {
                return Task.Run(() => _practicePaperService.GetAllPracticePaper());
            }
            catch (Exception ex)
            {

                throw ex.InnerException ?? ex;

            }
        }
    }
}
