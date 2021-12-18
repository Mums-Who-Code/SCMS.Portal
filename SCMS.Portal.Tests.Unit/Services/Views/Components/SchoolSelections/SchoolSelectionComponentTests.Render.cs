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

            this.schoolViewServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldRenderComponent()
        {
            // given
            ComponentState expectedComponentState = ComponentState.Content;

            string expectedSchoolDropdownPlaceholder = "School";

            // when
            this.renderedSchoolSelectionComponent =
                RenderComponent<SchoolSelectionComponent>();

            // then
            this.renderedSchoolSelectionComponent.Instance
                .SchoolViewService.Should().NotBeNull();

            this.renderedSchoolSelectionComponent.Instance
                .State.Should().Be(expectedComponentState);

            this.renderedSchoolSelectionComponent.Instance
                .SchoolViews.Should().NotBeNull();

            this.renderedSchoolSelectionComponent.Instance
                .SchoolsDropdown.Should().NotBeNull();

            this.renderedSchoolSelectionComponent.Instance
                .SchoolsDropdown.Placeholder.Should()
                    .Be(expectedSchoolDropdownPlaceholder);

            this.renderedSchoolSelectionComponent.Instance
                .SelectedSchool.Should().BeNull();

            this.schoolViewServiceMock.VerifyNoOtherCalls();
        }
    }
}
