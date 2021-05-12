using System;
using SportCenter.Models;
using SportCenter.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SportCenter.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateTournament : ContentPage
    {
        Repository.Repository repository;
        string name;
        string description;
        string format;
        bool team;
        public CreateTournament()
        {
            InitializeComponent();
            repository = new Repository.Repository();
        }
        // Handle hardware back button press
        protected override bool OnBackButtonPressed() {
            Application.Current.MainPage = new MainPage();
            Application.Current.MainPage.BindingContext = new MainPageViewModel();
            return true;
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
            Application.Current.MainPage.BindingContext = new MainPageViewModel();
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            name = string.IsNullOrEmpty(Entry.Text) ? "turneu" : Entry.Text;
            description = string.IsNullOrEmpty(Editor.Text) ? "descriere" : Editor.Text;
            format = Picker.SelectedItem == null ? "Round Robin" : Picker.SelectedItem.ToString();
            team = Switch.IsToggled;
            bool answer = await DisplayAlert("SportCenter", "Sunteți sigur că doriți să adăugati un turneu nou?", "Da", "Nu");
            if (answer == true)
            {
                int mama = await repository.PostTournamentAsync(new PostTournament() { Name = name, Description = description, Format = format, Team = team });
                Application.Current.MainPage = new MainPage();
                Application.Current.MainPage.BindingContext = new MainPageViewModel();
            }
        }
    }
}