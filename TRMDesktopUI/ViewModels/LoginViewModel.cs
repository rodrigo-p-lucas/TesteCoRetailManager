using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TRMDesktopUI.Helpers;

namespace TRMDesktopUI.ViewModels
{
    public class LoginViewModel : Screen
    {
		private IAPIHelper _apiHelper;
		private string _username;
		private string _password;

		public LoginViewModel(IAPIHelper apiHelper)
		{
			_apiHelper = apiHelper;
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
				var result = await _apiHelper.Authenticate(_username, _password);
				MessageBox.Show("Login realizado com sucesso!", "Login", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Erro ao logar: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
			}

		}
	}
}
