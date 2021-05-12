using SportCenter.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SportCenter.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeMatch : ContentPage
    {
        public ChangeMatch()
        {
            InitializeComponent();
        }

        // Handle hardware back button press
        protected override bool OnBackButtonPressed() {
            var trn = ((ChangeMatchViewModel) BindingContext).CurrentTournament;
            var db = ((ChangeMatchViewModel) BindingContext).Db;
            Application.Current.MainPage = new TournamentPage();
            Application.Current.MainPage.BindingContext = new TournamentPageViewModel(trn, db);
            ((TournamentPage)Application.Current.MainPage).CurrentPage =
                ((TournamentPage)Application.Current.MainPage).Children[1];
            return true;
        }
    }
}