using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using SportCenter.View;
using SportCenter.Helper;
using SportCenter.Models;
using Xamarin.Forms;

namespace SportCenter.ViewModels
{
    class AddParticipantViewModel : ObservableObject
    {
        private Repository.Repository db;
        private TournamentModel currentTournament;
        private TeamModel currentTeam;
        private string name;
        private string email;
        private string phoneNr;
        private ICommand closeCommand;
        private ICommand saveCommand;
        public Repository.Repository Db {
            get => db;
            set {
                if (value == db) return;
                db = value;
                OnPropertyChanged("Db");
            }
        }
        public TournamentModel CurrentTournament {
            get => currentTournament;
            set {
                if (currentTournament == value) return;
                currentTournament = value;
                OnPropertyChanged("CurrentTournament");
            }
        }
        public TeamModel CurrentTeam {
            get => currentTeam;
            set {
                if (value == currentTeam) return;
                currentTeam = value;
                OnPropertyChanged("CurrentTeam");
            }
        }
        public string Name {
            get => name;
            set {
                if (name == value) return;
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Email {
            get => email;
            set {
                if (email == value) return;
                email = value;
                OnPropertyChanged("Email");
            }
        }
        public string PhoneNr {
            get => phoneNr;
            set {
                if (phoneNr == value) return;
                phoneNr = value;
                OnPropertyChanged("PhoneNr");
            }
        }
        public ICommand CloseCommand {
            get {
                if (closeCommand == null) {
                    closeCommand = new RelayCommand<object>(
                        (_p) => { closePage(); },
                        _p => true);
                }

                return closeCommand;
            }
        }
        public ICommand SaveCommand {
            get {
                if (saveCommand == null) {
                    saveCommand = new RelayCommand<object>(
                        (_p) => { saveOptions(); },
                        (_p) => {
                            var model = _p as object[];
//                            var model = (AddParticipantViewModel) _p;
                            if (model == null)
                                return false;
                            return !string.IsNullOrEmpty((string)model[0]) && !string.IsNullOrEmpty((string)model[1]) &&
                                   !string.IsNullOrEmpty((string)model[2]);
                        });
                }

                return saveCommand;
            }
        }
        public AddParticipantViewModel(TeamModel _team, TournamentModel _tm, Repository.Repository _db) {
            CurrentTeam = _team;
            CurrentTournament = _tm;
            Db = _db;
        }

        public async void saveOptions() {
            bool answer = await Application.Current.MainPage.DisplayAlert("PadelCenter", "Sunteți sigur că doriți să adăugati un participant nou?", "Da", "Nu");
            if (answer == true) {
                await Db.PostParticipantAsync(new Models.PostParticipantModel() { Name = Name, Mail = Email, PhoneNumber = PhoneNr, TeamModelId = CurrentTeam.Id, Points = 0 });
                Application.Current.MainPage = new TournamentPage();
                Application.Current.MainPage.BindingContext = new TournamentPageViewModel(CurrentTournament, db);
                ((TournamentPageViewModel)Application.Current.MainPage.BindingContext).updateTournamentPage();
                ((TournamentPage)Application.Current.MainPage).CurrentPage =
                    ((TournamentPage)Application.Current.MainPage).Children[2];
            }
        }
        public void closePage() {
            Application.Current.MainPage = new TournamentPage();
            Application.Current.MainPage.BindingContext = new TournamentPageViewModel(CurrentTournament, db);
            ((TournamentPage)Application.Current.MainPage).CurrentPage =
                ((TournamentPage)Application.Current.MainPage).Children[2];
        }
    }
}
