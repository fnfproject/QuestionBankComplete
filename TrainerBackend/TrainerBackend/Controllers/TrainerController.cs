using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using QuestionBankApi.Trainer.Dtos;
using QuestionBankApi.Trainer.Services;
using QuestionBankAPI.Trainer.Services;
using QuestionBankDll.Trainer.Dtos;
using QuestionBankDll.Trainer.Models;
using QuestionBankDll.Trainer.Services;
using TrainerBackendDll.Dtos;
using TrainerBackendDll.Services;

namespace QuestionBankAPI.Trainer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("cors")]
    public class TrainerController : ControllerBase
    {
        private readonly IQuestionService _questionDataService;
        private readonly IResultDataService _resultDataService;
        private readonly IPracticePaperDataService _practicePaperDataService;
        private readonly ITestService _testDataService;

        private readonly string _uploadPath;
        private readonly string _imageFolder;
        private readonly string _imageUIFolder;


        public TrainerController(IQuestionService questionDataService, IResultDataService resultDataService, IPracticePaperDataService practicePaperDataService, ITestService testService)
        {
            _questionDataService = questionDataService;
            _resultDataService = resultDataService;
            _practicePaperDataService = practicePaperDataService;
            _testDataService = testService;

            _uploadPath = @"C:\Users\6147960\Desktop\Git\QuestionBankComplete\TrainerBackend\TrainerBackend\pdf\";
            _imageFolder = @"C:\Users\6147960\source\repos\ConsoleApp2\QuestionBank\TrainerBackend\TrainerBackend\Images\";
            _imageUIFolder = @"C:\Users\6147960\source\repos\ConsoleApp2\QuestionBank\TrainerBackendUI\TrainerBackendUI\TrainerBackendUI\wwwroot\Images\";
        }



        [HttpPost("generate-quiz")]
        public async Task<ActionResult<List<Question>>> GenerateQuiz([FromBody] List<int> selectedQuestionIds)
        {
            try
            {
                if (selectedQuestionIds == null || !selectedQuestionIds.Any())
                {
                    return BadRequest("No question selected.");
                }
                var data = _questionDataService.GenerateTestAsync(selectedQuestionIds);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpPost("AddSingleQuestion")] //single question of type text
        public async Task<ActionResult> AddSingleQuestion(QuestionDtos question)
        {
            try
            {
                 await _questionDataService.AddQuestion(question);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddBulkQuestions")] //Bulk upload from excel
        public async Task<IActionResult> AddQuestionsInBulk(IFormFile file)
        {
            if(file==null || file.Length == 0)
            {
                return BadRequest("The file field is required");
            }
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                if (!Directory.Exists(_imageFolder))
                    Directory.CreateDirectory(_imageFolder);

                using (var package = new ExcelPackage(file.OpenReadStream()))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var question = new QuestionDtos
                        {
                            Subject = _questionDataService.ProcessOption(worksheet.Cells[row, 1], _imageFolder),
                            Topic = _questionDataService.ProcessOption(worksheet.Cells[row, 2], _imageFolder),
                            DifficultyLevel = _questionDataService.ProcessOption(worksheet.Cells[row, 3], _imageFolder),
                            QuestionText = _questionDataService.ProcessOption(worksheet.Cells[row, 4], _imageFolder),
                            OptionA = _questionDataService.ProcessOption(worksheet.Cells[row, 5], _imageFolder),
                            OptionB = _questionDataService.ProcessOption(worksheet.Cells[row, 6], _imageFolder),
                            OptionC = _questionDataService.ProcessOption(worksheet.Cells[row, 7], _imageFolder),
                            OptionD = _questionDataService.ProcessOption(worksheet.Cells[row, 8], _imageFolder),
                            CorrectAnswer = _questionDataService.ProcessOption(worksheet.Cells[row, 9], _imageFolder),
                            Explaination = _questionDataService.ProcessOption(worksheet.Cells[row, 10], _imageFolder),
                           // CreatedBy = 1,
                            //CreatedAt = DateTime.Now
                            //UpdatedAt = DateTime.Now,

                        };
                         await _questionDataService.AddQuestion(question);


                    }
                    return Ok();

                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("Find/{id}")]
        public async Task<ActionResult<Question>> FindQuestion(int id)
        {
            try
            {
                var rec = await _questionDataService.FindQuestion(id);
                return Ok(rec);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddPracticePaper")]
        public async Task<IActionResult> AddPracticePaper(IFormFile file)
        {

            try
            {

                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file found");
                }
                if (!Directory.Exists(_uploadPath))
                    Directory.CreateDirectory(_uploadPath);
                var filePath = Path.Combine(_uploadPath, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                try
                {
                    await _practicePaperDataService.AddPracticePaper(filePath, file.FileName);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }


        [HttpPost("generate-pdf")]
        public async Task<IActionResult> GeneratePdf([FromBody] List<int> questionIds)
        {
            try
            {
                var pdfBytes = await _questionDataService.GeneratePdf(questionIds);
                return File(pdfBytes, "application/pdf", "SelectedQuestions.pdf");
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteQuestion/{id:int}")]
        public async Task<ActionResult> DeleteQuestion(int id)
        {
            try
            {
                await _questionDataService.DeleteQuestion(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeletePracticePaper/{practicePaper}")]
        public async Task<IActionResult> DeletePracticePaper(string practicePaper)
        {
            try
            {
                await _practicePaperDataService.DeletePracticePaper(practicePaper);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetByText/{name}")]
        public async Task<ActionResult<List<Question>>> FindQuestionByText(string text)
        {
            try
            {
                await _questionDataService.FindQuestionByText(text);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllTests")]
        public async Task<ActionResult<List<QuestionBankDll.Trainer.Dtos.TestDtos>>> GetAllTests()
        {
            try
            {
                return await _resultDataService.GetAllTests();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPracticePapers")]
        public async Task<ActionResult<List<string>>> GetPracticePapersNames()
        {
            try
            {
                return await _practicePaperDataService.GetAllPracticePaper();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("DownloadPracticePaper/{paperName}")]
        public async Task<IActionResult> DownloadPracticePaper(string paperName)
        {
            try
            {
                var fileBytes = await _practicePaperDataService.DownloadPracticePaperAsync(paperName);
                if (fileBytes == null)
                {
                    return BadRequest("File not found");
                }
                return File(fileBytes, "application/pdf", paperName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetAllQuestions")]
        public async Task<ActionResult<List<Question>>> GetAll()
        {
            try
            {
                return await _questionDataService.GetAllQuestions();
                //return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("ResultsByTestName/{name}")]
        public async Task<ActionResult<List<TestTraineeResultAPIDto>>> GetResultsByTestName(string name)
        {
            try
            {
                return await _resultDataService.GetResultByTestName(name);
                //return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ResultsByTraineeName/{name}")]
        public async Task<ActionResult<List<TraineeTestResultDto>>> GetResultsByTraineeName(string name)
        {
            try
            {
                return await _resultDataService.GetResultByTraineeName(name);
                //return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateQuestion(Question question)
        {
            try
            {
                await  _questionDataService.UpdateQue(question);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        
        }

        [HttpPost("test/GenerateTest")]
        public async Task<IActionResult> GenerateTest(GenerateTestRequest request)
        {
           // Console.WriteLine("This is in Controller");

            try
            {
                await _testDataService.GenerateTestAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
               // Console.WriteLine("This is in Controller");

                return BadRequest(ex.Message);
                //Console.WriteLine(ex.StackTrace);
            }

        }


        [HttpGet("test/{hyperlink}")]
        public async Task<IActionResult> GetTest(string hyperlink)
        {
            try
            {
                var test = await _testDataService.GetTestByHyperlinkAsync(hyperlink);
                return Ok(test);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the test.");
            }
        }

        [HttpPost("submit-test")]
        public async Task<IActionResult> SubmitTest([FromBody] SubmitTestRequest request)
        {
            try
            {
                await _testDataService.SubmitAnswersAsync(request.TestId, request.UserId, request.UserAnswers);
                return Ok("Test results saved successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while submitting the test.");
            }
        }

        [HttpPost("save-test")]
        public async Task<IActionResult> SaveTest([FromBody] SubmitTestRequest request)
        {
            try
            {
                await _testDataService.SaveTestResultsAsync(request.TestId, request.UserId, request.UserAnswerDtos);
                return Ok("Test results saved successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while submitting the test.");
            }
        }



    }
}
