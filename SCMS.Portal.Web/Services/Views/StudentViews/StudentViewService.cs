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
    public partial class StudentViewService : IStudentViewService
    {
        private readonly IStudentService studentService;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly IUserService userService;
        private readonly ILoggingBroker loggingBroker;

        public StudentViewService(
            IStudentService studentService,
            IDateTimeBroker dateTimeBroker,
            IUserService userService,
            ILoggingBroker loggingBroker)
        {
            this.studentService = studentService;
            this.dateTimeBroker = dateTimeBroker;
            this.userService = userService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<StudentView> AddStudentViewAsync(StudentView studentView) =>
        TryCatch(async () =>
        {
            ValidateStudentViewOnAdd(studentView);
            Student student = MapToStudent(studentView);
            await this.studentService.AddStudentAsync(student);

            return studentView;
        });


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
                Status = StudentStatus.Active,
                CreatedDate = currentDateTime,
                UpdatedDate = currentDateTime,
                CreatedBy = currentlyLoggedInUserId,
                UpdatedBy = currentlyLoggedInUserId,
            };

        }
    }
}
