﻿// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using SCMS.Portal.Web.Models.Views.Components.Containers;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews;
using SCMS.Portal.Web.Services.Views.Foundations.SchoolViews;
using SCMS.Portal.Web.Views.Bases.Dropdowns.AutoCompletes;

namespace SCMS.Portal.Web.Views.Components.SchoolSelections
{
    public partial class SchoolSelectionComponent : ComponentBase
    {
        [Inject]
        public ISchoolViewService SchoolViewService { get; set; }

        public ComponentState State { get; set; }
        public List<SchoolView> SchoolViews { get; set; }
        public DropdownAutoCompleteBase<SchoolView> SchoolsDropdown { get; set; }
        public SchoolView SelectedSchool { get; set; }
        public EventCallback<SchoolView> SetSelectedSchool { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.SchoolViews = await this.SchoolViewService.RetrieveAllSchoolViewsAsync();
            this.State = ComponentState.Content;
        }

        public async Task OnSelectedItemChanged(SchoolView schoolView)
        {
            await SetSelectedSchoolValue(schoolView);
        }

        public async Task SetSelectedSchoolValue(SchoolView schoolView)
        {
            await SetSelectedSchool.InvokeAsync();
        }
    }
}
