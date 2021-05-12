using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using SportCenter.Helper;
using SportCenter.Models;
using Xamarin.Forms;

namespace SportCenter.ViewModels
{
    class MainPageViewModel : ObservableObject
    {
       
        Repository.Repository repository = new Repository.Repository();
        private ObservableCollection<TournamentModel> _tournaments;
        public ObservableCollection<TournamentModel> tournaments 
        {
            get => _tournaments;
            set { _tournaments = value;
                OnPropertyChanged("tournaments");
            }
        }
        public MainPageViewModel()
        {
            _tournaments = new ObservableCollection<TournamentModel>();
            UpdateTournamentsFromList();
            MessagingCenter.Subscribe<object>(this , "updateTournaments", Callback);
        }
        private void Callback(object _obj) {
            var lst = (List<TournamentModel>) _obj;
            tournaments = new ObservableCollection<TournamentModel>(lst);
        }
        public void UpdateTournamentsFromList()
        {
            List<TournamentModel> tour = Task.Run(()=>repository.GetTournaments()).Result;
            foreach (TournamentModel turneu in tour)
            {
                int numofparticipants = 0;
                if (turneu.Team == true)
                {
                    numofparticipants = numofparticipants + turneu.Teams.Count;
                    foreach(var team in turneu.Teams)
                    {
                        if(turneu.Format == "Single Elimination")
                        {
                            team.VisiblePoints = false;
                            foreach (var participant in team.Participants)
                            {
                                participant.VisiblePoints = false;
                            }
                        }
                        else
                        {
                            team.VisiblePoints = true;
                            foreach (var participant in team.Participants)
                            {
                                participant.VisiblePoints = false;
                            }
                        }
                    }
                }
                else
                {
                    if (turneu.Teams.Count !=0)
                    {
                        if (turneu.Format == "Single Elimination")
                        {
                            turneu.Teams[0].VisiblePoints = false;
                            foreach (var participant in turneu.Teams[0].Participants)
                            {
                                participant.VisiblePoints = false;
                            }
                        }
                        else
                        {
                            turneu.Teams[0].VisiblePoints = false;
                            foreach (var participant in turneu.Teams[0].Participants)
                            {
                                participant.VisiblePoints = true;
                            }
                        }
                        numofparticipants = numofparticipants + turneu.Teams[0].Participants.Count;
                    }
                }

                turneu.Matches = new ObservableCollection<MatchModel>(turneu.Matches.OrderBy((el) => el.Stage));

                foreach (MatchModel match in turneu.Matches)
                {
                    match.WinIconTwo = false;
                    match.WinIconOne = false;
                    if (match.PlayerOne == match.Winner)
                        match.WinIconOne = true;
                    if (match.PlayerTwo == match.Winner)
                        match.WinIconTwo = true;
                }
                switch (turneu.Format)
                {
                    case "Single Elimination":
                        { 
                            turneu.FormatIcon = <enter your api ip here>"https://REDACTED/tournamentSingle.png";
                            turneu.FormatColorLight = "#be1e2d";
                            turneu.FormatColorDark = "#a61a27";
                            turneu.AddButton = true;
                        }
                        break;
                    case "Round Robin":
                        {
                            turneu.FormatIcon = <enter your api ip here>"https://REDACTED/tournamentRoundRobin.png";
                            turneu.FormatColorLight = "#662d91";
                            turneu.FormatColorDark = "#592189";
                            if (turneu.Matches.Count == 0)
                                turneu.AddButton = true;
                            else
                                turneu.AddButton = false;
                        }
                        break;
                    case "Tur Retur":
                        {
                            turneu.FormatIcon = <enter your api ip here>"https://REDACTED/tournamentTurRetur.png";
                            turneu.FormatColorLight = "#00aeef";
                            turneu.FormatColorDark = "#0098ed";
                            if (turneu.Matches.Count == 0)
                                turneu.AddButton = true;
                            else
                                turneu.AddButton = false;
                        }
                        break;
                    default:
                        {
                            turneu.FormatIcon = <enter your api ip here>"https://REDACTED/tournamentLogo.png";
                            turneu.FormatColorLight = "#be1e2d";
                            turneu.FormatColorDark = "#a61a27";
                            if (turneu.Matches.Count == 0)
                                turneu.AddButton = true;
                            else
                                turneu.AddButton = false;
                        }
                        break;
                }
                turneu.NumberOfParticipants = numofparticipants;
                _tournaments.Add(turneu);
                
            }
        }
    }

}
