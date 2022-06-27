namespace SmartCharging.Domain.Query.DTOs
{
    public class ChargeStationDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid GroupId { get; set; }

        public List<ConnectorDto> Connectors { get; set; }
    }
}