// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Portal.Web.Models.Views.StudentViews.Exceptions
{
    public class StudentViewDependencyValidationException : Xeption
    {
        public StudentViewDependencyValidationException(Exception innerException)
            : base("Student view dependency validation error occured, try again.", innerException)
        { }
    }
}
