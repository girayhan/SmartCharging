namespace SmartCharging.Domain.Query.DTOs
{
    public class ConnectorDto
    {
        public int ConnectorId { get; set; }

        public Guid ChargeStationId { get; set; }

        public double MaxCurrentInAmps { get; set; }
    }
}