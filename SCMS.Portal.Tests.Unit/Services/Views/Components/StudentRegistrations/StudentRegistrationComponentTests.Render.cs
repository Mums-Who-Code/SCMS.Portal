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
            initialStudentRegistrationComponent.StudentView.Should().BeNull();
            initialStudentRegistrationComponent.FirstNameTextBox.Should().BeNull();
            initialStudentRegistrationComponent.LastNameTextBox.Should().BeNull();
            initialStudentRegistrationComponent.DateOfBirthPicker.Should().BeNull();
            initialStudentRegistrationComponent.GenderDropdown.Should().BeNull();
            initialStudentRegistrationComponent.FideIdTextBox.Should().BeNull();
            initialStudentRegistrationComponent.NotesTextBox.Should().BeNull();
            initialStudentRegistrationComponent.NextButton.Should().BeNull();
            initialStudentRegistrationComponent.StatusLabel.Should().BeNull();
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
            string expectedFideIdTextBoxPlaceholder = "Fide Id";
            string expectedNotesTextBoxPlaceholder = "Notes";
            string expectedNextButtonLabel = "Next";

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

            this.renderedStudentRegistrationComponent.Instance.FideIdTextBox
                .Should().NotBeNull();

            this.renderedStudentRegistrationComponent.Instance.FideIdTextBox
                .IsDisabled.Should().BeFalse();

            this.renderedStudentRegistrationComponent.Instance.FideIdTextBox
                .Placeholder.Should().Be(expectedFideIdTextBoxPlaceholder);

            this.renderedStudentRegistrationComponent.Instance.NotesTextBox
                .Should().NotBeNull();

            this.renderedStudentRegistrationComponent.Instance.NotesTextBox
                .IsDisabled.Should().BeFalse();

            this.renderedStudentRegistrationComponent.Instance.NotesTextBox
                .Placeholder.Should().Be(expectedNotesTextBoxPlaceholder);

            this.renderedStudentRegistrationComponent.Instance.NextButton
                .Should().NotBeNull();

            this.renderedStudentRegistrationComponent.Instance.NextButton
                .IsDisabled.Should().BeFalse();

            this.renderedStudentRegistrationComponent.Instance.NextButton
                .Label.Should().Be(expectedNextButtonLabel);

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

            this.renderedStudentRegistrationComponent.Instance.NextButton.Click();

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

            this.renderedStudentRegistrationComponent.Instance.GenderDropdown.IsDisabled
                .Should().BeTrue();

            this.renderedStudentRegistrationComponent.Instance.FideIdTextBox.IsDisabled
               .Should().BeTrue();

            this.renderedStudentRegistrationComponent.Instance.NotesTextBox.IsDisabled
               .Should().BeTrue();

            this.renderedStudentRegistrationComponent.Instance.NextButton.IsDisabled
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
            List<SchoolView> someSchoolViews = CreateRandomSchoolViews();
            SchoolView selectedSchool = someSchoolViews.FirstOrDefault();
            SchoolView expectedSchoolView = selectedSchool.DeepClone();

            this.schoolViewServiceMock.Setup(service =>
                service.RetrieveAllSchoolViewsAsync())
                    .ReturnsAsync(someSchoolViews);

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
                .GenderDropdown.SetValue(inputStudentView.Gender);

            this.renderedStudentRegistrationComponent.Instance
                .FideIdTextBox.SetValue(inputStudentView.FideId);

            this.renderedStudentRegistrationComponent.Instance
                .NotesTextBox.SetValue(inputStudentView.Notes);

            this.renderedStudentRegistrationComponent.Instance
                .SchoolSelectionComponent.SelectedSchool =
                   selectedSchool;

            this.renderedStudentRegistrationComponent.Instance
                .NextButton.Click();

            // then
            this.renderedStudentRegistrationComponent.Instance.FirstNameTextBox
                .Value.Should().Be(expectedStudentView.FirstName);

            this.renderedStudentRegistrationComponent.Instance.LastNameTextBox
                .Value.Should().Be(expectedStudentView.LastName);

            this.renderedStudentRegistrationComponent.Instance.DateOfBirthPicker
                .Value.Should().Be(expectedStudentView.DateOfBirth);

            this.renderedStudentRegistrationComponent.Instance.GenderDropdown
                .Value.Should().Be(expectedStudentView.Gender);

            this.renderedStudentRegistrationComponent.Instance.FideIdTextBox
                .Value.Should().Be(expectedStudentView.FideId);

            this.renderedStudentRegistrationComponent.Instance.NotesTextBox
                .Value.Should().Be(expectedStudentView.Notes);

            this.renderedStudentRegistrationComponent.Instance.SchoolSelectionComponent
                .SelectedSchool.Id.Should().Be(expectedSchoolView.Id);

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
