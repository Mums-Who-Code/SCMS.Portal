// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Portal.Web.Models.Foundations.Students;

namespace SCMS.Portal.Web.Brokers.Apis
{
    public partial class ApiBroker : IApiBroker
    {
        private const string StudentsRelativeUrl = "api/students";

        public async ValueTask<Student> PostStudentAsync(Student student) =>
            await this.PostAsync(StudentsRelativeUrl, student);
    }
}
