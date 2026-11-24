using Pr1_1609.Infrastructure;

namespace Pr1_1609.BusinessLogic.Services
{
    public sealed class HR_Student_Services
    {
        private readonly DepartmentServyses services;

        public HR_Student_Services(DepartmentServyses services)
        {
            this.services = services;
        }

        public void GetStudentBySurname()
        {
            string surname = InputHelper.ReadString("Введіть прізвище студента: ");
            services.GetStudentInfoByLastName(surname);
        }
    }
}
