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

using System;
using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
    [PackageName("c8y_LogfileRequest")]
    [Serializable]
    public class LogfileRequest
    {

        private const long serialVersionUID = -6443811928706492241L;

        private DateTime dateFrom;
        private DateTime dateTo;
        private int maximumLines = 0;
        private string file;
        private string logFile;
        private string searchText;
        private string tenant;
        private string deviceUser;

        public LogfileRequest()
        {
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "dateFrom")]
        public virtual DateTime DateFrom
        {
            get
            {
                return dateFrom;
            }
            set
            {
                this.dateFrom = value;
            }
        }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "dateTo")]
        public virtual DateTime DateTo
        {
            get
            {
                return dateTo;
            }
            set
            {
                this.dateTo = value;
            }
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "maximumLines")]
        public virtual int MaximumLines
        {
            get
            {
                return maximumLines;
            }
            set
            {
                this.maximumLines = value;
            }
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "logFile")]
        public virtual string LogFile
        {
            get
            {
                return logFile;
            }
            set
            {
                this.logFile = value;
            }
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "file")]
        public virtual string File
        {
            get
            {
                return file;
            }
            set
            {
                this.file = value;
            }
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "searchText")]
        public virtual string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                this.searchText = value;
            }
        }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "tenant")]
        public virtual string Tenant
        {
            get
            {
                return tenant;
            }
            set
            {
                this.tenant = value;
            }
        }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "deviceUser")]
        public virtual string DeviceUser
        {
            get
            {
                return deviceUser;
            }
            set
            {
                this.deviceUser = value;
            }
        }


        public static long Serialversionuid
        {
            get
            {
                return serialVersionUID;
            }
        }

        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;
            result = prime * result + ((dateFrom == null) ? 0 : dateFrom.GetHashCode());
            result = prime * result + ((dateTo == null) ? 0 : dateTo.GetHashCode());
            result = prime * result + ((string.ReferenceEquals(deviceUser, null)) ? 0 : deviceUser.GetHashCode());
            result = prime * result + ((string.ReferenceEquals(file, null)) ? 0 : file.GetHashCode());
            result = prime * result + ((string.ReferenceEquals(logFile, null)) ? 0 : logFile.GetHashCode());
            result = prime * result + maximumLines;
            result = prime * result + ((string.ReferenceEquals(searchText, null)) ? 0 : searchText.GetHashCode());
            result = prime * result + ((string.ReferenceEquals(tenant, null)) ? 0 : tenant.GetHashCode());
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
            LogfileRequest other = (LogfileRequest)obj;
            if (dateFrom == null)
            {
                if (other.dateFrom != null)
                {
                    return false;
                }
            }
            else if (!dateFrom.Equals(other.dateFrom))
            {
                return false;
            }
            if (dateTo == null)
            {
                if (other.dateTo != null)
                {
                    return false;
                }
            }
            else if (!dateTo.Equals(other.dateTo))
            {
                return false;
            }
            if (string.ReferenceEquals(deviceUser, null))
            {
                if (!string.ReferenceEquals(other.deviceUser, null))
                {
                    return false;
                }
            }
            else if (!deviceUser.Equals(other.deviceUser))
            {
                return false;
            }
            if (string.ReferenceEquals(file, null))
            {
                if (!string.ReferenceEquals(other.file, null))
                {
                    return false;
                }
            }
            else if (!file.Equals(other.file))
            {
                return false;
            }
            if (string.ReferenceEquals(logFile, null))
            {
                if (!string.ReferenceEquals(other.logFile, null))
                {
                    return false;
                }
            }
            else if (!logFile.Equals(other.logFile))
            {
                return false;
            }
            if (maximumLines != other.maximumLines)
            {
                return false;
            }
            if (string.ReferenceEquals(searchText, null))
            {
                if (!string.ReferenceEquals(other.searchText, null))
                {
                    return false;
                }
            }
            else if (!searchText.Equals(other.searchText))
            {
                return false;
            }
            if (string.ReferenceEquals(tenant, null))
            {
                if (!string.ReferenceEquals(other.tenant, null))
                {
                    return false;
                }
            }
            else if (!tenant.Equals(other.tenant))
            {
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            return "LogfileRequest [dateFrom=" + dateFrom + ", dateTo=" + dateTo + ", maximumLines=" + maximumLines + ", file=" + file + ", logFile=" + logFile + ", searchText=" + searchText + ", tenant=" + tenant + ", deviceUser=" + deviceUser + "]";
        }

    }
}
