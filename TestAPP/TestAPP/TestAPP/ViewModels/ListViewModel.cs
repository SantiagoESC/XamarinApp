using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TestAPP.Models;
using TestAPP.Services;
using Xamarin.Forms;

namespace TestAPP.ViewModels
{
	public class ListViewModel : BaseViewModel
	{
		#region Services
		private ApiService apiService;
		#endregion

		#region Atributtes
		//
		private ObservableCollection<Drink> drinks;
		#endregion

		#region Properties

		public ObservableCollection<Drink> Drinks
		{
			get { return this.drinks; }
			set { SetProperty(ref this.drinks, value); }
		}
		#endregion

		#region Constructor

		public ListViewModel()
		{
			this.apiService = new ApiService();
			this.LoadDrinks();
		}
		#endregion

		#region Methods
		public async void LoadDrinks()
		{
			var response = await this.apiService.GetList<Drink>(
				"https://www.thecocktaildb.com",
				"/api",
				"/json/v1/1/filter.php?c=Cocktail");
			if (!response.IsSuccess)
			{
				await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
				return;
			}

			var drinks =(List<Drink>) response.Result;
			this.Drinks = new ObservableCollection<Drink>(drinks);
		}
		#endregion

	}
}
