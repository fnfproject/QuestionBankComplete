using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeBackendDll.Models
{
    [Table("TestQuestion")]
    public class TestQuestion
    {

        [ForeignKey("TestId")]
        public int TestId { get; set; }

        [ForeignKey("QuestionId")]
        public int QuestionId { get; set; }
    }
}
