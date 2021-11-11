// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Portal.Web.Brokers.Apis;
using SCMS.Portal.Web.Brokers.Loggings;
using SCMS.Portal.Web.Models.Foundations.Students;

namespace SCMS.Portal.Web.Services.Foundations.Students
{
    public class StudentService : IStudentService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public StudentService(
            IApiBroker apiBroker,
            ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<Student> AddStudentAsyc(Student student) =>
            await this.apiBroker.PostStudentAsync(student);
    }
}
