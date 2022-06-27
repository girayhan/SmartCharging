namespace SmartCharging.DataAccess.Entities
{
    public class ChargeStationEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid GroupId { get; set; }
    }
}