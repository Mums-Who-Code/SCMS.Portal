// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews;

namespace SCMS.Portal.Web.Services.Views.Processings.SchoolViews
{
    public interface ISchoolViewProcessingService
    {
        ValueTask<List<SchoolView>> RetrieveAllSchoolViewsAsync();
    }
}
