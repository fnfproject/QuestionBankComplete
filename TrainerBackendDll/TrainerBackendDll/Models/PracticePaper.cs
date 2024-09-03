using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBankDll.Trainer.Models
{
    public class PracticePaper
    {
        public int PaperId { get; set; }
        [Required]
        public string PaperName { get; set; }
        [Required]
        public string Subject { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
