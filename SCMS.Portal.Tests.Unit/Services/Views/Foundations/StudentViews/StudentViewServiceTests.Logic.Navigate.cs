// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Moq;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.Foundations.StudentViews
{
    public partial class StudentViewServiceTests
    {
        [Fact]
        public async Task ShouldNavigateToRoute()
        {
            // given
            string randomRoute = GetRandomRoute();
            string inputRoute = randomRoute;

            // when
            this.studentViewService.NavigateTo(inputRoute);

            // then
            this.navigationBrokerMock.Verify(broker =>
                broker.NavigateTo(inputRoute),
                    Times.Once);

            this.navigationBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.studentServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
