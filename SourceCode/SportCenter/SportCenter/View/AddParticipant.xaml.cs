using SportCenter.View;
using SportCenter.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SportCenter.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddParticipant : ContentPage
    {
        public AddParticipant()
        {
            InitializeComponent();
        }

        // Handle hardware back button press
        protected override bool OnBackButtonPressed() {
            var trn = ((AddParticipantViewModel)BindingContext).CurrentTournament;
            var db = ((AddParticipantViewModel)BindingContext).Db;
            Application.Current.MainPage = new TournamentPage();
            Application.Current.MainPage.BindingContext = new TournamentPageViewModel(trn, db);
            ((TournamentPage)Application.Current.MainPage).CurrentPage =
                ((TournamentPage)Application.Current.MainPage).Children[2];
            return true;
        }
    }
}