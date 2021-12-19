// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using SCMS.Portal.Web.Models.Views.Components.Containers;
using SCMS.Portal.Web.Models.Views.Components.SchoolSelections.Exceptions;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews.Exceptions;
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
        public SchoolSelectionComponentException Exception { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                this.SchoolViews = await this.SchoolViewService.RetrieveAllSchoolViewsAsync();
                this.State = ComponentState.Content;
            }
            catch (SchoolViewDependencyException schoolViewDependencyException)
            {
                this.State = ComponentState.Error;
                this.Exception = new SchoolSelectionComponentException(
                    schoolViewDependencyException);
            }
            catch (SchoolViewServiceException schoolViewServiceException)
            {
                this.State = ComponentState.Error;
                this.Exception = new SchoolSelectionComponentException(
                    schoolViewServiceException);
            }
            catch (Exception exception)
            {
                this.State = ComponentState.Error;
                this.Exception = new SchoolSelectionComponentException(exception);
            }
        }

        public async Task OnSelectedItemChanged(SchoolView schoolView)
        {
            try
            {
                await SetSelectedSchoolValue(schoolView);
            }
            catch (Exception exception)
            {
                this.State = ComponentState.Error;
                this.Exception = new SchoolSelectionComponentException(exception);
            }
        }

        public async Task SetSelectedSchoolValue(SchoolView schoolView)
        {
            this.SelectedSchool = schoolView;
            await SetSelectedSchool.InvokeAsync();
        }
    }
}
