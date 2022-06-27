namespace SmartCharging.DataAccess.Entities
{
    public class ConnectorEntity
    {
        public int Id { get; set; }

        public Guid ChargeStationId { get; set; }

        public double MaxCurrentInAmps { get; set; }
    }
}