// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SCMS.Portal.Web.Models.Foundations.Students;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Foundations.Students
{
    public partial class StudentServiceTests
    {
        [Fact]
        public async Task ShouldAddStudentAsync()
        {
            //given
            Student randomStudent = CreateRandomStudent();
            Student inputStudent = randomStudent;
            Student retrievedStudent = inputStudent;
            Student expectedStudent = retrievedStudent.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.PostStudentAsync(inputStudent))
                    .ReturnsAsync(retrievedStudent);

            //when
            Student actualStudent =
                await this.studentService.AddStudentAsync(inputStudent);

            //then
            actualStudent.Should().BeEquivalentTo(expectedStudent);

            this.apiBrokerMock.Verify(broker =>
                broker.PostStudentAsync(inputStudent),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
