// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Portal.Web.Models.Views.Foundations.StudentViews.Exceptions
{
    public class FailedStudentViewServiceException : Xeption
    {
        public FailedStudentViewServiceException(Exception innerException)
            : base(message: "Failed student view service error occurred.", innerException)
        { }
    }
}
