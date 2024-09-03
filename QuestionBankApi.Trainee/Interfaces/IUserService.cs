using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestionBankApi.Core.Dtos;
using QuestionBankApi.Core.Models;
using QuestionBankApi.Trainee.Dtos;

namespace QuestionBankApi.Trainee.Interfaces
{
    public interface IUserService
    {
        User RegisterUser(RegisterDto model, UserRole role);
        User AuthenticateUser(string username, string password);
        (byte[] QrCode, string SecretKey) EnableTwoFactorAuth(string username);
        bool VerifyTwoFactorCode(string username, string code);

        User MarkUserAsVerified(string username);

        bool VerifyAdminCode(string code);

        void UpdateAdminVerificationStatus(string username);


    }
}
