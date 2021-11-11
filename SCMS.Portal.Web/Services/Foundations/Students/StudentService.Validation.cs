// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using SCMS.Portal.Web.Models.Foundations.Students;
using SCMS.Portal.Web.Services.Foundations.Students.Exceptions;

namespace SCMS.Portal.Web.Services.Foundations.Students
{
    public partial class StudentService
    {
        private void ValidateStudent(Student student)
        {
            switch (student)
            {
                case null:
                    throw new NullStudentException();
                case { } when IsInvalid(student.Id):
                    throw new InvalidStudentException(
                        parameterName: nameof(Student.Id),
                        parameterValue: student.Id);
            }
        }

        private static bool IsInvalid(Guid id) =>
            id == Guid.Empty;
    }
}