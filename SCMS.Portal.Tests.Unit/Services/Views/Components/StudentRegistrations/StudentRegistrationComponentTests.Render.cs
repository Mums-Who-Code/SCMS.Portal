// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Bunit;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SCMS.Portal.Web.Models.Views.Components.Colors;
using SCMS.Portal.Web.Models.Views.Components.Containers;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews;
using SCMS.Portal.Web.Models.Views.Foundations.StudentViews;
using SCMS.Portal.Web.Views.Components.StudentRegistrations;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.Components.StudentRegistrations
{
    public partial class StudentRegistrationComponentTests : TestContext
    {
        [Fact]
        public void ShouldInitializeComponent()
        {
            // given
            ComponentState expectedComponentState = ComponentState.Loading;

            // when
            var initialStudentRegistrationComponent = new StudentRegistrationComponent();

            // then
            initialStudentRegistrationComponent.State.Should().Be(expectedComponentState);
            initialStudentRegistrationComponent.FirstNameTextBox.Should().BeNull();
            initialStudentRegistrationComponent.LastNameTextBox.Should().BeNull();
            initialStudentRegistrationComponent.StudentView.Should().BeNull();
            initialStudentRegistrationComponent.DateOfBirthPicker.Should().BeNull();
            initialStudentRegistrationComponent.SchoolSelectionComponent.Should().BeNull();
        }

        [Fact]
        public void ShouldRenderComponent()
        {
            // given
            ComponentState expectedComponentState = ComponentState.Content;
            List<SchoolView> someSchoolViews = CreateRandomSchoolViews();
            string expectedFirstNameTextBoxPlaceholder = "First Name";
            string expectedLastNameTextBoxPlaceholder = "Last Name";
            string expectedDateOfBirthPickerPlaceholder = "Date of Birth";
            string expectedRegisterButtonLabel = "Register";

            this.schoolViewServiceMock.Setup(service =>
                service.RetrieveAllSchoolViewsAsync())
                    .ReturnsAsync(someSchoolViews);

            // when
            this.renderedStudentRegistrationComponent =
                RenderComponent<StudentRegistrationComponent>();

            this.renderedStudentRegistrationComponent.Instance
                .SchoolSelectionComponent.SelectedSchool =
                    someSchoolViews.FirstOrDefault();

            // then
            this.renderedStudentRegistrationComponent.Instance.StudentView
                .Should().NotBeNull();

            this.renderedStudentRegistrationComponent.Instance.State
                .Should().Be(expectedComponentState);

            this.renderedStudentRegistrationComponent.Instance.FirstNameTextBox
                .Should().NotBeNull();

            this.renderedStudentRegistrationComponent.Instance.FirstNameTextBox
                .IsDisabled.Should().BeFalse();

            this.renderedStudentRegistrationComponent.Instance.FirstNameTextBox
                .Placeholder.Should().Be(expectedFirstNameTextBoxPlaceholder);

            this.renderedStudentRegistrationComponent.Instance.LastNameTextBox
                .Should().NotBeNull();

            this.renderedStudentRegistrationComponent.Instance.LastNameTextBox
                .IsDisabled.Should().BeFalse();

            this.renderedStudentRegistrationComponent.Instance.LastNameTextBox
                .Placeholder.Should().Be(expectedLastNameTextBoxPlaceholder);

            this.renderedStudentRegistrationComponent.Instance.DateOfBirthPicker
                .Should().NotBeNull();

            this.renderedStudentRegistrationComponent.Instance.DateOfBirthPicker
                .IsDisabled.Should().BeFalse();

            this.renderedStudentRegistrationComponent.Instance.DateOfBirthPicker
                .Placeholder.Should().Be(expectedDateOfBirthPickerPlaceholder);

            this.renderedStudentRegistrationComponent.Instance.GenderDropdown
                .Should().NotBeNull();

            this.renderedStudentRegistrationComponent.Instance.GenderDropdown
                .IsDisabled.Should().BeFalse();

            this.renderedStudentRegistrationComponent.Instance.RegisterButton
                .Should().NotBeNull();

            this.renderedStudentRegistrationComponent.Instance.RegisterButton
                .IsDisabled.Should().BeFalse();

            this.renderedStudentRegistrationComponent.Instance.RegisterButton
                .Label.Should().Be(expectedRegisterButtonLabel);

            this.renderedStudentRegistrationComponent.Instance.StatusLabel
                .Should().NotBeNull();

            this.renderedStudentRegistrationComponent.Instance.StatusLabel
                .Value.Should().BeNull();

            this.studentViewServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldDisplaySubmittingStatusAndDisabledControlsBeforeStudentRegistrationCompletes()
        {
            // given
            StudentView someStudentView = CreateRandomStudentView();
            List<SchoolView> someSchoolViews = CreateRandomSchoolViews();

            this.schoolViewServiceMock.Setup(service =>
                service.RetrieveAllSchoolViewsAsync())
                    .ReturnsAsync(someSchoolViews);

            this.studentViewServiceMock.Setup(service =>
                service.AddStudentViewAsync(It.IsAny<StudentView>()))
                    .ReturnsAsync(
                        value: someStudentView,
                        delay: TimeSpan.FromMilliseconds(500));

            // when
            this.renderedStudentRegistrationComponent =
                RenderComponent<StudentRegistrationComponent>();

            this.renderedStudentRegistrationComponent.Instance
                .SchoolSelectionComponent.SelectedSchool =
                    someSchoolViews.FirstOrDefault();

            this.renderedStudentRegistrationComponent.Instance.RegisterButton.Click();

            // then
            this.renderedStudentRegistrationComponent.Instance.StatusLabel.Value
                .Should().BeEquivalentTo("Registering...");

            this.renderedStudentRegistrationComponent.Instance.StatusLabel.Color
                .Should().Be(Color.Black);

            this.renderedStudentRegistrationComponent.Instance.FirstNameTextBox.IsDisabled
               .Should().BeTrue();

            this.renderedStudentRegistrationComponent.Instance.LastNameTextBox.IsDisabled
               .Should().BeTrue();

            this.renderedStudentRegistrationComponent.Instance.DateOfBirthPicker.IsDisabled
               .Should().BeTrue();

            this.renderedStudentRegistrationComponent.Instance.RegisterButton.IsDisabled
               .Should().BeTrue();

            this.studentViewServiceMock.Verify(service =>
                service.AddStudentViewAsync(It.IsAny<StudentView>()),
                    Times.Once);

            this.studentViewServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldRegisterStudent()
        {
            // given
            StudentView randomStudentView = CreateRandomStudentView();
            StudentView inputStudentView = randomStudentView;
            StudentView expectedStudentView = inputStudentView.DeepClone();
            string expectedStatusLabel = "Registration completed.";
            Color expectedStatusLabelColor = Color.Green;

            // when
            this.renderedStudentRegistrationComponent =
                RenderComponent<StudentRegistrationComponent>();

            this.renderedStudentRegistrationComponent.Instance
                .FirstNameTextBox.SetValue(inputStudentView.FirstName);

            this.renderedStudentRegistrationComponent.Instance
                .LastNameTextBox.SetValue(inputStudentView.LastName);

            this.renderedStudentRegistrationComponent.Instance
                .DateOfBirthPicker.SetValue(inputStudentView.DateOfBirth);

            this.renderedStudentRegistrationComponent.Instance
                .RegisterButton.Click();

            // then
            this.renderedStudentRegistrationComponent.Instance.FirstNameTextBox
                .Value.Should().Be(expectedStudentView.FirstName);

            this.renderedStudentRegistrationComponent.Instance.LastNameTextBox
                .Value.Should().Be(expectedStudentView.LastName);

            this.renderedStudentRegistrationComponent.Instance.DateOfBirthPicker
                .Value.Should().Be(expectedStudentView.DateOfBirth);

            this.renderedStudentRegistrationComponent.Instance.StatusLabel
                .Value.Should().Be(expectedStatusLabel);

            this.renderedStudentRegistrationComponent.Instance.StatusLabel
                .Color.Should().Be(expectedStatusLabelColor);

            this.studentViewServiceMock.Verify(service =>
                service.AddStudentViewAsync(
                    this.renderedStudentRegistrationComponent.Instance.StudentView),
                        Times.Once);

            this.studentViewServiceMock.VerifyNoOtherCalls();
        }
    }
}
