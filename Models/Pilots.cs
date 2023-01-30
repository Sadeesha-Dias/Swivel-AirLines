namespace Swivel_AirLines.Models
{
    public class Pilots
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PilotLisenceNumber { get; set; } = string.Empty;
        public int Status { get; set; }
        public int FlyingHoures { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
