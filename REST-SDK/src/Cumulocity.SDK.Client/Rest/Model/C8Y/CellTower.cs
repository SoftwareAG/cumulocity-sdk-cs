using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
    [PackageName("c8y_CellTower")]
    public class CellTower
    {

        private string radioType;
        private int mobileCountryCode;
        private int mobileNetworkCode;
        private int locationAreaCode;
        private int cellId;
        private int timingAdvance;
        private int signalStrength;
        private int primaryScramblingCode;
        private int serving;

        [JsonProperty(PropertyName = "radioType")]
        public virtual string RadioType
        {
            get
            {
                return radioType;
            }
            set
            {
                this.radioType = value;
            }
        }
        [JsonProperty(PropertyName = "mobileCountryCode")]
        public virtual int MobileCountryCode
        {
            get
            {
                return mobileCountryCode;
            }
            set
            {
                this.mobileCountryCode = value;
            }
        }
        [JsonProperty(PropertyName = "mobileNetworkCode")]
        public virtual int MobileNetworkCode
        {
            get
            {
                return mobileNetworkCode;
            }
            set
            {
                this.mobileNetworkCode = value;
            }
        }
        [JsonProperty(PropertyName = "locationAreaCode")]
        public virtual int LocationAreaCode
        {
            get
            {
                return locationAreaCode;
            }
            set
            {
                this.locationAreaCode = value;
            }
        }
        [JsonProperty(PropertyName = "cellId")]
        public virtual int CellId
        {
            get
            {
                return cellId;
            }
            set
            {
                this.cellId = value;
            }
        }
        [JsonProperty(PropertyName = "timingAdvance")]
        public virtual int TimingAdvance
        {
            get
            {
                return timingAdvance;
            }
            set
            {
                this.timingAdvance = value;
            }
        }
        [JsonProperty(PropertyName = "signalStrength")]
        public virtual int SignalStrength
        {
            get
            {
                return signalStrength;
            }
            set
            {
                this.signalStrength = value;
            }
        }
        [JsonProperty(PropertyName = "primaryScramblingCode")]
        public virtual int PrimaryScramblingCode
        {
            get
            {
                return primaryScramblingCode;
            }
            set
            {
                this.primaryScramblingCode = value;
            }
        }
        [JsonProperty(PropertyName = "serving")]
        public virtual int Serving
        {
            get
            {
                return serving;
            }
            set
            {
                this.serving = value;
            }
        }
        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;
            result = prime * result + cellId;
            result = prime * result + locationAreaCode;
            result = prime * result + mobileCountryCode;
            result = prime * result + mobileNetworkCode;
            result = prime * result + primaryScramblingCode;
            result = prime * result + ((string.ReferenceEquals(radioType, null)) ? 0 : radioType.GetHashCode());
            result = prime * result + serving;
            result = prime * result + signalStrength;
            result = prime * result + timingAdvance;
            return result;
        }
        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (obj == null)
            {
                return false;
            }
            if (this.GetType() != obj.GetType())
            {
                return false;
            }
            CellTower other = (CellTower)obj;
            if (cellId != other.cellId)
            {
                return false;
            }
            if (locationAreaCode != other.locationAreaCode)
            {
                return false;
            }
            if (mobileCountryCode != other.mobileCountryCode)
            {
                return false;
            }
            if (mobileNetworkCode != other.mobileNetworkCode)
            {
                return false;
            }
            if (primaryScramblingCode != other.primaryScramblingCode)
            {
                return false;
            }
            if (string.ReferenceEquals(radioType, null))
            {
                if (!string.ReferenceEquals(other.radioType, null))
                {
                    return false;
                }
            }
            else if (!radioType.Equals(other.radioType))
            {
                return false;
            }
            if (serving != other.serving)
            {
                return false;
            }
            if (signalStrength != other.signalStrength)
            {
                return false;
            }
            if (timingAdvance != other.timingAdvance)
            {
                return false;
            }
            return true;
        }
    }
}
