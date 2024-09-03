using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBankApi.Trainee.Dtos
{
    public class VerifyAdminCodeDto
    {
        // This property will hold the verification code entered by the trainer.
        public string Code { get; set; }

        public string Username { get; set; }
    }

    // Hardcoded admin verification code. Replace "YourHardcodedCode" with your desired code.
    public static class AdminVerificationConfig
    {
        public const string HardcodedAdminVerificationCode = "asdf";
    }
}
