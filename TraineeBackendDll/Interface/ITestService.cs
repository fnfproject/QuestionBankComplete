using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeBackendDll.Dtos;

namespace TraineeBackendDll.Interface
{
    public interface ITestService
    {
        Task<TestDetailsDto> GetTestDetailsAsync(int testId);
    }
}
