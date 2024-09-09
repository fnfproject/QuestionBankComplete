using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeBackendDll.Models
{
    public class RoleService
    {
        private string _role;

        public void SetRole(string role)
        {
            _role = role;
        }

        public string GetRole()
        {
            return _role;
        }
    }
}
