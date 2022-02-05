// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using SCMS.Portal.Web.Brokers.DateTimes;
using SCMS.Portal.Web.Brokers.Loggings;
using SCMS.Portal.Web.Brokers.Navigations;
using SCMS.Portal.Web.Models.Foundations.Students;
using SCMS.Portal.Web.Models.Views.Foundations.StudentViews;
using SCMS.Portal.Web.Services.Foundations.Students;
using SCMS.Portal.Web.Services.Foundations.Users;

namespace SCMS.Portal.Web.Services.Views.Foundations.StudentViews
{
    public partial class StudentViewService : IStudentViewService
    {
        private readonly IStudentService studentService;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly IUserService userService;
        private readonly INavigationBroker navigationBroker;
        private readonly ILoggingBroker loggingBroker;

        public StudentViewService(
            IStudentService studentService,
            IDateTimeBroker dateTimeBroker,
            IUserService userService,
            INavigationBroker navigationBroker,
            ILoggingBroker loggingBroker)
        {
            this.studentService = studentService;
            this.dateTimeBroker = dateTimeBroker;
            this.userService = userService;
            this.navigationBroker = navigationBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<StudentView> AddStudentViewAsync(StudentView studentView) =>
        TryCatch(async () =>
        {
            ValidateStudentViewOnAdd(studentView);
            Student student = MapToStudent(studentView);
            Student addedStudent = await this.studentService.AddStudentAsync(student);

            return MapToStudentView(addedStudent);
        });

        public void NavigateTo(string route) =>
            throw new NotImplementedException();

        private Student MapToStudent(StudentView studentView)
        {
            DateTimeOffset currentDateTime = dateTimeBroker.GetCurrentDateTime();
            Guid currentlyLoggedInUserId = this.userService.GetCurrentlyLoggedInUser();

            return new Student
            {
                Id = Guid.NewGuid(),
                FirstName = studentView.FirstName,
                LastName = studentView.LastName,
                DateOfBirth = studentView.DateOfBirth,
                Gender = (StudentGender)studentView.Gender,
                FideId = studentView.FideId,
                Notes = studentView.Notes,
                SchoolId = studentView.SchoolId,
                Status = StudentStatus.Active,
                CreatedDate = currentDateTime,
                UpdatedDate = currentDateTime,
                CreatedBy = currentlyLoggedInUserId,
                UpdatedBy = currentlyLoggedInUserId,
            };

        }

        private StudentView MapToStudentView(Student student)
        {
            return new StudentView
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                DateOfBirth = student.DateOfBirth,
                Gender = (StudentGenderView)student.Gender,
                FideId = student.FideId,
                Notes = student.Notes,
                SchoolId = student.SchoolId
            };
        }
    }
}
