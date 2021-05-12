using System;
using SportCenter.Models;
using SportCenter.ViewModels;
using Xamarin.Forms;

namespace SportCenter.View
{ 
    public partial class MainPage : ContentPage
    {
        TournamentModel tournamentModel;
        private Repository.Repository db;
        public MainPage() {
            InitializeComponent();
            db = new Repository.Repository();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            tournamentModel = (TournamentModel)MainPageListView.SelectedItem;
            tournamentModel.prepareInnerProperties();
            Application.Current.MainPage = new TournamentPage();
            Application.Current.MainPage.BindingContext = new TournamentPageViewModel(tournamentModel, db);
//            SendMessageTournament();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new CreateTournament();
        }
//        public void SendMessageTournament()
//        {
//            MessagingCenter.Send(this, "turneu", tournamentModel);
//        }
    }
}
