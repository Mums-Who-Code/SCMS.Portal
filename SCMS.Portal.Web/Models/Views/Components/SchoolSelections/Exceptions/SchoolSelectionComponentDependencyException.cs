// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Views.Components.SchoolSelections.Exceptions
{
    public class SchoolSelectionComponentDependencyException : Xeption
    {
        public SchoolSelectionComponentDependencyException(Xeption innerException)
            : base(message: "School selection dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
