// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using Bunit;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SCMS.Portal.Web.Models.Views.Components.Containers;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews;
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

            List<SchoolView> randomSchoolViews = CreateRandomSchoolViews();
            List<SchoolView> returningSchoolViews = randomSchoolViews;
            List<SchoolView> expectedSchoolViews = returningSchoolViews.DeepClone();

            this.schoolViewServiceMock.Setup(service =>
                service.RetrieveAllSchoolViewsAsync())
                    .ReturnsAsync(returningSchoolViews);

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
                .SchoolViews.Should().BeEquivalentTo(expectedSchoolViews);

            this.renderedSchoolSelectionComponent.Instance
                .SchoolsDropdown.Should().NotBeNull();

            this.renderedSchoolSelectionComponent.Instance
                .SchoolsDropdown.Placeholder.Should()
                    .Be(expectedSchoolDropdownPlaceholder);

            this.renderedSchoolSelectionComponent.Instance
                .SelectedSchool.Should().BeNull();

            this.schoolViewServiceMock.Verify(service =>
                service.RetrieveAllSchoolViewsAsync(),
                    Times.Once());

            this.schoolViewServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldSetSelectedSchool()
        {
            // given
            SchoolView randomSchoolView = CreateRandomSchoolView();
            SchoolView selectedSchool = randomSchoolView;

            List<SchoolView> randomSchoolViews =
                CreateRandomSchoolViewsWith(selectedSchool);

            List<SchoolView> returningSchoolViews =
                randomSchoolViews;

            this.schoolViewServiceMock.Setup(service =>
                service.RetrieveAllSchoolViewsAsync())
                    .ReturnsAsync(returningSchoolViews);

            // when
            this.renderedSchoolSelectionComponent =
                RenderComponent<SchoolSelectionComponent>();

            this.renderedSchoolSelectionComponent.Instance
                .OnSelectedItemChanged(selectedSchool);

            // then
            this.renderedSchoolSelectionComponent.Instance
                .SelectedSchool.Should().NotBeNull();

            this.renderedSchoolSelectionComponent.Instance
                .SelectedSchool.Should().Be(selectedSchool);

            this.schoolViewServiceMock.Verify(service =>
                service.RetrieveAllSchoolViewsAsync(),
                    Times.Once());

            this.schoolViewServiceMock.VerifyNoOtherCalls();
        }
    }
}
