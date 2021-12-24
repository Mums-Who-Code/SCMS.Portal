// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SCMS.Portal.Web.Models.Foundations.Guardians;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Foundations.Guardians
{
    public partial class GuardianServiceTests
    {
        [Fact]
        public async Task ShouldAddGuardianAsync()
        {
            //given
            Guardian randomGuardian = CreateRandomGuardian();
            Guardian inputGuardian = randomGuardian;
            Guardian retrievedGuardian = inputGuardian;
            Guardian expectedGuardian = retrievedGuardian.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.PostGuardianAsync(inputGuardian))
                    .ReturnsAsync(retrievedGuardian);

            //when
            Guardian actualGuardian = await this.guardianService.AddGuardianAsync(inputGuardian);

            //then
            actualGuardian.Should().BeEquivalentTo(expectedGuardian);

            this.apiBrokerMock.Verify(broker =>
                broker.PostGuardianAsync(inputGuardian),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

    }
}
