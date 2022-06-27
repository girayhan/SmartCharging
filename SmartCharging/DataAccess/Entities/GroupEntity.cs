namespace SmartCharging.DataAccess.Entities
{
    public class GroupEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double CapacityInAmps { get; set; }
    }
}