namespace SmartCharging.Domain.Query.DTOs
{
    public class GroupDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double CapacityInAmps { get; set; }

        public List<ChargeStationDto> ChargeStations { get; set; }
    }
}