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
    }
}
