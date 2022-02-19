// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Moq;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.Foundations.SchoolViews
{
    public partial class SchoolViewServiceTests
    {
        [Fact]
        public void ShouldNavigateToRoute()
        {
            // given
            string randomRoute = GetRandomRoute();
            string inputRoute = randomRoute;

            // when
            this.schoolViewService.NavigateTo(inputRoute);

            // then
            this.navigationBrokerMock.Verify(broker =>
                broker.NavigateTo(inputRoute),
                    Times.Once);

            this.navigationBrokerMock.VerifyNoOtherCalls();
            this.schoolServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
