﻿using System;
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
				this.Email = string.Empty;
				this.Password = string.Empty;
				//Redirigir a otra pagina.
				//Antes de redirigir la pagina, obtenemos la instancia de la ListPage y la creamos para que no haya error.
				MainViewModel.GetInstance().ListPage = new ListViewModel();
				await Application.Current.MainPage.Navigation.PushAsync(new ListPage());
			}
		}
		#endregion

		#region Properties
		public string Email
		{
			get { return this.email; }
			set
			{
				SetProperty(ref this.email, value);
			}
		}

		public string Password
		{
			get { return this.password; }
			set { SetProperty(ref this.password, value); }
		}
		public bool IsRunning
		{
			get { return this.isRunning; }
			set { SetProperty(ref this.isRunning, value); }
		}
		public bool IsRemembered
		{
			get { return this.isEnabled; }
			set { SetProperty(ref this.isEnabled, value); }
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

		#endregion
		private async void OnLoginClicked(object obj)
		{
			// Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
			await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
		}
	}
}
