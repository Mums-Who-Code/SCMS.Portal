// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using SCMS.Portal.Web.Models.Foundations.Students;
using SCMS.Portal.Web.Models.Views.StudentViews;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.StudentViews
{
    public partial class StudentViewServiceTests
    {
        [Fact]
        public async Task ShouldAddStudentViewAsync()
        {
            //given
            Guid currentLoggedInUserId = Guid.NewGuid();
            DateTimeOffset randomDateTime = GetRandomDate();

            dynamic randomStudentProperties =
                CreateRandomStudentViewProperties(
                    auditDates: randomDateTime,
                    auditIds: currentLoggedInUserId);

            var randomStudentView = new StudentView
            {
                FirstName = randomStudentProperties.FirstName,
                LastName = randomStudentProperties.LastName,
                DateOfBirth = randomStudentProperties.DateOfBirth,
            };

            var inputStudentView = randomStudentView;
            var expectedStudentView = inputStudentView;

            var randomStudent = new Student
            {
                Id = randomStudentProperties.Id,
                FirstName = randomStudentProperties.FirstName,
                LastName = randomStudentProperties.LastName,
                DateOfBirth = randomStudentProperties.DateOfBirth,
                Status = randomStudentProperties.Status,
                CreatedDate = randomStudentProperties.CreatedDate,
                UpdatedDate = randomStudentProperties.UpdatedDate,
                CreatedBy = randomStudentProperties.CreatedBy,
                UpdatedBy = randomStudentProperties.UpdatedBy,
            };

            Student expectedInputStudent = randomStudent;
            Student returnedStudent = expectedInputStudent;

            this.userServiceMock.Setup(service =>
                service.GetCurrentlyLoggedInUser())
                    .Returns(currentLoggedInUserId);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Returns(randomDateTime);

            this.studentServiceMock.Setup(service =>
                service.AddStudentAsync(It.Is(
                    SameStudentAs(expectedInputStudent))))
                        .ReturnsAsync(returnedStudent);

            //when
            StudentView actualStudentView =
                await this.studentViewService
                    .AddStudentViewAsync(inputStudentView);

            //then
            actualStudentView.Should().BeEquivalentTo(expectedStudentView);

            this.userServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInUser(),
                    Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
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
