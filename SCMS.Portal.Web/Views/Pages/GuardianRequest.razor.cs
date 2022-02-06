// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace SCMS.Portal.Web.Views.Pages
{
    public partial class GuardianRequest : ComponentBase
    {
        [Parameter]
        public string StudentId { get; set; }
    }
}
