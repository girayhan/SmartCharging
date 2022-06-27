using SmartCharging.Domain.Exceptions;

namespace SmartCharging.Domain.Command.Model
{
    public class Connector
    {
        private int id;
        public int Id
        {
            get { return id; }
            private set { id = value; }
        }

        private Guid chargeStationId;
        public Guid ChargeStationId
        {
            get { return chargeStationId; }
            private set { chargeStationId = value; }
        }

        private double maxCurrentInAmps;
        public double MaxCurrentInAmps
        {
            get { return maxCurrentInAmps; }
            private set
            {
                if (value < 0)
                {
                    throw new InvalidMaxCurrentInAmpsException();
                }

                maxCurrentInAmps = value;
            }
        }

        public Connector(int id, Guid chargeStationId, double maxCurrentInAmps)
        {
            Id = id;
            ChargeStationId = chargeStationId;
            MaxCurrentInAmps = maxCurrentInAmps;
        }
    }
}