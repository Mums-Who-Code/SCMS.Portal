// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews.Exceptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.Foundations.SchoolViews
{
    public partial class SchoolViewServiceTests
    {
        [Fact]
        public void ShouldThrowServiceExceptionOnNavigateIfServiceErrorOccursAndLogIt()
        {
            // given
            String someRoute = GetRandomRoute();

            var serviceException = new Exception();

            var failedSchoolViewServiceException =
                new FailedSchoolViewServiceException(serviceException);

            var expectedSchoolViewServiceException =
                new SchoolViewServiceException(failedSchoolViewServiceException);

            this.navigationBrokerMock.Setup(service =>
                service.NavigateTo(It.IsAny<string>()))
                    .Throws(serviceException);

            // when
            Action navigateToAction = () =>
                this.schoolViewService.NavigateTo(someRoute);

            // then
            Assert.Throws<SchoolViewServiceException>(
                navigateToAction);

            this.navigationBrokerMock.Verify(service =>
                service.NavigateTo(It.IsAny<string>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSchoolViewServiceException))),
                        Times.Once);

            this.navigationBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }    
    }
}
