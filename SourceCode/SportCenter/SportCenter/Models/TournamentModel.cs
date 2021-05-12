using System.Collections.ObjectModel;
using System.Linq;

namespace SportCenter.Models {
    public class TournamentModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Format { get; set; }
        public string Description { get; set; }
        public bool Team { get; set; }
        public ObservableCollection<TeamModel> Teams { get; set; }
        public ObservableCollection<MatchModel> Matches { get; set; }
        public int NumberOfParticipants { get; set; }
        public string SoloTeam {
            get {
                if (Team)
                    return "Turneu pe echipe";
                return "Turneu solo";
            }
        }
        public string FormatIcon { get; set; }
        public string FormatColorDark { get; set; }
        public string FormatColorLight { get; set; }
        public bool AddButton { get; set; }
        public bool AddTeamButton { get; set; }

        public void prepareInnerProperties() {
            var numofparticipants = 0;
            if (Team) {
                AddTeamButton = true;
                numofparticipants = numofparticipants + Teams.Count;
                foreach (var team in Teams)
                    if (Format == "Single Elimination") {
                        team.VisiblePoints = false;
                        foreach (var participant in team.Participants) participant.VisiblePoints = false;
                    }
                    else {
                        team.VisiblePoints = true;
                        foreach (var participant in team.Participants) participant.VisiblePoints = false;
                    }
            }
            else {
                AddTeamButton = false;
                if (Teams.Count != 0) {
                    if (Format == "Single Elimination") {
                        Teams[0].VisiblePoints = false;
                        foreach (var participant in Teams[0].Participants) participant.VisiblePoints = false;
                    }
                    else {
                        Teams[0].VisiblePoints = false;
                        foreach (var participant in Teams[0].Participants) participant.VisiblePoints = true;
                    }

                    numofparticipants = numofparticipants + Teams[0].Participants.Count;
                }
            }

            Matches = new ObservableCollection<MatchModel>(Matches.OrderBy((el) => el.Stage));

            foreach (var match in Matches) {
                match.WinIconTwo = false;
                match.WinIconOne = false;
                if (match.PlayerOne == match.Winner)
                    match.WinIconOne = true;
                if (match.PlayerTwo == match.Winner)
                    match.WinIconTwo = true;
            }

            switch (Format) {
                case "Single Elimination": {
                    FormatIcon = "tournamentSingle.png";
                    FormatColorLight = "#be1e2d";
                    FormatColorDark = "#a61a27";
                    AddButton = true;
                }
                    break;
                case "Round Robin": {
                    FormatIcon = "tournamentRoundRobin.png";
                    FormatColorLight = "#662d91";
                    FormatColorDark = "#592189";
                    if (Matches.Count == 0)
                        AddButton = true;
                    else
                        AddButton = false;
                }
                    break;
                case "Tur Retur": {
                    FormatIcon = "tournamentTurRetur.png";
                    FormatColorLight = "#00aeef";
                    FormatColorDark = "#0098ed";
                    if (Matches.Count == 0)
                        AddButton = true;
                    else
                        AddButton = false;
                }
                    break;
                default: {
                    FormatIcon = "logo.png";
                    FormatColorLight = "#be1e2d";
                    FormatColorDark = "#a61a27";
                    if (Matches.Count == 0)
                        AddButton = true;
                    else
                        AddButton = false;
                }
                    break;
            }

            this.NumberOfParticipants = numofparticipants;
        }
    }
}