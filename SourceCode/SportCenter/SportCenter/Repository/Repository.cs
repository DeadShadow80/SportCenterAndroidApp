using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SportCenter.Models;

namespace SportCenter.Repository
{
    class Repository
    {
        HttpClient client;
        public Repository()
        {
            client = new HttpClient();
        }
        public async Task<List<TournamentModel>> GetTournaments()
        {
            List<TournamentModel> tournamentModel = new List<TournamentModel>();
            string link = <enter your api ip here>"https://REDACTED/api/Central/GetTournaments";
            Uri uri = new Uri(string.Format(link, string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                tournamentModel = JsonConvert.DeserializeObject<List<TournamentModel>>(content);
            }
            return tournamentModel;
        }
        public async Task<int> PostParticipantAsync(PostParticipantModel participantModel)
        {
            int ok = 0;
            string jsonString = System.Text.Json.JsonSerializer.Serialize(participantModel);
            string link = <enter your api ip here>"https://REDACTED/api/Central/PostParticipant";
            Uri uri = new Uri(String.Format(link, string.Empty));
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
                ok = 1;
            return ok;
        }
        public async Task<int> PostTeamAsync(PostTeamModel teamModel)
        {
            int ok = 0;
            string jsonString = System.Text.Json.JsonSerializer.Serialize(teamModel);
            string link = <enter your api ip here>"https://REDACTED/api/Central/PostTeam";
            Uri uri = new Uri(String.Format(link, string.Empty));
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
                ok = 1;
            return ok;
        }
        public async Task<int> PostMatchAsync(PostMatchModel matchModel)
        {
            int ok = 0;
            string jsonString = System.Text.Json.JsonSerializer.Serialize(matchModel);
            string link = <enter your api ip here>"https://REDACTED/api/Central/PostMatch";
            Uri uri = new Uri(String.Format(link, string.Empty));
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
                ok = 1;
            return ok;
        }
        public async Task<int> PostTournamentAsync(PostTournament tournamentModel)
        {
            int ok = 0;
            string jsonString = System.Text.Json.JsonSerializer.Serialize(tournamentModel);
            string link = <enter your api ip here>"https://REDACTED/api/Central/PostTournament";
            Uri uri = new Uri(String.Format(link, string.Empty));
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
                ok = 1;
            return ok;
        }
        public async Task<int> DeleteParticipantAsync(PostParticipantModel participantModel)
        {
            int ok = 0;
            string jsonString = System.Text.Json.JsonSerializer.Serialize(participantModel);
            string link = <enter your api ip here>"https://REDACTED/api/Central/DeleteParticipant";
            Uri uri = new Uri(String.Format(link, string.Empty));
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
                ok = 1;
            return ok;
        }
        public async Task UpdateMatchAsync (PostMatchModel matchModel)
        {
            string jsonString = System.Text.Json.JsonSerializer.Serialize(matchModel);
            string link = <enter your api ip here>"https://REDACTED/api/Central/UpdateMatch";
            Uri uri = new Uri(String.Format(link, string.Empty));
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content);

        }
        public async Task<int> UpdateParticipantAsync (PostParticipantModel participantModel)
        {
            int ok = 0;
            string jsonString = System.Text.Json.JsonSerializer.Serialize(participantModel);
            string link = <enter your api ip here>"https://REDACTED/api/Central/UpdateParticipant";
            Uri uri = new Uri(String.Format(link, string.Empty));
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
                ok = 1;
            return ok;
        }
        public async Task<int> UpdateTeamAsync(PostTeamModel teamModel)
        {
            int ok = 0;
            string jsonString = System.Text.Json.JsonSerializer.Serialize(teamModel);
            string link = <enter your api ip here>"https://REDACTED/api/Central/UpdateTeam";
            Uri uri = new Uri(String.Format(link, string.Empty));
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
                ok = 1;
            return ok;
        }
    }
}
