// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Bunit;
using FluentAssertions;
using SCMS.Portal.Web.Models.Views.Components.Containers;
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
            initialStudentRegistrationComponent.Exception.Should().BeNull();
            initialStudentRegistrationComponent.FirstNameTextBox.Should().BeNull();
            initialStudentRegistrationComponent.LastNameTextBox.Should().BeNull();
            initialStudentRegistrationComponent.StudentView.Should().BeNull();
        }

        [Fact]
        public void ShouldRenderComponent()
        {
            // given
            ComponentState expectedComponentState = ComponentState.Content;

            string expectedFirstNameTextBoxPlaceholder = "First Name";
            string expectedLastNameTextBoxPlaceholder = "Last Name";
            string expectedRegisterButtonLabel = "Register";

            // when
            this.renderedStudentRegistrationComponent =
                RenderComponent<StudentRegistrationComponent>();

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

            this.renderedStudentRegistrationComponent.Instance.RegisterButton
                .Should().NotBeNull();

            this.renderedStudentRegistrationComponent.Instance.RegisterButton
                .IsDisabled.Should().BeFalse();

            this.renderedStudentRegistrationComponent.Instance.RegisterButton
                .Label.Should().Be(expectedRegisterButtonLabel);

            this.renderedStudentRegistrationComponent.Instance.Exception.Should().BeNull();
            this.studentViewServiceMock.VerifyNoOtherCalls();
        }
    }
}
