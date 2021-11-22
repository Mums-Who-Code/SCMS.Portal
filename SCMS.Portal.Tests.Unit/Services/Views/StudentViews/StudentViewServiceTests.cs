// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using KellermanSoftware.CompareNetObjects;
using Moq;
using SCMS.Portal.Web.Brokers.DateTimes;
using SCMS.Portal.Web.Brokers.Loggings;
using SCMS.Portal.Web.Models.Foundations.Students;
using SCMS.Portal.Web.Models.Foundations.Students.Exceptions;
using SCMS.Portal.Web.Models.Views.StudentViews;
using SCMS.Portal.Web.Services.Foundations.Students;
using SCMS.Portal.Web.Services.Foundations.Users;
using SCMS.Portal.Web.Services.Views.StudentViews;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.StudentViews
{
    public partial class StudentViewServiceTests
    {
        private readonly Mock<IStudentService> studentServiceMock;
        private readonly Mock<IUserService> userServiceMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ICompareLogic compareLogic;
        private readonly IStudentViewService studentViewService;

        public StudentViewServiceTests()
        {
            this.studentServiceMock = new Mock<IStudentService>();
            this.userServiceMock = new Mock<IUserService>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            var compareConfig = new ComparisonConfig();
            compareConfig.IgnoreProperty<Student>(student => student.Id);
            this.compareLogic = new CompareLogic(compareConfig);

            this.studentViewService = new StudentViewService(
                studentService: this.studentServiceMock.Object,
                userService: this.userServiceMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }
        public static TheoryData DependencyValidationExceptions()
        {
            var innerException = new Xeption();

            return new TheoryData<Exception>
            {
                new StudentValidationException(innerException),
                new StudentDependencyValidationException(innerException)
            };
        }

        public static TheoryData DependencyExceptions()
        {
            var innerException = new Xeption();

            return new TheoryData<Exception>
            {
                new StudentDependencyException(innerException),
                new StudentServiceException(innerException)
            };
        }

        private static dynamic CreateRandomStudentViewProperties(
            DateTimeOffset auditDates,
            Guid auditIds)
        {
            return new
            {
                Id = Guid.NewGuid(),
                FirstName = GetRandomFirstName(),
                LastName = GetRandomLastName(),
                DateOfBirth = GetRandomDate(),
                Status = StudentStatus.Active,
                CreatedDate = auditDates,
                UpdatedDate = auditDates,
                CreatedBy = auditIds,
                UpdatedBy = auditIds,
            };
        }

        private Expression<Func<Student, bool>> SameStudentAs(Student expectedStudent)
        {
            return actualStudent => this.compareLogic.Compare(actualStudent, expectedStudent).AreEqual;
        }

        private static string GetRandomFirstName() =>
            new RealNames(NameStyle.FirstName).GetValue();

        private static string GetRandomLastName() =>
            new RealNames(NameStyle.LastName).GetValue();

        private static DateTimeOffset GetRandomDate() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }

        private static StudentView CreateRandomStudentView() =>
            CreateStudentViewFiller().Create();

        private static Filler<StudentView> CreateStudentViewFiller()
        {
            var filler = new Filler<StudentView>();

            filler.Setup().
                OnType<DateTimeOffset>().Use(GetRandomDate());

            return filler;
        }

    }
}
