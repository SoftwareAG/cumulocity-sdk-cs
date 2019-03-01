using Cumulocity.SDK.Client.Rest.Utils;

namespace Cumulocity.SDK.Client.IntegrationTest.Inventory
{
    [PackageName("c8y_Coordinate")]
    public class Coordinate
    {

        private double? longitude;

        private double? latitude;

        public Coordinate() : this(100.0, 101.0)
        {
        }

        public Coordinate(double latitude, double longitude)
        {
            this.longitude = longitude;
            this.latitude = latitude;
        }

        public virtual double? Latitude
        {
            set
            {
                this.latitude = value;
            }
            get
            {
                return latitude;
            }
        }

        public virtual double? Longitude
        {
            set
            {
                this.longitude = value;
            }
            get
            {
                return longitude;
            }
        }

        public override bool Equals(object o)
        {
            if (this == o)
            {
                return true;
            }
            if (o == null || this.GetType() != o.GetType())
            {
                return false;
            }

            Coordinate that = (Coordinate) o;

            if (latitude != null ?!latitude.Equals(that.latitude) : that.latitude != null)
            {
                return false;
            }
            if (longitude != null ?!longitude.Equals(that.longitude) : that.longitude != null)
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            int result = longitude != null ? longitude.GetHashCode() : 0;
            result = 31 * result + (latitude != null ? latitude.GetHashCode() : 0);
            return result;
        }


    }
}