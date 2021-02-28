using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAPP.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new MainViewModel();
        }
        public void ShowList()
		{
            this.BindingContext = new ItemsPage();

		}
    }
}