using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
    [PackageName("c8y_Geofence")]
    public class Geofence 
    {
        private decimal lat;
        private decimal lng;
        private decimal radius;
        private bool active;

        [JsonProperty(PropertyName = "Lat")]
        public virtual decimal Lat
        {
            get
            {
                return lat;
            }
            set
            {
                this.lat = value;
            }
        }

        [JsonProperty(PropertyName = "Lng")]
        public virtual decimal Lng
        {
            get
            {
                return lng;
            }
            set
            {
                this.lng = value;
            }
        }

        [JsonProperty(PropertyName = "Radius")]
        public virtual decimal Radius
        {
            get
            {
                return radius;
            }
            set
            {
                this.radius = value;
            }
        }

        [JsonProperty(PropertyName = "Active")]
        public virtual bool Active
        {
            get
            {
                return active;
            }
            set
            {
                this.active = value;
            }
        }


        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj == this)
            {
                return true;
            }
            if (!(obj is Geofence))
            {
                return false;
            }

            Geofence rhs = (Geofence)obj;
            bool result = (lat == null) ? (rhs.lat == null) : lat.Equals(rhs.lat);
            result = result && ((lng == null) ? (rhs.lng == null) : lng.Equals(rhs.lng));
            result = result && ((radius == null) ? (rhs.radius == null) : radius.Equals(rhs.radius));
            result = result && (active == rhs.active);
            return result;
        }

        public override int GetHashCode()
        {
            int result = lat == null ? 0 : lat.GetHashCode();
            result = 31 * result + (lng == null ? 0 : lng.GetHashCode());
            result = 31 * result + (radius == null ? 0 : radius.GetHashCode());
            result = 31 * result + (active ? 1 : 0);
            return result;
        }
    }

}
