namespace Pr1_1609.BusinessLogic.Services
{
    public sealed class Department_StudentAddress_Services
    {
        private readonly DepartmentServyses services;

        public Department_StudentAddress_Services(DepartmentServyses services)
        {
            this.services = services;
        }

        public void GetStudentsWithAddresses() => services.GetAllInformationAboutStudients();
    }
}
