// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Portal.Web.Models.Views.Components.SchoolSelections.Exceptions
{
    public class SchoolSelectionComponentServiceException : Xeption
    {
        public SchoolSelectionComponentServiceException(Exception innerException)
            : base(message: "School selection service error occurred, contact support.",
                  innerException)
        { }
    }
}
