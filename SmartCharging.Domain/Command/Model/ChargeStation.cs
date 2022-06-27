namespace SmartCharging.Domain.Command.Model
{
    public class ChargeStation
    {
        private Guid id;
        public Guid Id
        {
            get { return id; }
            private set { id = value; }
        }

        private Guid groupId;
        public Guid GroupId
        {
            get { return groupId; }
            private set { groupId = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        private List<Connector> connectors;
        public List<Connector> Connectors
        {
            get { return connectors; }
            private set { connectors = value; }
        }

        public ChargeStation(Guid id, string name, List<Connector> connectors)
        {
            Id = id;
            Name = name;
            Connectors = connectors;
        }
    }
}