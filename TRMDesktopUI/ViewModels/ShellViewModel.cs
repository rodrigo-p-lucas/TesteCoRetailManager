﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TRMDesktopUI.Events;
using TRMDesktopUI.Library.Api;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private IEventAggregator _events;
        private ILoggedInUserModel _user;
        private IAPIHelper _apiHelper;

        public ShellViewModel(IEventAggregator events, ILoggedInUserModel user, IAPIHelper apiHelper)
        {
            _events = events;
            _user = user;
            _apiHelper = apiHelper;
            _events.SubscribeOnPublishedThread(this);

            ActivateItemAsync(IoC.Get<LoginViewModel>(), new CancellationToken());
        }

        public bool IsLoggedIn { 
            get
            {
                bool output = false;

                if (!string.IsNullOrWhiteSpace(_user.Token))
                {
                    output = true;
                }

                return output;
            } 
        }

        public async Task LogOut()
        {
            _user.ResetUserModel();
            _apiHelper.LogOffUser();
            NotifyOfPropertyChange(() => IsLoggedIn);
            await ActivateItemAsync(IoC.Get<LoginViewModel>(), new CancellationToken());
        }

        public void ExitApplication()
        {
            TryCloseAsync();
        }

        public async Task UserManagement()
        {
            await ActivateItemAsync(IoC.Get<UserDisplayViewModel>(), new CancellationToken());
        }

        public async Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
        {
            await ActivateItemAsync(IoC.Get<SalesViewModel>(), cancellationToken);
            NotifyOfPropertyChange(() => IsLoggedIn);
        }
    }
}
