namespace Swivel_AirLines.DTO.OutgoingDTO
{
    public class PilotOutgoingDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PilotLisenceNumber { get; set; } = string.Empty;
        public int FlyingHours { get; set; }
    }
}
