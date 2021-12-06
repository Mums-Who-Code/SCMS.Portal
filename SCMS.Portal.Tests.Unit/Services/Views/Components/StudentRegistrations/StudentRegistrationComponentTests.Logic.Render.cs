﻿// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Bunit;
using FluentAssertions;
using Moq;
using SCMS.Portal.Web.Models.Views.Components.Colors;
using SCMS.Portal.Web.Models.Views.Components.Containers;
using SCMS.Portal.Web.Models.Views.StudentViews;
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

            this.renderedStudentRegistrationComponent.Instance.StatusLabel
                .Should().NotBeNull();

            this.renderedStudentRegistrationComponent.Instance.StatusLabel
                .Value.Should().BeNull();

            this.renderedStudentRegistrationComponent.Instance.Exception.Should().BeNull();
            this.studentViewServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldDisplaySubmittingStatusAndDisabledControlsBeforeStudentRegistrationCompletes()
        {
            // given
            StudentView someStudentView = CreateRandomStudentView();

            this.studentViewServiceMock.Setup(service =>
                service.AddStudentViewAsync(It.IsAny<StudentView>()))
                    .ReturnsAsync(
                        value: someStudentView,
                        delay: TimeSpan.FromMilliseconds(500));

            // when
            this.renderedStudentRegistrationComponent =
                RenderComponent<StudentRegistrationComponent>();

            this.renderedStudentRegistrationComponent.Instance.RegisterButton.Click();

            // then
            this.renderedStudentRegistrationComponent.Instance.StatusLabel.Value
                .Should().BeEquivalentTo("Registering... ");

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

    }
}
