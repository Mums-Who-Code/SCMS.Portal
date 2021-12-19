// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Bunit;
using FluentAssertions;
using Moq;
using SCMS.Portal.Web.Models.Views.Components.Containers;
using SCMS.Portal.Web.Models.Views.Components.SchoolSelections.Exceptions;
using SCMS.Portal.Web.Views.Components.SchoolSelections;
using Xeptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.Components.SchoolSelections
{
    public partial class SchoolSelectionComponentTests : TestContext
    {
        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public void ShouldThrowDependencyExceptionOnRenderIfDependencyErrorOccurs(
            Xeption dependencyException)
        {
            // given
            var expectedComponentState = ComponentState.Error;

            var expectedSchoolSelectionCompoenentException =
                new SchoolSelectionComponentException(
                    dependencyException);

            this.schoolViewServiceMock.Setup(service =>
                service.RetrieveAllSchoolViewsAsync())
                    .ThrowsAsync(dependencyException);

            // when .
            this.renderedSchoolSelectionComponent =
                RenderComponent<SchoolSelectionComponent>();

            // then
            this.renderedSchoolSelectionComponent.Instance.
                Exception.Should().BeEquivalentTo(
                    expectedSchoolSelectionCompoenentException);

            this.renderedSchoolSelectionComponent.Instance
                .State.Should().Be(expectedComponentState);

            this.renderedSchoolSelectionComponent.Instance
                .SchoolsDropdown.Should().BeNull();

            this.schoolViewServiceMock.Verify(service =>
                service.RetrieveAllSchoolViewsAsync(),
                    Times.Once());

            this.schoolViewServiceMock.VerifyNoOtherCalls();
        }
    }
}
