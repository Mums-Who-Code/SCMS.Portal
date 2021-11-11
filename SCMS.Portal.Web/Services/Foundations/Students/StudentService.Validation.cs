// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Portal.Web.Models.Foundations.Students;
using SCMS.Portal.Web.Services.Foundations.Students.Exceptions;

namespace SCMS.Portal.Web.Services.Foundations.Students
{
    public partial class StudentService
    {
        private void ValidateStudent(Student student)
        {
            if (student is null)
            {
                throw new NullStudentException();
            }
        }
    }
}