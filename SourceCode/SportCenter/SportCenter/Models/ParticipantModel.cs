namespace SportCenter.Models
{
    public class ParticipantModel
    {
        public int Id { get; set; }
        public int TeamModelId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Mail { get; set; }
        public int Points { get; set; }
        public bool VisiblePoints { get; set; }
    }
}
