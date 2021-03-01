using System;
using System.Collections.Generic;
using System.Text;
using TestAPP.Views;
using Xamarin.Forms;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;

namespace TestAPP.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		#region Commands
		//public Command LoginCommand { get; }

		public ICommand LoginCommand
		{
			get
			{
				return new RelayCommand(Login);
			}
		}

		private async void Login()
		{
			if (string.IsNullOrEmpty(this.Email) && string.IsNullOrWhiteSpace(this.Email))
			{
				await Application.Current.MainPage.DisplayAlert("Error", "Your Email is empty or incorrect. Check!", "Accept");
				return;
			}
			if (string.IsNullOrEmpty(this.Password) && string.IsNullOrWhiteSpace(this.Password))
			{
				await Application.Current.MainPage.DisplayAlert("Error", "Your Password is empty or incorrect. Check!", "Accept");

				return;
			}
			this.IsRunning = true;

			this.isEnabled = false;
			//Prueba de Navegacion.
			if (!this.Email.Contains("sescribas@gmail.com") && !this.Password.Contains("123"))
			{
				this.IsRunning = false;
				this.isEnabled = true;
				await Application.Current.MainPage.DisplayAlert(
						"Error",
						"Email or Password Incorrect. Check!",
						"Accept");
				this.Password = string.Empty;

				return;
			}
			else
			{
				this.IsRunning = true;
				this.isEnabled = false;
				await Application.Current.MainPage.DisplayAlert("Success", "Redirect to the app", "Accept");
			}
		}
		#endregion

		#region Properties
		public string Email { get; set; }

		public string Password
		{
			get { return this.password; }
			set
			{
				if (this.password != value)
				{
					this.password = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Password)));
				}
			}
		}
		public bool IsRunning
		{
			get { return this.isRunning; }
			set
			{
				if (this.isRunning != value)
				{
					this.isRunning = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.IsRunning)));
				}
			}
		}
		public bool IsRemembered
		{
			get { return this.isEnabled; }
			set
			{
				if (this.isEnabled != value)
				{
					this.isEnabled = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.isEnabled)));
				}
			}
		}
		#endregion

		#region Attributes
		private string email;
		private string password;

		private bool isRunning;

		private bool isEnabled;
		#endregion

		#region Constructor
		public LoginViewModel()
		{
			this.IsRemembered = true;


			//  LoginCommand = new Command(OnLoginClicked);
		}

		#endregion

		#region Events
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
		private async void OnLoginClicked(object obj)
		{
			// Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
			await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
		}
	}
}
