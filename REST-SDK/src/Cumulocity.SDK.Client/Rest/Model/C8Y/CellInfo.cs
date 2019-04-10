using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
    [PackageName("c8y_CellInfo")]
    public class CellInfo
    {
        private string radioType;
        private IList<CellTower> cellTowers;

        public CellInfo()
        {

        }

        public CellInfo(string radioType, IList<CellTower> cellTowers)
        {
            this.radioType = radioType;
            this.cellTowers = cellTowers;
        }
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

        [JsonProperty(PropertyName = "cellTowers")]
        public virtual IList<CellTower> CellTowers
        {
            get
            {
                return cellTowers;
            }
            set
            {
                this.cellTowers = value;
            }
        }


        public override string ToString()
        {
            return "CellInfo{" +
                   "radioType='" + radioType + '\'' +
                   ", cellTowers=" + cellTowers +
                   '}';
        }

        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;
            result = prime * result + ((cellTowers == null) ? 0 : cellTowers.GetHashCode());
            result = prime * result + ((string.ReferenceEquals(radioType, null)) ? 0 : radioType.GetHashCode());
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
            CellInfo other = (CellInfo)obj;
            if (cellTowers == null)
            {
                if (other.cellTowers != null)
                {
                    return false;
                }
            }

            //ORIGINAL LINE: else if (!cellTowers.equals(other.cellTowers))
            else if (!cellTowers.Equals(other.cellTowers))
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
            return true;
        }

    }
}
