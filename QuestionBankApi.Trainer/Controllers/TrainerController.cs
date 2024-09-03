using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestionBankApi.Trainer.Dtos;
using QuestionBankApi.Trainer.Services;
using QuestionBankAPI.Trainer.Services;
using QuestionBankDll.Trainer.Dtos;
using QuestionBankDll.Trainer.Models;
using QuestionBankDll.Trainer.Services;

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
        
        public TrainerController(IQuestionService questionDataService,IResultDataService resultDataService, IPracticePaperDataService practicePaperDataService)
        {
            _questionDataService = questionDataService;
            _resultDataService = resultDataService;
            _practicePaperDataService = practicePaperDataService;
        }


        [HttpPost("AddSingleQuestion")] //single question of type text
        public async Task<ActionResult> AddSingleQuestion(Question question)
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
            try
            {
                await _questionDataService.AddQuestionsInBulk(file);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddPracticePaper")]
        public async Task<IActionResult> AddPracticePaper(IFormFile file, string name)
        {
            try
            {
                await _practicePaperDataService.AddPracticePaper(file, name);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
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
            catch(Exception ex)
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
            catch(Exception ex)
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
                if(fileBytes == null)
                {
                    return BadRequest("File not found");
                }
                return File(fileBytes, "application/pdf", paperName);
            }
            catch(Exception ex)
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
            catch(Exception ex)
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
        public async Task<ActionResult> UpdateQuestion(QuestionDtos questionDto)
        {
            try
            {
                await _questionDataService.UpdateQuestionAsync(questionDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }



    }
}
