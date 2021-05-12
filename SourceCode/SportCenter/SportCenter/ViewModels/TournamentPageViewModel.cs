using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using SportCenter.Helper;
using SportCenter.Models;
using SportCenter.View;
using Xamarin.Forms;

namespace SportCenter.ViewModels {
    class TournamentPageViewModel : ObservableObject
    {
        #region FIELDS
        private TournamentModel selectedTournament;
        private Repository.Repository db;
        private ICommand participantAddTeamCommand;
        private ICommand participantAddTeamMemberCommand;
        private ICommand participantRemoveTeamMemberCommand;
        private ICommand matchesGenerateCommand;
        private ICommand editMatchCommand;
        #endregion

        #region PROPERTIES
        public TournamentModel SelectedTournament {
            get => selectedTournament;
            set {
                if (selectedTournament == value) return;
                selectedTournament = value;
                OnPropertyChanged("SelectedTournament");
            }
        }
        public ICommand ParticipantAddTeamCommand {
            get {
                if (participantAddTeamCommand == null) {
                    participantAddTeamCommand = new RelayCommand<object>(
                        (_p) => { participantAddTeam(); },
                        _p => ((_p as bool?) == true));
                }

                return participantAddTeamCommand;
            }
        }
        public ICommand ParticipantAddTeamMemberCommand {
            get {
                if (participantAddTeamMemberCommand == null) {
                    participantAddTeamMemberCommand = new RelayCommand<object>(
                        (_p) => { participantAddTeamMember((TeamModel) _p); },
                        _p => (_p.GetType() == typeof(TeamModel)));
                }

                return participantAddTeamMemberCommand;
            }
        }
        public ICommand ParticipantRemoveTeamMemberCommand {
            get {
                if (participantRemoveTeamMemberCommand == null) {
                    participantRemoveTeamMemberCommand = new RelayCommand<object>(
                        (_p) => { participantRemoveTeamMember((TeamModel)_p); },
                        _p => (_p.GetType() == typeof(TeamModel)));
                }

                return participantRemoveTeamMemberCommand;
            }
        }
        public ICommand MatchesGenerateCommand {
            get {
                if (matchesGenerateCommand == null) {
                    matchesGenerateCommand = new RelayCommand<object>(
                        (_p) => { matchesGenerate(); },
                        _p => true);
                }

                return matchesGenerateCommand;
            }
        }
        public ICommand EditMatchCommand {
            get {
                if (editMatchCommand == null) {
                    editMatchCommand = new RelayCommand<object>(
                        (_p) => { editMatch((MatchModel)_p); },
                        _p => true);
                }

                return editMatchCommand;
            }
        }
        #endregion

        #region METHODS

        private async void participantAddTeam() {
            string result = await Application.Current.MainPage.DisplayPromptAsync("Adaugă echipă", "Care e numele echipei?");
            if (result != null)
            {
                var postteamModel = new PostTeamModel() { Name = result, TournamentModelId = SelectedTournament.Id, Points = 0 };
                await db.PostTeamAsync(postteamModel);
                updateTournamentPage();
            }
        }
        private async void participantAddTeamMember(TeamModel _t) {
            try {
                Application.Current.MainPage = new AddParticipant();

                Application.Current.MainPage.BindingContext = new AddParticipantViewModel(_t, SelectedTournament, db);
            }
            catch (Exception e) {

            }
        }
        private async void participantRemoveTeamMember(TeamModel _t) {
            List<string> arraylist = new List<string>();
            foreach (var participant in _t.Participants)
            {
                arraylist.Add(participant.Name);
            }
            string result = await Application.Current.MainPage.DisplayActionSheet("Pe cine doriti sa eliminati?", "Cancel", null, arraylist.ToArray());
            foreach(var participant in _t.Participants)
            {
                if (result == participant.Name)
                {
                    await db.DeleteParticipantAsync(new PostParticipantModel() {Name = participant.Name, Id = participant.Id, Mail = participant.Mail, PhoneNumber = participant.PhoneNumber, Points = participant.Points, TeamModelId = participant.TeamModelId });
                    updateTournamentPage();
                }
            }
        }


