// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Portal.Web.Models.Foundations.Students;

namespace SCMS.Portal.Web.Services.Foundations.Students
{
    public interface IStudentService
    {
        ValueTask<Student> AddStudentAsync(Student student);
    }
}
