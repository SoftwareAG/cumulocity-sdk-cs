using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
    [PackageName("c8y_Mobile")]
    public class Mobile
    {
        private string cellId;
        private string iccid;

        private string imei;
        private string imsi;
        private string lac;
        private string mcc;
        private string mnc;
        private string msisdn;

        public Mobile()
        {
        }

        public Mobile(string imei, string cellId, string iccid)
        {
            this.imei = imei;
            this.cellId = cellId;
            this.iccid = iccid;
        }

        public Mobile(string imei, string cellId, string iccid, string mcc, string mnc, string imsi, string lac,
            string msisdn)
        {
            this.imei = imei;
            this.cellId = cellId;
            this.iccid = iccid;
            this.mcc = mcc;
            this.mnc = mnc;
            this.imsi = imsi;
            this.lac = lac;
            this.msisdn = msisdn;
        }
        [JsonProperty("imei")]
        public virtual string Imei
        {
            get => imei;
            set => imei = value;
        }
        [JsonProperty("cellId")]
        public virtual string CellId
        {
            get => cellId;
            set => cellId = value;
        }
        [JsonProperty("iccid")]
        public virtual string Iccid
        {
            get => iccid;
            set => iccid = value;
        }

        [JsonProperty("mcc")]
        public virtual string Mcc
        {
            get => mcc;
            set => mcc = value;
        }

        [JsonProperty("mnc")]
        public virtual string Mnc
        {
            get => mnc;
            set => mnc = value;
        }

        [JsonProperty("imsi")]
        public virtual string Imsi
        {
            get => imsi;
            set => imsi = value;
        }

        [JsonProperty("lac")]
        public virtual string Lac
        {
            get => lac;
            set => lac = value;
        }

        [JsonProperty("msisdn")]
        public virtual string Msisdn
        {
            get => msisdn;
            set => msisdn = value;
        }


        public override string ToString()
        {
            return "Mobile{" +
                   "imei='" + imei + '\'' +
                   ", cellId='" + cellId + '\'' +
                   ", iccid='" + iccid + '\'' +
                   ", mcc='" + mcc + '\'' +
                   ", mnc='" + mnc + '\'' +
                   ", imsi='" + imsi + '\'' +
                   ", lac='" + lac + '\'' +
                   ", msisdn='" + msisdn + '\'' +
                   '}';
        }

        public override int GetHashCode()
        {
            const int prime = 31;
            var result = 1;
            result = prime * result + (ReferenceEquals(cellId, null) ? 0 : cellId.GetHashCode());
            result = prime * result + (ReferenceEquals(iccid, null) ? 0 : iccid.GetHashCode());
            result = prime * result + (ReferenceEquals(imei, null) ? 0 : imei.GetHashCode());
            result = prime * result + (ReferenceEquals(imsi, null) ? 0 : imsi.GetHashCode());
            result = prime * result + (ReferenceEquals(lac, null) ? 0 : lac.GetHashCode());
            result = prime * result + (ReferenceEquals(mcc, null) ? 0 : mcc.GetHashCode());
            result = prime * result + (ReferenceEquals(mnc, null) ? 0 : mnc.GetHashCode());
            result = prime * result + (ReferenceEquals(msisdn, null) ? 0 : msisdn.GetHashCode());
            return result;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;
            var other = (Mobile) obj;
            if (ReferenceEquals(cellId, null))
            {
                if (!ReferenceEquals(other.cellId, null)) return false;
            }
            else if (!cellId.Equals(other.cellId))
            {
                return false;
            }

            if (ReferenceEquals(iccid, null))
            {
                if (!ReferenceEquals(other.iccid, null)) return false;
            }
            else if (!iccid.Equals(other.iccid))
            {
                return false;
            }

            if (ReferenceEquals(imei, null))
            {
                if (!ReferenceEquals(other.imei, null)) return false;
            }
            else if (!imei.Equals(other.imei))
            {
                return false;
            }

            if (ReferenceEquals(imsi, null))
            {
                if (!ReferenceEquals(other.imsi, null)) return false;
            }
            else if (!imsi.Equals(other.imsi))
            {
                return false;
            }

            if (ReferenceEquals(lac, null))
            {
                if (!ReferenceEquals(other.lac, null)) return false;
            }
            else if (!lac.Equals(other.lac))
            {
                return false;
            }

            if (ReferenceEquals(mcc, null))
            {
                if (!ReferenceEquals(other.mcc, null)) return false;
            }
            else if (!mcc.Equals(other.mcc))
            {
                return false;
            }

            if (ReferenceEquals(mnc, null))
            {
                if (!ReferenceEquals(other.mnc, null)) return false;
            }
            else if (!mnc.Equals(other.mnc))
            {
                return false;
            }

            if (ReferenceEquals(msisdn, null))
            {
                if (!ReferenceEquals(other.msisdn, null)) return false;
            }
            else if (!msisdn.Equals(other.msisdn))
            {
                return false;
            }

            return true;
        }
    }
}