        #region Matches Generation
        public async Task RoundRobinSolo(List<ParticipantModel> list) {
            int c = 1;
            for (int i = 0; i < list.Count; i++) {
                for (int j = 0; j < list.Count; j++) {
                    if (j > i) {
                        await db.PostMatchAsync(new PostMatchModel() { Date = new DateTime(2021, 1, 1, 12, 0, 0), TournamentModelId = selectedTournament.Id, Winner = "No one", PlayerOne = list[i].Name, PlayerTwo = list[j].Name, Stage = "Runda " + (c + selectedTournament.Matches.Count) });
                        c++;
                    }

                }
            }

        }
        public async Task RoundRobinTeam(List<TeamModel> list) {
            int c = 1;
            for (int i = 0; i < list.Count; i++) {
                for (int j = 0; j < list.Count; j++) {
                    if (j > i) {
                        await db.PostMatchAsync(new PostMatchModel() { Date = new DateTime(2021, 1, 1, 12, 0, 0), TournamentModelId = selectedTournament.Id, Winner = "No one", PlayerOne = list[i].Name, PlayerTwo = list[j].Name, Stage = "Runda " + (c + selectedTournament.Matches.Count) });
                        c++;
                    }

                }
            }


        }
        public async Task TurReturSolo(List<ParticipantModel> list) {
            int c = 1;
            for (int i = 0; i < list.Count; i++) {
                for (int j = 0; j < list.Count; j++) {
                    if (j > i) {
                        await db.PostMatchAsync(new PostMatchModel() { Date = new DateTime(2021, 1, 1, 12, 0, 0), TournamentModelId = selectedTournament.Id, Winner = "No one", PlayerOne = list[i].Name, PlayerTwo = list[j].Name, Stage = "Runda " + (c + selectedTournament.Matches.Count) });
                        c++;
                    }

                }
            }
            for (int i = 0; i < list.Count; i++) {
                for (int j = 0; j < list.Count; j++) {
                    if (j > i) {
                        await db.PostMatchAsync(new PostMatchModel() { Date = new DateTime(2021, 1, 1, 12, 0, 0), TournamentModelId = selectedTournament.Id, Winner = "No one", PlayerOne = list[i].Name, PlayerTwo = list[j].Name, Stage = "Runda " + (c + selectedTournament.Matches.Count) });
                        c++;
                    }

                }
            }

        }
        public async Task TurReturTeam(List<TeamModel> list) {
            int c = 1;
            for (int i = 0; i < list.Count; i++) {
                for (int j = 0; j < list.Count; j++) {
                    if (j > i) {
                        await db.PostMatchAsync(new PostMatchModel() { Date = new DateTime(2021, 1, 1, 12, 0, 0), TournamentModelId = selectedTournament.Id, Winner = "No one", PlayerOne = list[i].Name, PlayerTwo = list[j].Name, Stage = "Runda " + (c + selectedTournament.Matches.Count) });
                        c++;
                    }

                }
            }
            for (int i = 0; i < list.Count; i++) {
                for (int j = 0; j < list.Count; j++) {
                    if (j > i) {
                        await db.PostMatchAsync(new PostMatchModel() { Date = new DateTime(2021, 1, 1, 12, 0, 0), TournamentModelId = selectedTournament.Id, Winner = "No one", PlayerOne = list[i].Name, PlayerTwo = list[j].Name, Stage = "Runda " + (c + selectedTournament.Matches.Count) });
                        c++;
                    }

                }
            }


        }
        public async Task SingleEliminationSolo(List<ParticipantModel> list) {


            int tournamentParticipants = list.Count;
            double bracketHeight = 1;
            int power = 2;
            double numOfGames = 0;
            double aux = 0;
            List<double> stages = new List<double>();
            List<MatchModel> matches = selectedTournament.Matches.ToList();
            for (int i = 2; i <= 7; i++) {
                if (tournamentParticipants > Math.Pow(2, i)) {
                    bracketHeight = Math.Pow(2, i + 1);
                    power = i + 1;
                }
            }
            numOfGames = bracketHeight - 1;
            stages.Add(0);
            while (power >= 2) {
                power--;
                aux = aux + Math.Pow(2, power) - 1;
                stages.Add(aux);
            }
            ///////////////////////////////////////////// ^ Etapa 1. Stabilirea variabilelor. 
            if (matches.Count == 0) {
                if (bracketHeight == tournamentParticipants) {
                    for (int i = 0; i < tournamentParticipants / 2; i++) {
                        await db.PostMatchAsync(new PostMatchModel() { Date = new DateTime(2021, 1, 1, 12, 0, 0), TournamentModelId = selectedTournament.Id, Winner = "No one", PlayerOne = list[i].Name, PlayerTwo = list[tournamentParticipants - 1 - i].Name, Stage = "Runda " + (i + selectedTournament.Matches.Count) });
                    }
                } else {
                    int x = 0;
                    for (int i = 0; i < bracketHeight / 2; i++) {
                        if (i < ((int)bracketHeight - tournamentParticipants)) {
                            await db.PostMatchAsync(new PostMatchModel() { Date = new DateTime(2021, 1, 1, 12, 0, 0), TournamentModelId = selectedTournament.Id, Winner = "No one", PlayerOne = list[i].Name, PlayerTwo = "swing", Stage = "Runda " + i });
                        } else {
                            await db.PostMatchAsync(new PostMatchModel() { Date = new DateTime(2021, 1, 1, 12, 0, 0), TournamentModelId = selectedTournament.Id, Winner = "No one", PlayerOne = list[i].Name, PlayerTwo = list[tournamentParticipants - 1 - x].Name, Stage = "Runda " + (i + selectedTournament.Matches.Count) });
                            x++;
                        }
                    }
                }
                //  Debug.WriteLine("bracketheight" + bracketHeight);

            } else {
                int x = 0;
                int y = 0;
                List<string> xList = new List<string>();
                while (x < matches.Count) {
                    x = x + (int)bracketHeight / 2;
                    y = (int)bracketHeight / 2;
                    bracketHeight = bracketHeight / 2;
                }
                for (int i = 0; i < y; i++) {
                    //count-i-1
                    xList.Add(matches[matches.Count - 1 - i].Winner);
                }
                for (int i = 0; i < xList.Count / 2; i++) {
                    await db.PostMatchAsync(new PostMatchModel() { Date = new DateTime(2021, 1, 1, 12, 0, 0), TournamentModelId = selectedTournament.Id, Winner = "No one", PlayerOne = xList[i], PlayerTwo = xList[xList.Count - 1 - i], Stage = "Runda " + (i + selectedTournament.Matches.Count) });

                }
            }


        }
        public async Task SingleEliminationTeam(List<TeamModel> list) {
            int tournamentParticipants = list.Count;
            double bracketHeight = 1;
            int power = 2;
            double aux = 0;
            List<double> stages = new List<double>();
            List<MatchModel> matches = selectedTournament.Matches.ToList();
            for (int i = 2; i <= 7; i++) {
                if (tournamentParticipants > Math.Pow(2, i)) {
                    bracketHeight = Math.Pow(2, i + 1);
                    power = i + 1;
                }
            }
            stages.Add(0);
            while (power >= 2) {
                power--;
                aux = aux + Math.Pow(2, power) - 1;
                stages.Add(aux);
            }
            ///////////////////////////////////////////// ^ Etapa 1. Stabilirea variabilelor. 
            if (matches.Count == 0) {
                if (bracketHeight == tournamentParticipants) {
                    for (int i = 0; i < tournamentParticipants / 2; i++) {
                        await db.PostMatchAsync(new PostMatchModel() { Date = new DateTime(2021, 1, 1, 12, 0, 0), TournamentModelId = selectedTournament.Id, Winner = "No one", PlayerOne = list[i].Name, PlayerTwo = list[tournamentParticipants - 1 - i].Name, Stage = "Runda " + (i + selectedTournament.Matches.Count) });
                    }
                } else {
                    int x = 0;
                    for (int i = 0; i < bracketHeight / 2; i++) {
                        if (i < ((int)bracketHeight - tournamentParticipants)) {
                            await db.PostMatchAsync(new PostMatchModel() { Date = new DateTime(2021, 1, 1, 12, 0, 0), TournamentModelId = selectedTournament.Id, Winner = "No one", PlayerOne = list[i].Name, PlayerTwo = "swing", Stage = "Runda " + (i + selectedTournament.Matches.Count) });
                        } else {
                            await db.PostMatchAsync(new PostMatchModel() { Date = new DateTime(2021, 1, 1, 12, 0, 0), TournamentModelId = selectedTournament.Id, Winner = "No one", PlayerOne = list[i].Name, PlayerTwo = list[tournamentParticipants - 1 - x].Name, Stage = "Runda " + (i + selectedTournament.Matches.Count) });
                            x++;
                        }
                    }
                }
                //  Debug.WriteLine("bracketheight" + bracketHeight);

            } else {
                int x = 0;
                int y = 0;
                List<string> xList = new List<string>();
                while (x < matches.Count) {
                    x = x + (int)bracketHeight / 2;
                    y = (int)bracketHeight / 2;
                    bracketHeight = bracketHeight / 2;
                }
                for (int i = 0; i < y; i++) {
                    //count-i-1
                    xList.Add(matches[matches.Count - 1 - i].Winner);
                }
                for (int i = 0; i < xList.Count / 2; i++) {
                    await db.PostMatchAsync(new PostMatchModel() { Date = new DateTime(2021, 1, 1, 12, 0, 0), TournamentModelId = selectedTournament.Id, Winner = "No one", PlayerOne = xList[i], PlayerTwo = xList[xList.Count - 1 - i], Stage = "Runda " + (i + selectedTournament.Matches.Count) });

                }
            }
        }
        #endregion
        private async void matchesGenerate() {
            
            bool answer = await Application.Current.MainPage.DisplayAlert("SportCenter", "Sunteți sigur că doriți să adăugati meciurile?", "Da", "Nu");
            if (answer == true)
            {
                if (SelectedTournament.Format == "Round Robin")
                {
                    if (SelectedTournament.Team == true)
                    {
                        await RoundRobinTeam(SelectedTournament.Teams.ToList());
                    }
                    else
                    {
                        await RoundRobinSolo(SelectedTournament.Teams[0].Participants.ToList());
                    }
                }
                else
                {
                    if (SelectedTournament.Format == "Tur Retur")
                    {
                        if (SelectedTournament.Team == true)
                        {
                            await TurReturTeam(SelectedTournament.Teams.ToList());
                        }
                        else
                        {
                            await TurReturSolo(SelectedTournament.Teams[0].Participants.ToList());
                        }
                    }
                    else
                    {
                        if (SelectedTournament.Format == "Single Elimination")
                        {
                            if (SelectedTournament.Team == true)
                            {
                                await SingleEliminationTeam(SelectedTournament.Teams.ToList());
                            }
                            else
                            {
                                await SingleEliminationSolo(SelectedTournament.Teams[0].Participants.ToList());
                            }
                        }
                    }
                }

                updateTournamentPage();
                Application.Current.MainPage = new TournamentPage();
                Application.Current.MainPage.BindingContext = new TournamentPageViewModel(SelectedTournament, db);
                ((TournamentPage)Application.Current.MainPage).CurrentPage =
                    ((TournamentPage)Application.Current.MainPage).Children[1];
            }
        }

        public void updateTournamentPage() {
            // Get the full list, and update the current tournament.
            List<TournamentModel> lst = Task.Run(() => db.GetTournaments()).Result;
            foreach (var el in lst) {
                if (el.Id == SelectedTournament.Id) {
                    el.prepareInnerProperties();
                    SelectedTournament = el;
                }
            }
        }

        private void editMatch(MatchModel _m) {
            Application.Current.MainPage = new ChangeMatch();
            Application.Current.MainPage.BindingContext = new ChangeMatchViewModel(_m, SelectedTournament, db);
        }
        #endregion
        public TournamentPageViewModel(TournamentModel _t, Repository.Repository _db) {
            SelectedTournament = _t;
            db = _db;

            // If solo tournament without members default team, create it
            if (SelectedTournament.Team == false && SelectedTournament.Teams.Count == 0) {
                Task t;
                List<TournamentModel> list;
                
                t = db.PostTeamAsync(new PostTeamModel() {
                    TournamentModelId = SelectedTournament.Id,
                    Name = "Participanți",
                    Points = 0
                }).ContinueWith((finishedTask) => 
                    { updateTournamentPage(); });
            }
        }
    }
}
