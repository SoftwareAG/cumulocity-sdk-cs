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
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Utils;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
    [PackageName("c8y_SsidInformation")]
    public class SsidInformation 
    {
        private const string MAC_ADDRESS_KEY = "macAddress";
        private const string SSID_KEY = "ssid";
        private const string SIGNAL_STRENGTH_KEY = "signalStrength";


        private string macAddress;
        private string ssid;
        private int? signalStrength;

        public SsidInformation()
        {
        }

        public SsidInformation(string macAddress, string ssid, int? signalStrength)
        {
            this.macAddress = macAddress;
            this.ssid = ssid;
            this.signalStrength = signalStrength;
        }

        public SsidInformation(IDictionary<string, object> map)
        {
            foreach (string key in map.Keys)
            {
                object value = map[key];
                if (value == null)
                {
                    continue;
                }
                switch (key)
                {
                    case MAC_ADDRESS_KEY:
                        this.macAddress = value.ToString();
                        continue;
                    case SSID_KEY:
                        this.ssid = value.ToString();
                        continue;
                    case SIGNAL_STRENGTH_KEY:
                        if (value is int)
                        {
                            this.signalStrength = ((int) value);
                        }
                        continue;
                    default:
                        //SetProperty(key, value);
                        break;
                }
            }

            object macAddress = map["macAddress"];
            if (macAddress != null)
            {
                this.macAddress = macAddress.ToString();
            }
            object ssid = map["ssid"];
            if (ssid != null)
            {
                this.ssid = ssid.ToString();
            }
            object signalStrength = map["signalStrength"];
            if (signalStrength != null)
            {
                if (signalStrength is int)
                {
                    this.signalStrength = ((int)signalStrength);
                }
                else
                {
                    this.signalStrength = Convert.ToInt32(signalStrength.ToString());
                }
            }
        }

        public virtual string MacAddress
        {
            get
            {
                return macAddress;
            }
            set
            {
                this.macAddress = value;
            }
        }


        public virtual string Ssid
        {
            get
            {
                return ssid;
            }
            set
            {
                this.ssid = value;
            }
        }


        public virtual int? SignalStrength
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


        public override string ToString()
        {
            return "SsidInformation{" +
                    "macAddress='" + macAddress + '\'' +
                    ", ssid='" + ssid + '\'' +
                    ", signalStrength=" + signalStrength +
                    '}';
        }

    }
}
