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
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
    [PackageName("c8y_SinglePhaseEnergyMeasurement")]
    /// <summary>
    /// Provides a representation for a electricity measurement, as reported by <seealso cref="SinglePhaseElectricitySensor"/>.
    /// See <a>https://code.telcoassetmarketplace.com/devcommunity/index.php/c8ydocumentation/114/320#Energy</a> for details.
    /// @author ricardomarques
    /// 
    /// </summary>
    public class SinglePhaseEnergyMeasurement 
    {

        private MeasurementValue A_plus, A_minus;

        private MeasurementValue P_plus, P_minus;

        public SinglePhaseEnergyMeasurement()
        {
        }

        public SinglePhaseEnergyMeasurement(MeasurementValue a_plus, MeasurementValue a_minus, MeasurementValue p_plus, MeasurementValue p_minus)
        {
            A_plus = a_plus;
            A_minus = a_minus;
            P_plus = p_plus;
            P_minus = p_minus;
        }

        /// <returns> the a_plus </returns>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "A+")]
        public MeasurementValue TotalActiveEnergyIn
        {
            get
            {
                return A_plus;
            }
            set
            {
                A_plus = value;
            }
        }


        /// <returns> the a_minus </returns>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "A-")]
        public MeasurementValue TotalActiveEnergyOut
        {
            get
            {
                return A_minus;
            }
            set
            {
                A_minus = value;
            }
        }


        /// <returns> the p_plus </returns>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "P+")]
        public MeasurementValue TotalActivePowerIn
        {
            get
            {
                return P_plus;
            }
            set
            {
                P_plus = value;
            }
        }


        /// <returns> the p_minus </returns>
        //ORIGINAL LINE: @JSONProperty(value = "P-", ignoreIfNull = true) public final MeasurementValue getTotalActivePowerOut()
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "P-")]
        public MeasurementValue TotalActivePowerOut
        {
            get
            {
                return P_minus;
            }
            set
            {
                P_minus = value;
            }
        }


        public override int GetHashCode()
        {
            int result = A_plus != null ? A_plus.GetHashCode() : 0;
            result = 31 * result + (A_minus != null ? A_minus.GetHashCode() : 0);
            result = 31 * result + (P_plus != null ? P_plus.GetHashCode() : 0);
            result = 31 * result + (P_minus != null ? P_minus.GetHashCode() : 0);
            return result;
        }

        public override bool Equals(object o)
        {
            if (this == o)
            {
                return true;
            }
            if (!(o is SinglePhaseEnergyMeasurement))
            {
                return false;
            }

            SinglePhaseEnergyMeasurement that = (SinglePhaseEnergyMeasurement)o;

            if (A_minus != null ? !A_minus.Equals(that.A_minus) : that.A_minus != null)
            {
                return false;
            }
            if (A_plus != null ? !A_plus.Equals(that.A_plus) : that.A_plus != null)
            {
                return false;
            }
            if (P_minus != null ? !P_minus.Equals(that.P_minus) : that.P_minus != null)
            {
                return false;
            }
            if (P_plus != null ? !P_plus.Equals(that.P_plus) : that.P_plus != null)
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            return "SinglePhaseEnergyMeasurement{" +
                    "A_plus=" + A_plus +
                    ", A_minus=" + A_minus +
                    ", P_plus=" + P_plus +
                    ", P_minus=" + P_minus +
                    '}';
        }
    }
}
