// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SCMS.Portal.Web.Models.Foundations.Students;
using SCMS.Portal.Web.Models.Views.Foundations.StudentViews;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.Foundations.StudentViews
{
    public partial class StudentViewServiceTests
    {
        [Fact]
        public async Task ShouldAddStudentViewAsync()
        {
            //given
            Guid currentLoggedInUserId = Guid.NewGuid();
            DateTimeOffset randomDateTime = GetRandomDate();

            dynamic randomStudentViewProperties =
                CreateRandomStudentViewProperties(
                    auditDates: randomDateTime,
                    auditIds: currentLoggedInUserId);

            var randomStudentView = new StudentView
            {
                FirstName = randomStudentViewProperties.FirstName,
                LastName = randomStudentViewProperties.LastName,
                DateOfBirth = randomStudentViewProperties.DateOfBirth,
                Gender = (StudentGenderView)randomStudentViewProperties.Gender,
                SchoolId = randomStudentViewProperties.SchoolId
            };

            var inputStudentView = randomStudentView;
            var expectedStudentView = inputStudentView.DeepClone();

            var randomStudent = new Student
            {
                Id = randomStudentViewProperties.Id,
                FirstName = randomStudentViewProperties.FirstName,
                LastName = randomStudentViewProperties.LastName,
                DateOfBirth = randomStudentViewProperties.DateOfBirth,
                Gender = (StudentGender)randomStudentViewProperties.Gender,
                SchoolId = randomStudentViewProperties.SchoolId,
                Status = randomStudentViewProperties.Status,
                CreatedDate = randomStudentViewProperties.CreatedDate,
                UpdatedDate = randomStudentViewProperties.UpdatedDate,
                CreatedBy = randomStudentViewProperties.CreatedBy,
                UpdatedBy = randomStudentViewProperties.UpdatedBy,
            };

            Student expectedInputStudent = randomStudent;
            Student persistedStudent = expectedInputStudent.DeepClone();

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Returns(randomDateTime);

            this.userServiceMock.Setup(service =>
                service.GetCurrentlyLoggedInUser())
                    .Returns(currentLoggedInUserId);

            this.studentServiceMock.Setup(service =>
                service.AddStudentAsync(It.Is(
                    SameStudentAs(expectedInputStudent))))
                        .ReturnsAsync(persistedStudent);

            //when
            StudentView actualStudentView =
                await this.studentViewService
                    .AddStudentViewAsync(inputStudentView);

            //then
            actualStudentView.Should().BeEquivalentTo(expectedStudentView);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.userServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInUser(),
                    Times.Once);


            this.studentServiceMock.Verify(service =>
                service.AddStudentAsync(It.Is(
                    SameStudentAs(expectedInputStudent))),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.studentServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
