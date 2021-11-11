﻿// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Foundations.Students.Exceptions
{
    public class InvalidStudentException : Xeption
    {
        public InvalidStudentException()
            : base(message: "Invalid student, fix the errors and try again.")
        { }
    }
}
