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
using Cumulocity.SDK.Client.Rest.Model.Measurement;
using Cumulocity.SDK.Client.Rest.Utils;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
    [PackageName("c8y_SpeedMeasurement")]
    /// <summary>
    /// Provides a representation for a motion measurement, as reported by <seealso cref="MotionSensor"/>.
    /// See <a>https://code.telcoassetmarketplace.com/devcommunity/index.php/c8ydocumentation/114/320#Motion</a> for details.
    /// @author ricardomarques
    /// </summary>
    public class SpeedMeasurement 
    {

        public const string SPEED_UNITS = "m/s";

        public const string BEARING_UNITS = "degrees";

        private MeasurementValue motion;

        private MeasurementValue speed;

        private MeasurementValue bearing;

        public SpeedMeasurement()
        {
        }

        public SpeedMeasurement(MeasurementValue motion, MeasurementValue speed, MeasurementValue bearing)
        {
            this.motion = motion;
            this.speed = speed;
            this.bearing = bearing;
        }

        /// <summary>
        /// Motion will be true if motionDetected value is not 0
        /// @return
        /// </summary>
        //ORIGINAL LINE: @JSONProperty(ignore = true) public System.Nullable<bool> isMotionDetected()
        public virtual bool? MotionDetected
        {
            get
            {
                if (motion == null || motion.Value == null)
                {
                    return false;
                }
                return (motion.Value > 0);
            }
        }

        /// <returns> the motion, or null if no motion is set </returns>
        //ORIGINAL LINE: @JSONProperty(value = "motionDetected", ignoreIfNull = true) public MeasurementValue getMotion()
        public virtual MeasurementValue Motion
        {
            get
            {
                return motion;
            }
            set
            {
                this.motion = value;
            }
        }


        //ORIGINAL LINE: @JSONProperty(value = "speed", ignoreIfNull = true) public MeasurementValue getSpeed()
        public virtual MeasurementValue Speed
        {
            get
            {
                return speed;
            }
            set
            {
                this.speed = value;
            }
        }


        /// <returns> the bearing, or null if the bearing is not set </returns>
        //ORIGINAL LINE: @JSONProperty(value = "bearing", ignoreIfNull = true) public MeasurementValue getBearing()
        public virtual MeasurementValue Bearing
        {
            get
            {
                return bearing;
            }
            set
            {
                this.bearing = value;
            }
        }


        public override int GetHashCode()
        {
            int result = motion != null ? motion.GetHashCode() : 0;
            result = 31 * result + (speed != null ? speed.GetHashCode() : 0);
            result = 31 * result + (bearing != null ? bearing.GetHashCode() : 0);
            return result;
        }

        public override bool Equals(object o)
        {
            if (this == o)
            {
                return true;
            }
            if (!(o is SpeedMeasurement))
            {
                return false;
            }

            SpeedMeasurement that = (SpeedMeasurement)o;

            if (bearing != null ? !bearing.Equals(that.bearing) : that.bearing != null)
            {
                return false;
            }
            if (motion != null ? !motion.Equals(that.motion) : that.motion != null)
            {
                return false;
            }
            if (speed != null ? !speed.Equals(that.speed) : that.speed != null)
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            return "SpeedMeasurement{" +
                    "motion=" + motion +
                    ", speed=" + speed +
                    ", bearing=" + bearing +
                    '}';
        }
    }
}
