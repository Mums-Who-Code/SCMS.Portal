// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Bunit;
using FluentAssertions;
using Moq;
using SCMS.Portal.Web.Models.Views.Components.Colors;
using SCMS.Portal.Web.Models.Views.StudentViews;
using SCMS.Portal.Web.Views.Components.StudentRegistrations;
using Xeptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.Components.StudentRegistrations
{
    public partial class StudentRegistrationComponentTests : TestContext
    {
        [Theory]
        [MemberData(nameof(StudentViewValidationExceptions))]
        public void ShouldRenderInnerExceptionMessageIfValidationErrorOccurs(
            Xeption studentViewValidationException)
        {
            // given
            string expectedErrorMessage =
                studentViewValidationException.InnerException.Message;

            this.studentViewServiceMock.Setup(service =>
                service.AddStudentViewAsync(It.IsAny<StudentView>()))
                    .ThrowsAsync(studentViewValidationException);

            // when
            this.renderedStudentRegistrationComponent =
                RenderComponent<StudentRegistrationComponent>();

            this.renderedStudentRegistrationComponent.Instance
                .RegisterButton.Click();

            // then
            this.renderedStudentRegistrationComponent.Instance
                .StatusLabel.Value.Should().Be(expectedErrorMessage);

            this.renderedStudentRegistrationComponent.Instance
                .StatusLabel.Color.Should().Be(Color.Red);

            this.renderedStudentRegistrationComponent.Instance
                .FirstNameTextBox.IsDisabled.Should().BeFalse();

            this.renderedStudentRegistrationComponent.Instance
                .LastNameTextBox.IsDisabled.Should().BeFalse();

            this.renderedStudentRegistrationComponent.Instance
                .DateOfBirthPicker.IsDisabled.Should().BeFalse();

            this.renderedStudentRegistrationComponent.Instance
                .RegisterButton.IsDisabled.Should().BeFalse();

            this.studentViewServiceMock.Verify(service =>
                service.AddStudentViewAsync(It.IsAny<StudentView>()),
                    Times.Once);

            this.studentViewServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(StudentViewDependencyExceptions))]
        public void ShouldRenderExceptionMessageIfDependencyErrorOccurs(
            Xeption studentViewDependencyException)
        {
            // given
            string expectedErrorMessage =
                studentViewDependencyException.Message;

            this.studentViewServiceMock.Setup(service =>
                service.AddStudentViewAsync(It.IsAny<StudentView>()))
                    .ThrowsAsync(studentViewDependencyException);

            // when
            this.renderedStudentRegistrationComponent =
                RenderComponent<StudentRegistrationComponent>();

            this.renderedStudentRegistrationComponent.Instance
                .RegisterButton.Click();

            // then
            this.renderedStudentRegistrationComponent.Instance
                .StatusLabel.Value.Should().Be(expectedErrorMessage);

            this.renderedStudentRegistrationComponent.Instance
                .StatusLabel.Color.Should().Be(Color.Red);

            this.renderedStudentRegistrationComponent.Instance
                .FirstNameTextBox.IsDisabled.Should().BeFalse();

            this.renderedStudentRegistrationComponent.Instance
                .LastNameTextBox.IsDisabled.Should().BeFalse();

            this.renderedStudentRegistrationComponent.Instance
                .DateOfBirthPicker.IsDisabled.Should().BeFalse();

            this.renderedStudentRegistrationComponent.Instance
                .RegisterButton.IsDisabled.Should().BeFalse();

            this.studentViewServiceMock.Verify(service =>
                service.AddStudentViewAsync(It.IsAny<StudentView>()),
                    Times.Once);

            this.studentViewServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldRenderExceptionMessageIfRenderErrorOccurs()
        {
            // given
            string randomMessage = GetRandomString();
            string expectedErrorMessage = randomMessage;
            var exception = new Exception(randomMessage);

            this.studentViewServiceMock.Setup(service =>
                service.AddStudentViewAsync(It.IsAny<StudentView>()))
                    .ThrowsAsync(exception);

            // when
            this.renderedStudentRegistrationComponent =
                RenderComponent<StudentRegistrationComponent>();

            this.renderedStudentRegistrationComponent.Instance
                .RegisterButton.Click();

            // then
            this.renderedStudentRegistrationComponent.Instance
                .StatusLabel.Value.Should().Be(expectedErrorMessage);

            this.renderedStudentRegistrationComponent.Instance
                .StatusLabel.Color.Should().Be(Color.Red);

            this.renderedStudentRegistrationComponent.Instance
                .FirstNameTextBox.IsDisabled.Should().BeFalse();

            this.renderedStudentRegistrationComponent.Instance
                .LastNameTextBox.IsDisabled.Should().BeFalse();

            this.renderedStudentRegistrationComponent.Instance
                .DateOfBirthPicker.IsDisabled.Should().BeFalse();

            this.renderedStudentRegistrationComponent.Instance
                .RegisterButton.IsDisabled.Should().BeFalse();

            this.studentViewServiceMock.Verify(service =>
                service.AddStudentViewAsync(It.IsAny<StudentView>()),
                    Times.Once);

            this.studentViewServiceMock.VerifyNoOtherCalls();
        }
    }
}
