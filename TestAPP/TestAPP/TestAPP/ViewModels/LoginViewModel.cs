using System;
using System.Collections.Generic;
using System.Text;
using TestAPP.Views;
using Xamarin.Forms;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace TestAPP.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
		#region Commands
		//public Command LoginCommand { get; }

        public ICommand LoginCommand {
			get
			{
                return new RelayCommand(Login);
			} 
        }

		private async void Login()
		{
			/*if (string.IsNullOrEmpty(this.Email) && string.IsNullOrWhiteSpace(this.Email))
			{
				await Application.Current.MainPage.DisplayAlert("Error", "Your Email is empty or incorrect. Check!", "Accept");
				return;
			}*/
			if (string.IsNullOrEmpty(this.Password) && string.IsNullOrWhiteSpace(this.Password))
			{
				await Application.Current.MainPage.DisplayAlert("Error", "Your Password is empty or incorrect. Check!", "Accept");

				return;
			}
			this.IsRunning = true;
		}
		#endregion
		#region Properties
		public string Email { get; set; }

        public string Password { get; set; }
        public bool IsRunning { get; set; }
        public bool IsRemembered { get; set; }
		#endregion

		#region Constructor
		public LoginViewModel()
        {
            this.IsRemembered = true;
            

          //  LoginCommand = new Command(OnLoginClicked);
        }

		#endregion

		#region Privates Methods

		#endregion
		private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
