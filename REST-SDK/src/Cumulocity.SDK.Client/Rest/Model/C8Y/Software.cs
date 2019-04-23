using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Utils;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
    [PackageName("c8y_Software")]
    /// <summary>
    /// A map of software to their particular versions and builds installed or to be
    /// installed on a device. When used as operation, maps to URLs of the files. The
    /// last part of the URL is used as file name.
    /// </summary>
    [Obsolete]
    public class Software : Dictionary<string, string>
    {
        private const long serialVersionUID = 9094136944975438527L;
    }
}
