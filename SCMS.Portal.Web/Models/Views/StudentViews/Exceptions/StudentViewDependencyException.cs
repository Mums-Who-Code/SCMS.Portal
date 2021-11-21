// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Portal.Web.Models.Views.StudentViews.Exceptions
{
    public class StudentViewDependencyException : Xeption
    {
        public StudentViewDependencyException(Exception innerException)
            : base("Student view dependency error occured, contact support.", innerException)
        { }
    }
}
