// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Bunit;
using FluentAssertions;
using SCMS.Portal.Web.Models.Views.Components.Containers;
using SCMS.Portal.Web.Views.Components.SchoolSelections;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.Components.SchoolSelections
{
    public partial class SchoolSelectionComponentTests : TestContext
    {
        [Fact]
        public void ShouldInitializeComponent()
        {
            // given
            ComponentState expetedComponentState = ComponentState.Loading;

            // when
            var initialSchoolSelectionComponent = new SchoolSelectionComponent();

            // then
            initialSchoolSelectionComponent.State.Should().Be(expetedComponentState);
            initialSchoolSelectionComponent.SchoolsDropdown.Should().BeNull();
        }
    }
}
