// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using SCMS.Portal.Web.Brokers.DateTimes;
using SCMS.Portal.Web.Brokers.Loggings;
using SCMS.Portal.Web.Models.Foundations.Students;
using SCMS.Portal.Web.Models.Views.StudentViews;
using SCMS.Portal.Web.Services.Foundations.Students;
using SCMS.Portal.Web.Services.Foundations.Users;

namespace SCMS.Portal.Web.Services.Views.StudentViews
{
    public class StudentViewService : IStudentViewService
    {
        private readonly IStudentService studentService;
        private readonly IUserService userService;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public StudentViewService(
            IStudentService studentService,
            IUserService userService,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.studentService = studentService;
            this.userService = userService;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<StudentView> AddStudentViewAsync(StudentView studentView)
        {
            Student student = MapToStudent(studentView);
            await this.studentService.AddStudentAsync(student);

            return studentView;
        }

        private Student MapToStudent(StudentView studentView)
        {
            Guid currentlyLoggedInUserId = this.userService.GetCurrentlyLoggedInUser();
            DateTimeOffset currentDateTime = dateTimeBroker.GetCurrentDateTime();

            return new Student
            {
                Id = Guid.NewGuid(),
                FirstName = studentView.FirstName,
                LastName = studentView.LastName,
                DateOfBirth = studentView.DateOfBirth,
                Status = StudentStatus.Active,
                CreatedDate = currentDateTime,
                UpdatedDate = currentDateTime,
                CreatedBy = currentlyLoggedInUserId,
                UpdatedBy = currentlyLoggedInUserId,
            };

        }
    }
}
