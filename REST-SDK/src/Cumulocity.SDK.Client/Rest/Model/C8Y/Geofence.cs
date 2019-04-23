/*
 * Copyright (C) 2019 Cumulocity GmbH
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of 
 * this software and associated documentation files (the "Software"),
 * to deal in the Software without restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
 * and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
 * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
 * ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
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
