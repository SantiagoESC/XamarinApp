using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
			try
			{

				var response = apiService.MakeRequest("GET", "https://www.thecocktaildb.com/api/json/v1/1/filter.php?c=Cocktail");
				if (response != string.Empty)
				{

				}
			}catch(Exception ex)
			{
				Console.WriteLine(ex);
			}
			//this.Drinks = new ObservableCollection<Drink>(drinks);
		}
		#endregion

	}
}
