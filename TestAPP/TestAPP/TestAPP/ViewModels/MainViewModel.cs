using System;
using System.Collections.Generic;
using System.Text;

namespace TestAPP.ViewModels
{
	public class MainViewModel
	{

		#region ViewModels
		public LoginViewModel Login { get; set; }
		public ListViewModel ListPage{ get; set; }

		#endregion

		#region Command

		#endregion

		#region Constructor

		public MainViewModel()
		{
			this.Login = new LoginViewModel();
		}
		#endregion

		//Aplicando patron de diseño Singleton para que solo exista 1 sola instancia de la MainViewModel.
		#region Singleton

		private static MainViewModel Instance;

		public static MainViewModel GetInstance()
		{
			if(Instance == null)
			{
				return new MainViewModel();

			}
			else
			{
				return Instance;
			}
		}
		#endregion
	}
}
