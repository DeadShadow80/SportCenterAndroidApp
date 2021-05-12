using System;
using SportCenter.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SportCenter.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TournamentPage_Participants : ContentPage
    {
        public TournamentPage_Participants()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
            Application.Current.MainPage.BindingContext = new MainPageViewModel();
        }

        // Handling hardware back button press
        protected override bool OnBackButtonPressed() {
            Application.Current.MainPage = new MainPage();
            Application.Current.MainPage.BindingContext = new MainPageViewModel();
            return true;
        }
    }
}