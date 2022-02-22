// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Bunit;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SCMS.Portal.Web.Models.Views.Components.Colors;
using SCMS.Portal.Web.Models.Views.Components.Containers;
using SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews;
using SCMS.Portal.Web.Views.Components.GuardianRequestForms;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Views.Components.GuardianRequestForms
{
    public partial class GuardianRequestFormComponentTests
    {
        [Fact]
        public void ShouldInitializeComponent()
        {
            // given
            ComponentState expectedComponentState = ComponentState.Loading;

            // when
            var initialGuardianRequestFormComponent = new GuardianRequestFormComponent();

            // then
            initialGuardianRequestFormComponent.State.Should().Be(expectedComponentState);
            initialGuardianRequestFormComponent.GuardianRequestView.Should().BeNull();
            initialGuardianRequestFormComponent.TitleDropdown.Should().BeNull();
            initialGuardianRequestFormComponent.FirstNameTextBox.Should().BeNull();
            initialGuardianRequestFormComponent.LastNameTextBox.Should().BeNull();
            initialGuardianRequestFormComponent.EmailTextBox.Should().BeNull();
            initialGuardianRequestFormComponent.CountryCodeTextBox.Should().BeNull();
            initialGuardianRequestFormComponent.ContactNumberTextBox.Should().BeNull();
            initialGuardianRequestFormComponent.OccupationTextBox.Should().BeNull();
            initialGuardianRequestFormComponent.ContactLevelDropdown.Should().BeNull();
            initialGuardianRequestFormComponent.RelationshipDropdown.Should().BeNull();
            initialGuardianRequestFormComponent.StudentId.Should().BeEmpty();
        }

        [Fact]
        public void ShoudRenderComponent()
        {
            // given
            ComponentState expectedComponentState = ComponentState.Content;
            string expectedTitleLabel = "Title";
            string expectedFirstNameTextBoxPlaceholder = "First Name";
            string expectedLastNameTextBoxPlaceholder = "Last Name";
            string expectedEmailTextBoxPlaceholder = "Email";
            string expectedCountryCodeTextBoxPlaceholder = "Country Code";
            string expectedContactNumberTextBoxPlaceholder = "Contact Number";
            string expectedOccupationTextBoxPlaceholder = "Occupation";
            string expectedContactLevelLabel = "Contact Level";
            string expectedRelationshipLabel = "Relationship";
            string expectedRegisterButtonLabel = "Register";
            Guid randomStudentId = Guid.NewGuid();
            Guid inputStudentId = randomStudentId;
            Guid expectedStudentId = inputStudentId;

            ComponentParameter componentParameter = ComponentParameter.CreateParameter(
                name: nameof(GuardianRequestFormComponent.StudentId),
                value: inputStudentId);

            // when
            this.renderedGuardianRequestFormComponent =
                RenderComponent<GuardianRequestFormComponent>(
                    componentParameter);

            // then
            this.renderedGuardianRequestFormComponent.Instance.GuardianRequestView
                .Should().NotBeNull();

            this.renderedGuardianRequestFormComponent.Instance.State
                .Should().Be(expectedComponentState);

            this.renderedGuardianRequestFormComponent.Instance.StudentId
                .Should().Be(expectedStudentId);

            this.renderedGuardianRequestFormComponent.Instance.TitleLabel
                .Value.Should().Be(expectedTitleLabel);

            this.renderedGuardianRequestFormComponent.Instance.TitleDropdown
                .Should().NotBeNull();

            this.renderedGuardianRequestFormComponent.Instance.TitleDropdown.Value.GetType()
                .Should().Be(typeof(GuardianRequestViewTitle));

            this.renderedGuardianRequestFormComponent.Instance.TitleDropdown
                .IsDisabled.Should().BeFalse();

            this.renderedGuardianRequestFormComponent.Instance.FirstNameTextBox
                .Should().NotBeNull();

            this.renderedGuardianRequestFormComponent.Instance.FirstNameTextBox
                .Placeholder.Should().Be(expectedFirstNameTextBoxPlaceholder);

            this.renderedGuardianRequestFormComponent.Instance.FirstNameTextBox
                .IsDisabled.Should().BeFalse();

            this.renderedGuardianRequestFormComponent.Instance.LastNameTextBox
                .Should().NotBeNull();

            this.renderedGuardianRequestFormComponent.Instance.LastNameTextBox
                .Placeholder.Should().Be(expectedLastNameTextBoxPlaceholder);

            this.renderedGuardianRequestFormComponent.Instance.LastNameTextBox
                .IsDisabled.Should().BeFalse();

            this.renderedGuardianRequestFormComponent.Instance.EmailTextBox
                .Should().NotBeNull();

            this.renderedGuardianRequestFormComponent.Instance.EmailTextBox
                .Placeholder.Should().Be(expectedEmailTextBoxPlaceholder);

            this.renderedGuardianRequestFormComponent.Instance.EmailTextBox
                .IsDisabled.Should().BeFalse();

            this.renderedGuardianRequestFormComponent.Instance.CountryCodeTextBox
                .Should().NotBeNull();

            this.renderedGuardianRequestFormComponent.Instance.CountryCodeTextBox
                .Placeholder.Should().Be(expectedCountryCodeTextBoxPlaceholder);

            this.renderedGuardianRequestFormComponent.Instance.CountryCodeTextBox
                .IsDisabled.Should().BeFalse();

            this.renderedGuardianRequestFormComponent.Instance.ContactNumberTextBox
                .Should().NotBeNull();

            this.renderedGuardianRequestFormComponent.Instance.ContactNumberTextBox
                .Placeholder.Should().Be(expectedContactNumberTextBoxPlaceholder);

            this.renderedGuardianRequestFormComponent.Instance.ContactNumberTextBox
                .IsDisabled.Should().BeFalse();

            this.renderedGuardianRequestFormComponent.Instance.OccupationTextBox
                .Should().NotBeNull();

            this.renderedGuardianRequestFormComponent.Instance.OccupationTextBox
                .Placeholder.Should().Be(expectedOccupationTextBoxPlaceholder);

            this.renderedGuardianRequestFormComponent.Instance.OccupationTextBox
                .IsDisabled.Should().BeFalse();

            this.renderedGuardianRequestFormComponent.Instance.ContactLevelLabel
                .Value.Should().Be(expectedContactLevelLabel);

            this.renderedGuardianRequestFormComponent.Instance.ContactLevelDropdown
                .Should().NotBeNull();

            this.renderedGuardianRequestFormComponent.Instance.ContactLevelDropdown.Value.GetType()
                .Should().Be(typeof(GuardianRequestViewContactLevel));

            this.renderedGuardianRequestFormComponent.Instance.ContactLevelDropdown
                .IsDisabled.Should().BeFalse();

            this.renderedGuardianRequestFormComponent.Instance.RelationshipLabel
                .Value.Should().Be(expectedRelationshipLabel);

            this.renderedGuardianRequestFormComponent.Instance.RelationshipDropdown
                .Should().NotBeNull();

            this.renderedGuardianRequestFormComponent.Instance.RelationshipDropdown.Value.GetType()
                .Should().Be(typeof(GuardianRequestViewRelationship));

            this.renderedGuardianRequestFormComponent.Instance.RelationshipDropdown
                .IsDisabled.Should().BeFalse();

            this.renderedGuardianRequestFormComponent.Instance.RegisterButton
                .Should().NotBeNull();

            this.renderedGuardianRequestFormComponent.Instance.RegisterButton
                .IsDisabled.Should().BeFalse();

            this.renderedGuardianRequestFormComponent.Instance.RegisterButton
                .Label.Should().Be(expectedRegisterButtonLabel);

            this.renderedGuardianRequestFormComponent.Instance.StatusLabel
                .Should().NotBeNull();

            this.renderedGuardianRequestFormComponent.Instance.StatusLabel
                .Value.Should().BeNull();

            this.guardianRequestViewServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldDisplaySubmittingStatusAndDisableControlsBeforeGuardianRequestRegistrationCompletes()
        {
            // given
            GuardianRequestView someGuardianRequestView = CreateRandomGuardianRequestView();
            Guid randomStudentId = Guid.NewGuid();
            Guid inputStudentId = randomStudentId;

            ComponentParameter componentParameter = ComponentParameter.CreateParameter(
                name: nameof(GuardianRequestView.StudentId),
                value: inputStudentId);

            this.guardianRequestViewServiceMock.Setup(service =>
                service.AddGuardianRequestViewAsync(It.IsAny<GuardianRequestView>()))
                    .ReturnsAsync(
                        value: someGuardianRequestView,
                        delay: TimeSpan.FromMilliseconds(500));

            // when
            this.renderedGuardianRequestFormComponent =
                RenderComponent<GuardianRequestFormComponent>(
                    componentParameter);

            this.renderedGuardianRequestFormComponent.Instance.RegisterButton.Click();

            // then
            this.renderedGuardianRequestFormComponent.Instance.StatusLabel.Value
                .Should().BeEquivalentTo("Registering...");

            this.renderedGuardianRequestFormComponent.Instance.StatusLabel.Color
                .Should().Be(Color.Black);

            this.renderedGuardianRequestFormComponent.Instance.TitleDropdown.IsDisabled
                .Should().BeTrue();

            this.renderedGuardianRequestFormComponent.Instance.FirstNameTextBox.IsDisabled
               .Should().BeTrue();

            this.renderedGuardianRequestFormComponent.Instance.LastNameTextBox.IsDisabled
               .Should().BeTrue();

            this.renderedGuardianRequestFormComponent.Instance.EmailTextBox.IsDisabled
               .Should().BeTrue();

            this.renderedGuardianRequestFormComponent.Instance.CountryCodeTextBox.IsDisabled
                .Should().BeTrue();

            this.renderedGuardianRequestFormComponent.Instance.ContactNumberTextBox.IsDisabled
               .Should().BeTrue();

            this.renderedGuardianRequestFormComponent.Instance.OccupationTextBox.IsDisabled
               .Should().BeTrue();

            this.renderedGuardianRequestFormComponent.Instance.ContactLevelDropdown.IsDisabled
               .Should().BeTrue();

            this.renderedGuardianRequestFormComponent.Instance.RelationshipDropdown.IsDisabled
               .Should().BeTrue();

            this.renderedGuardianRequestFormComponent.Instance.RegisterButton.IsDisabled
               .Should().BeTrue();

            this.guardianRequestViewServiceMock.Verify(service =>
                service.AddGuardianRequestViewAsync(It.IsAny<GuardianRequestView>()),
                    Times.Once);

            this.guardianRequestViewServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldRegisterGuardianRequest()
        {
            // given
            GuardianRequestView randomGuardianRequestView = CreateRandomGuardianRequestView();
            GuardianRequestView inputGuardianRequestView = randomGuardianRequestView;
            GuardianRequestView expectedGuardianRequestView = inputGuardianRequestView.DeepClone();
            Guid randomStudentId = Guid.NewGuid();
            Guid inputStudentId = randomStudentId;
            string expectedStatusLabel = "Registration completed.";
            Color expectedStatusLabelColor = Color.Green;

            ComponentParameter componentParameter = ComponentParameter.CreateParameter(
                name: nameof(GuardianRequestView.StudentId),
                value: inputStudentId);

            // when
            this.renderedGuardianRequestFormComponent =
                RenderComponent<GuardianRequestFormComponent>(
                    componentParameter);

            this.renderedGuardianRequestFormComponent.Instance
                .TitleDropdown.SetValue(inputGuardianRequestView.Title);

            this.renderedGuardianRequestFormComponent.Instance
                .FirstNameTextBox.SetValue(inputGuardianRequestView.FirstName);

            this.renderedGuardianRequestFormComponent.Instance
                .LastNameTextBox.SetValue(inputGuardianRequestView.LastName);

            this.renderedGuardianRequestFormComponent.Instance
                .EmailTextBox.SetValue(inputGuardianRequestView.Email);

            this.renderedGuardianRequestFormComponent.Instance
                .CountryCodeTextBox.SetValue(inputGuardianRequestView.CountryCode);

            this.renderedGuardianRequestFormComponent.Instance
                .ContactNumberTextBox.SetValue(inputGuardianRequestView.ContactNumber);

            this.renderedGuardianRequestFormComponent.Instance
                .OccupationTextBox.SetValue(inputGuardianRequestView.Occupation);

            this.renderedGuardianRequestFormComponent.Instance
                .ContactLevelDropdown.SetValue(inputGuardianRequestView.ContactLevel);

            this.renderedGuardianRequestFormComponent.Instance
                .RelationshipDropdown.SetValue(inputGuardianRequestView.Relationship);

            this.renderedGuardianRequestFormComponent.Instance
                .RegisterButton.Click();

            // then
            this.renderedGuardianRequestFormComponent.Instance
                .TitleDropdown.Value.Should().Be(expectedGuardianRequestView.Title);

            this.renderedGuardianRequestFormComponent.Instance
                .FirstNameTextBox.Value.Should().Be(expectedGuardianRequestView.FirstName);

            this.renderedGuardianRequestFormComponent.Instance
                .LastNameTextBox.Value.Should().Be(expectedGuardianRequestView.LastName);

            this.renderedGuardianRequestFormComponent.Instance
                .EmailTextBox.Value.Should().Be(expectedGuardianRequestView.Email);

            this.renderedGuardianRequestFormComponent.Instance
                .CountryCodeTextBox.Value.Should().Be(expectedGuardianRequestView.CountryCode);

            this.renderedGuardianRequestFormComponent.Instance
                .ContactNumberTextBox.Value.Should().Be(expectedGuardianRequestView.ContactNumber);

            this.renderedGuardianRequestFormComponent.Instance
                .OccupationTextBox.Value.Should().Be(expectedGuardianRequestView.Occupation);

            this.renderedGuardianRequestFormComponent.Instance
                .ContactLevelDropdown.Value.Should().Be(expectedGuardianRequestView.ContactLevel);

            this.renderedGuardianRequestFormComponent.Instance
                .RelationshipDropdown.Value.Should().Be(expectedGuardianRequestView.Relationship);

            this.renderedGuardianRequestFormComponent.Instance.StatusLabel
                .Value.Should().Be(expectedStatusLabel);

            this.renderedGuardianRequestFormComponent.Instance.StatusLabel
                .Color.Should().Be(expectedStatusLabelColor);

            this.guardianRequestViewServiceMock.Verify(service =>
                service.AddGuardianRequestViewAsync(
                    this.renderedGuardianRequestFormComponent.Instance.GuardianRequestView),
                        Times.Once);

            this.guardianRequestViewServiceMock.VerifyNoOtherCalls();
        }
    }
}
