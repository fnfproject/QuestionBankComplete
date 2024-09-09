namespace BlazorTrainee.Model
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
