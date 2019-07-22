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
using System.Collections.Generic;
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
