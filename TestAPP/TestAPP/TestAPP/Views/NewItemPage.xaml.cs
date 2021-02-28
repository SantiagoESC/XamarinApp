using System;
using System.Collections.Generic;
using System.ComponentModel;
using TestAPP.Models;
using TestAPP.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestAPP.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}