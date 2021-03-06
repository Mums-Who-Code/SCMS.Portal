// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Portal.Web.Models.Views.Components.SchoolSelections.Exceptions
{
    public class SchoolSelectionComponentException : Xeption
    {
        public SchoolSelectionComponentException(Exception innerException)
            : base(message: "School selection error occurred, contact support.",
                  innerException)
        { }
    }
}
