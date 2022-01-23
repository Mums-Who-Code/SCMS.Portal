// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SCMS.Portal.Web.Models.Foundations.GuardianRequests;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Foundations.GuardianRequests
{
    public partial class GuardianRequestServiceTests
    {
        [Fact]
        public async Task ShouldAddGuardianRequestAsync()
        {
            //given
            GuardianRequest randomGuardianRequest = CreateRandomGuardianRequest();
            GuardianRequest inputGuardianRequest = randomGuardianRequest;
            GuardianRequest retrievedGuardianRequest = inputGuardianRequest;
            GuardianRequest expectedGuardianRequest = retrievedGuardianRequest.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.PostGuardianRequestAsync(inputGuardianRequest))
                    .ReturnsAsync(retrievedGuardianRequest);

            //when
            GuardianRequest actualGuardianRequest = 
                await this.guardianRequestService.AddGuardianRequestAsync(
                    inputGuardianRequest);

            //then
            actualGuardianRequest.Should().BeEquivalentTo(expectedGuardianRequest);

            this.apiBrokerMock.Verify(broker =>
                broker.PostGuardianRequestAsync(inputGuardianRequest),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
