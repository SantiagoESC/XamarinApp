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
			var response =  apiService.MakeRequest("GET", "https://www.thecocktaildb.com/api/json/v1/1/filter.php?c=Cocktail");
			Console.WriteLine(response.StatusCode);
			var a =response.StatusCode;
			if (response.StatusCode != System.Net.HttpStatusCode.OK)
			{
				await Application.Current.MainPage.DisplayAlert("Error", response.StatusDescription, "Accept");
				return;
			}

			var drinks =(List<Drink>) response.;
			this.Drinks = new ObservableCollection<Drink>(drinks);
		}
		#endregion

	}
}
