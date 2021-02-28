using System.ComponentModel;
using TestAPP.ViewModels;
using Xamarin.Forms;

namespace TestAPP.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}