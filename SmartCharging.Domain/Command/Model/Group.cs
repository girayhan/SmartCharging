using SmartCharging.Domain.Exceptions;

namespace SmartCharging.Domain.Command.Model
{
    public class Group
    {
        private Guid id;
        public Guid Id
        {
            get { return id; }
            private set { id = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        private double capacityInAmps;
        public double CapacityInAmps
        {
            get
            {
                return capacityInAmps;
            }

            private set
            {
                if (value < 0)
                {
                    throw new InvalidCapacityInAmpsException();
                }

                capacityInAmps = value;
            }
        }

        public Group(Guid id, string name, double capacityInAmps)
        {
            Id = id;
            Name = name;
            CapacityInAmps = capacityInAmps;
        }
    }
}