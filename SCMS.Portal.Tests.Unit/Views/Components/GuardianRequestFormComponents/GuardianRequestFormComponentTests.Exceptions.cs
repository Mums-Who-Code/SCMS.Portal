// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Bunit;
using FluentAssertions;
using Moq;
using SCMS.Portal.Web.Models.Views.Components.Colors;
using SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews;
using SCMS.Portal.Web.Views.Components.GuardianRequestForms;
using Xeptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Views.Components.GuardianRequestForms
{
    public partial class GuardianRequestFormComponentTests
    {
        [Theory]
        [MemberData(nameof(GuardianRequestViewValidationExceptions))]
        public void ShouldRenderInnerExceptionMessageIfValidationErrorOccurs(
            Xeption guardianViewValidationException)
        {
            // given
            Guid randomStudentId = Guid.NewGuid();
            Guid inputStudentId = randomStudentId;

            string expectedErrorMessage =
                guardianViewValidationException.InnerException.Message;

            ComponentParameter componentParameter = ComponentParameter.CreateParameter(
                name: nameof(GuardianRequestView.StudentId),
                value: inputStudentId);

            this.guardianRequestViewServiceMock.Setup(service =>
                service.AddGuardianRequestViewAsync(It.IsAny<GuardianRequestView>()))
                    .ThrowsAsync(guardianViewValidationException);

            // when
            this.renderedGuardianRequestFormComponent =
                RenderComponent<GuardianRequestFormComponent>();

            this.renderedGuardianRequestFormComponent.Instance
                .RegisterButton.Click();

            // then
            this.renderedGuardianRequestFormComponent.Instance
                .StatusLabel.Value.Should().Be(expectedErrorMessage);

            this.renderedGuardianRequestFormComponent.Instance
                .StatusLabel.Color.Should().Be(Color.Red);

            this.renderedGuardianRequestFormComponent.Instance.TitleDropdown.IsDisabled
                .Should().BeFalse();

            this.renderedGuardianRequestFormComponent.Instance.FirstNameTextBox.IsDisabled
               .Should().BeFalse();

            this.renderedGuardianRequestFormComponent.Instance.LastNameTextBox.IsDisabled
               .Should().BeFalse();

            this.renderedGuardianRequestFormComponent.Instance.EmailTextBox.IsDisabled
               .Should().BeFalse();

            this.renderedGuardianRequestFormComponent.Instance.CountryCodeTextBox.IsDisabled
                .Should().BeFalse();

            this.renderedGuardianRequestFormComponent.Instance.ContactNumberTextBox.IsDisabled
               .Should().BeFalse();

            this.renderedGuardianRequestFormComponent.Instance.OccupationTextBox.IsDisabled
               .Should().BeFalse();

            this.renderedGuardianRequestFormComponent.Instance.ContactLevelDropdown.IsDisabled
               .Should().BeFalse();

            this.renderedGuardianRequestFormComponent.Instance.RelationshipDropdown.IsDisabled
               .Should().BeFalse();

            this.renderedGuardianRequestFormComponent.Instance.RegisterButton.IsDisabled
               .Should().BeFalse();

            this.guardianRequestViewServiceMock.Verify(service =>
                service.AddGuardianRequestViewAsync(It.IsAny<GuardianRequestView>()),
                    Times.Once);

            this.guardianRequestViewServiceMock.VerifyNoOtherCalls();
        }
    }
}
