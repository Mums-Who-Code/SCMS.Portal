// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace SCMS.Portal.Web.Brokers.Navigation
{
    public class NavigationBroker : INavigationBroker
    {
        private readonly NavigationManager navigationManager;
        public NavigationBroker(NavigationManager navigationManager) => 
            this.navigationManager = navigationManager;

        public void NavigateTo(string route) => 
            this.navigationManager.NavigateTo(route);
    }
}
