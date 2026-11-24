namespace Pr1_1609.BusinessLogic.Services
{
    public sealed class HR_StudentAddress_Services
    {
        private readonly DepartmentServyses services;

        public HR_StudentAddress_Services(DepartmentServyses services)
        {
            this.services = services;
        }

        public void GetStudentsWithAddresses() => services.GetStudentsWithAddresses();
    }
}
