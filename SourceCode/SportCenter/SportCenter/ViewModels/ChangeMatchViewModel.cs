using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using SportCenter.Helper;
using SportCenter.Models;
using SportCenter.View;
using Xamarin.Forms;

namespace SportCenter.ViewModels
{
    class ChangeMatchViewModel : ObservableObject
    {
        private Repository.Repository db;
        private TournamentModel currentTournament;
        private MatchModel currentMatch;
        private DateTime date;
        private TimeSpan time;
        private string winner;
        private ObservableCollection<string> winnerOptions;
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
        public MatchModel CurrentMatch {
            get => currentMatch;
            set {
                if (value == currentMatch) return;
                currentMatch = value;
                OnPropertyChanged("CurrentMatch");
            }
        }
        public DateTime Date {
            get => date;
            set {
                if (value == date) return;
                date = value;
                OnPropertyChanged("Date");
            }
        }
        public TimeSpan Time {
            get => time;
            set {
                time = value;
                OnPropertyChanged("Time");
            }
        }
        public string Winner {
            get => winner;
            set {
                if (winner == value) return;
                winner = value;
                OnPropertyChanged("Winner");
            }
        }
        public ObservableCollection<string> WinnerOptions {
            get => winnerOptions;
            set {
                if(value == winnerOptions) return;
                winnerOptions = value;
                OnPropertyChanged("WinnerOptions");
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
                        _p => true);
                }

                return saveCommand;
            }
        }
        public ChangeMatchViewModel(MatchModel _matchModel, TournamentModel _tm, Repository.Repository _db) {
            db = _db;
            CurrentTournament = _tm;
            CurrentMatch = _matchModel;
            Date = new DateTime(_matchModel.Date.Year, _matchModel.Date.Month, _matchModel.Date.Day, _matchModel.Date.Hour, _matchModel.Date.Minute, 0);
            Time = new TimeSpan(_matchModel.Date.Hour, _matchModel.Date.Minute, 0);
            WinnerOptions = new ObservableCollection<string>(){_matchModel.PlayerOne,_matchModel.PlayerTwo};
            if (_matchModel.Winner != null) {
                if (_matchModel.Winner == _matchModel.PlayerOne)
                    Winner = _matchModel.PlayerOne;
                else {
                    Winner = _matchModel.PlayerTwo;
                }
            }
        }
        public async Task AddPoints(string winner, TournamentModel selectedTournament)
        {
            if (selectedTournament.Format!="Single Elimination")
            {
                if(selectedTournament.Team == true)
                {
                    foreach(var team in selectedTournament.Teams)
                    {
                        if(team.Name == winner)
                        {
                            await db.UpdateTeamAsync(new PostTeamModel() {Id = team.Id, Name = team.Name, TournamentModelId = team.TournamentModelId, Points = team.Points + 3 });
                        }
                    }
                }
                else
                {
                    foreach(var participant in selectedTournament.Teams[0].Participants)
                    {
                        if(participant.Name == winner)
                        {
                            await db.UpdateParticipantAsync(new PostParticipantModel() { Id = participant.Id, Mail = participant.Mail, Name = participant.Name, PhoneNumber = participant.PhoneNumber, TeamModelId = participant.TeamModelId, Points = participant.Points + 3 });
                        }
                    }
                }
            }
        }
        public async void saveOptions() {
            DateTime _dt = new DateTime(Date.Year, Date.Month, Date.Day, Time.Hours, Time.Minutes, 0);
            bool answer = await Application.Current.MainPage.DisplayAlert("SportCenter", "Sunteți sigur că doriți să schimbați datele meciului?", "Da", "Nu");
            if (answer == true) {
                await db.UpdateMatchAsync(new PostMatchModel() { Id = CurrentMatch.Id, Date = _dt, PlayerOne = CurrentMatch.PlayerOne, PlayerTwo = CurrentMatch.PlayerTwo, Winner = Winner, Stage = CurrentMatch.Stage, TournamentModelId = CurrentMatch.TournamentModelId });
                await AddPoints(Winner, CurrentTournament);
                Application.Current.MainPage = new TournamentPage();
                Application.Current.MainPage.BindingContext = new TournamentPageViewModel(CurrentTournament, db);
                ((TournamentPageViewModel)Application.Current.MainPage.BindingContext).updateTournamentPage();
                ((TournamentPage)Application.Current.MainPage).CurrentPage =
                    ((TournamentPage)Application.Current.MainPage).Children[1];
            }


        }
        public void closePage() {
            Application.Current.MainPage = new TournamentPage();
            Application.Current.MainPage.BindingContext = new TournamentPageViewModel(CurrentTournament, db);
            ((TournamentPage) Application.Current.MainPage).CurrentPage =
                ((TournamentPage) Application.Current.MainPage).Children[1];
        }
    }
}
