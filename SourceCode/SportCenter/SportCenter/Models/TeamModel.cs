using System.Collections.Generic;

namespace SportCenter.Models
{
    public class TeamModel
    {
        public int Id { get; set; }
        public int TournamentModelId { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public bool VisiblePoints { get; set; }
        public List<ParticipantModel> Participants { get; set; }
    }
}
