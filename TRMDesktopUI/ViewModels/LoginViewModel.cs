using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TRMDesktopUI.Events;
using TRMDesktopUI.Library.Api;

namespace TRMDesktopUI.ViewModels
{
	public class LoginViewModel : Screen
    {
		private IAPIHelper _apiHelper;
		private IEventAggregator _events;
		private string _username;
		private string _password;
		private string _errorMessage;

		public LoginViewModel(IAPIHelper apiHelper, IEventAggregator events)
		{
			_apiHelper = apiHelper;
			_events = events;
		}

		public string Username
		{
			get { return _username; }
			set { 
				_username = value;
				NotifyOfPropertyChange(() => _username);
				NotifyOfPropertyChange(() => CanLogIn);
			}
		}

		public string Password
		{
			get { return _password; }
			set { 
				_password = value;
				NotifyOfPropertyChange(() => _password);
				NotifyOfPropertyChange(() => CanLogIn);
			}
		}

		public bool IsErrorVisible
		{
			get
			{
				return ErrorMessage?.Length > 0;
			}
		}

		public string ErrorMessage
		{
			get { return _errorMessage; }
			set { 
				_errorMessage = value;
				NotifyOfPropertyChange(() => ErrorMessage);
				NotifyOfPropertyChange(() => IsErrorVisible);
			}
		}

		public bool CanLogIn
		{
			get
			{
				bool output = false;

				if (Username?.Length > 0 && Password?.Length > 0)
				{
					output = true;
				}

				return output;
			}
		}

		public async Task LogIn()
		{
			try
			{
				ErrorMessage = "";
				var result = await _apiHelper.Authenticate(_username, _password);

				await _apiHelper.GetLoggedInUserInfo(result.Access_Token);

				_events.PublishOnUIThread(new LogOnEvent());
			}
			catch (Exception ex)
			{
				ErrorMessage = ex.Message;
			}
		}
	}
}
