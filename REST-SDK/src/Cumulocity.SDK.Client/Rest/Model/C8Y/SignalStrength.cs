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
    [PackageName("c8y_SignalStrength")]
    public class SignalStrength
    {
        public const string RSSI_UNIT = "dBm";
        public const string BER_UNIT = "%";

        public static readonly double[] BER_TABLE = new double[] { 0.14, 0.28, 0.57, 1.13, 2.26, 4.53, 9.05, 18.10 };

        private MeasurementValue rssi;
        private MeasurementValue ber;

        [JsonProperty("rssi")]
        public virtual MeasurementValue Rssi
        {
            get
            {
                return rssi;
            }
            set
            {
                this.rssi = value;
            }
        }

        [JsonIgnore]
        [JsonProperty("ber")]
        public virtual MeasurementValue Ber
        {
            get
            {
                return ber;
            }
            set
            {
                this.ber = value;
            }
        }

        [JsonIgnore]
        public virtual decimal? RssiValue
        {
            get
            {
                return rssi?.Value;
            }
            set
            {
                rssi = new MeasurementValue(RSSI_UNIT);
                rssi.Value = (decimal)value;
            }
        }


        public virtual void PutRawRssi(int rawRssi)
        {
            if (rawRssi != 99)
            {
                int rssiVal = -53 - (30 - rawRssi) * 2;
                RssiValue = new decimal(rssiVal);
            }
        }

        [JsonIgnore]
        public virtual decimal? BerValue
        {
            get => ber?.Value;
            set
            {
                ber = new MeasurementValue(BER_UNIT) { Value = (decimal)value };
            }
        }


        public virtual void PutRawBer(int rawBer)
        {
            if (rawBer != 99)
            {
                BerValue = new decimal(BER_TABLE[rawBer]);
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
            if (!(obj is SignalStrength))
            {
                return false;
            }

            SignalStrength rhs = (SignalStrength)obj;
            bool result = rssi?.Equals(rhs.rssi) ?? (rhs.rssi == null);
            result = result && (ber?.Equals(rhs.ber) ?? (rhs.ber == null));
            return result;
        }

        public override int GetHashCode()
        {
            int result = rssi == null ? 0 : rssi.GetHashCode();
            return result * 31 + (ber == null ? 0 : ber.GetHashCode());
        }
    }
}
