// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Moq;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.Foundations.GuardianRequestViews
{
    public partial class GuardianRequestViewServiceTests
    {
        [Fact]
        public async Task ShouldNavigateToRoute()
        {
            // given
            string randomRoute = GetRandomRoute();
            string inputRoute = randomRoute;

            // when
            this.guardianRequestViewService.NavigateTo(inputRoute);

            // then
            this.navigationBrokerMock.Verify(broker =>
                broker.NavigateTo(inputRoute),
                    Times.Once);

            this.navigationBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.guardianRequestServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
