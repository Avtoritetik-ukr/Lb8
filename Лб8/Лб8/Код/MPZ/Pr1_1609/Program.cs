using System;

using Pr1_1609.Domain;
using Pr1_1609.DataAccess;
using Pr1_1609.BusinessLogic.CRUD;
using Pr1_1609.BusinessLogic.Services;
using Pr1_1609.BusinessLogic.Navigation;
using Pr1_1609.Presentation.Main;
using Pr1_1609.Presentation.HR;
using Pr1_1609.Presentation.Department;
using Pr1_1609.Infrastructure;

namespace Pr1_1609
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = OperatingSystem.IsWindows() && !Console.IsInputRedirected
                ? System.Text.Encoding.Unicode
                : System.Text.Encoding.UTF8;

            DB db = new DB();
            Logger.Init(db.Config.GetLogPath());

            StateMachine stateMachine = new StateMachine(db);
            DepartmentServyses departmentServices = new DepartmentServyses(db);

            UserRole initialRole = Main_Menu.SelectRole();
            stateMachine.Role = initialRole;
            Logger.Write("Вхід", $"Роль: {initialRole}");

            stateMachine.TransitionTo(MenuState.MainMenu);

            HR_CRUD_Student hrCrudStudent = new HR_CRUD_Student(db, stateMachine);
            HR_CRUD_Address hrCrudAddress = new HR_CRUD_Address(db, stateMachine);
            HR_CRUD_Group hrCrudGroup = new HR_CRUD_Group(db, stateMachine);

            Student_CRUD_Menu studentMenu = new Student_CRUD_Menu(hrCrudStudent);
            Address_CRUD_Menu addressMenu = new Address_CRUD_Menu(hrCrudAddress);
            Group_CRUD_Menu groupMenu = new Group_CRUD_Menu(hrCrudGroup);
            HR_CRUD_Menu hrCrudMenu = new HR_CRUD_Menu(stateMachine, studentMenu, addressMenu, groupMenu);

            HR_StudentAddress_Services hrStudentAddressServices = new HR_StudentAddress_Services(departmentServices);
            HR_Student_Services hrStudentServices = new HR_Student_Services(departmentServices);

            HR_StudentAddress_Services_Menu hrStudentAddressServicesMenu = new HR_StudentAddress_Services_Menu(hrStudentAddressServices);
            HR_Student_Services_Menu hrStudentServicesMenu = new HR_Student_Services_Menu(hrStudentServices);
            HR_Services_Menu hrServicesMenu = new HR_Services_Menu(stateMachine, hrStudentAddressServicesMenu, hrStudentServicesMenu);

            HR_Menu hrMenu = new HR_Menu(stateMachine, hrCrudMenu, hrServicesMenu);

            Department_CRUD_Subject departmentCrudSubject = new Department_CRUD_Subject(db, stateMachine);
            Department_CRUD_GroupSubject departmentCrudGroupSubject = new Department_CRUD_GroupSubject(db, stateMachine);

            Subject_CRUD_Menu subjectMenu = new Subject_CRUD_Menu(departmentCrudSubject);
            GroupSubject_CRUD_Menu groupSubjectMenu = new GroupSubject_CRUD_Menu(departmentCrudGroupSubject);
            Department_CRUD_Menu departmentCrudMenu = new Department_CRUD_Menu(stateMachine, subjectMenu, groupSubjectMenu);

            Department_StudentAddress_Services departmentStudentAddressServices = new Department_StudentAddress_Services(departmentServices);
            Department_Student_Services departmentStudentServices = new Department_Student_Services(departmentServices);

            Department_StudentAddress_Services_Menu departmentStudentAddressServicesMenu = new Department_StudentAddress_Services_Menu(departmentStudentAddressServices);
            Department_Student_Services_Menu departmentStudentServicesMenu = new Department_Student_Services_Menu(departmentStudentServices);
            Department_Services_Menu departmentServicesMenu = new Department_Services_Menu(stateMachine, departmentStudentAddressServicesMenu, departmentStudentServicesMenu);

            Department_Menu departmentMenu = new Department_Menu(stateMachine, departmentCrudMenu, departmentServicesMenu);

            Main_Menu mainMenu = new Main_Menu(stateMachine, departmentServices, hrMenu, departmentMenu);
            mainMenu.HandleMenu();
        }
    }
}