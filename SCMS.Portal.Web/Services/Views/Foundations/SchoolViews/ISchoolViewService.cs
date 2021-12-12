// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews;

namespace SCMS.Portal.Web.Services.Views.Foundations.SchoolViews
{
    public interface ISchoolViewService
    {
        ValueTask<List<SchoolView>> RetrieveAllSchoolsAsync();
    }
}
