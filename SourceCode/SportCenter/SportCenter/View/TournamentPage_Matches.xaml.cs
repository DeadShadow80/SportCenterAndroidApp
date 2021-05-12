using System;
using SportCenter.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SportCenter.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TournamentPage_Matches : ContentPage
    {
        public TournamentPage_Matches()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
            Application.Current.MainPage.BindingContext = new MainPageViewModel();
        }

        // Handle hardware back button press
        protected override bool OnBackButtonPressed() {
            Application.Current.MainPage = new MainPage();
            Application.Current.MainPage.BindingContext = new MainPageViewModel();
            return true;
        }
    }
}