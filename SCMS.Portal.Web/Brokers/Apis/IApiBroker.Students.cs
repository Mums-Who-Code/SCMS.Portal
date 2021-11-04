// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Portal.Web.Models.Foundations.Students;

namespace SCMS.Portal.Web.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<Student> PostStudentAsync(Student student);
    }
}
