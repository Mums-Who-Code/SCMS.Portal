// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Moq;
using SCMS.Portal.Web.Brokers.Loggings;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews;
using SCMS.Portal.Web.Services.Views.Foundations.SchoolViews;
using SCMS.Portal.Web.Services.Views.Processings.SchoolViews;
using Tynamix.ObjectFiller;

namespace SCMS.Portal.Tests.Unit.Services.Views.Processings.SchoolViews
{
    public partial class SchoolViewProcessingServiceTests
    {
        private readonly ISchoolViewProcessingService schoolViewProcessingService;
        private readonly Mock<ISchoolViewService> schoolViewServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;

        public SchoolViewProcessingServiceTests()
        {
            this.schoolViewServiceMock = new Mock<ISchoolViewService>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.schoolViewProcessingService = new SchoolViewProcessingService(
                schoolViewService: this.schoolViewServiceMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static List<SchoolView> CreateRandomSchoolViews()
        {
            return CreateSchoolViewFiller()
                .Create(count: GetRandomNumber())
                    .ToList();
        }

        private static Filler<SchoolView> CreateSchoolViewFiller() =>
            new Filler<SchoolView>();
    }
}
