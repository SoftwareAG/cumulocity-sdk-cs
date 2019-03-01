#region Cumulocity GmbH

// /*
//  * Copyright (C) 2015-2018
//  *
//  * Permission is hereby granted, free of charge, to any person obtaining a copy of
//  * this software and associated documentation files (the "Software"),
//  * to deal in the Software without restriction, including without limitation the rights to use,
//  * copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
//  * and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//  *
//  * The above copyright notice and this permission notice shall be
//  * included in all copies or substantial portions of the Software.
//  *
//  * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
//  * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//  * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
//  * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
//  * ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//  */

#endregion

using System;
using System.Collections.Generic;

namespace Cumulocity.MQTT
{
    public class RestartEventArgs : EventArgs
    {
    }

    public class CommandEventArgs : EventArgs
    {
        public string CommandText { get; set; }
    }

    public class ConfigurationEventArgs : EventArgs
    {
        public string Configuration { get; set; }
    }

    public class FirmwareEventArgs : EventArgs
    {
        public string FirmwareName { get; set; }
        public string FirmwareVersion { get; set; }
        public string Url { get; set; }
    }

    public class SoftwareListEventArgs : EventArgs
    {
        public IList<Software> SoftwareList { get; set; }
    }

    public class MeasurementRequestOperationEventArgs : EventArgs
    {
        public string RequestName { get; set; }
    }

    public class RelayEventArgs : EventArgs
    {
        public string RelayState { get; set; }
    }

    public class RelayArrayEventArgs : EventArgs
    {
        public IList<string> RelayStates { get; set; }
    }

    public class UploadConfigurationFileEventArgs : EventArgs
    {
    }

    public class DownloadConfigurationFileEventArgs : EventArgs
    {
        public string Url { get; set; }
    }

    public class LogfileRequestEventArgs : EventArgs
    {
        public string LogFileName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string SearchText { get; set; }
        public string MaximumLines { get; set; }
    }

    public class CommunicationModeEventArgs : EventArgs
    {
        public string Mode { get; set; }
    }

    public class ChildrenOfDeviceEventArgs : EventArgs
    {
        public IList<string> ChildrenOfDevice { get; set; }
    }

    public class TemplateCollectionEventArgs : EventArgs
    {
        public string TemplateCollectionName { get; set; }
        public bool IsExist { get; set; }
        public int IdCollection { get; set; }
    }

    public class DeviceCredentialsEventArgs : EventArgs
    {
        public string Tenant { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class SmartRestResponseEventArgs : EventArgs
    {
        public string Topic { get; set; }
        public List<SmartRestResponse> Responses { get; set; }
    }

    public class SmartRestResponse
    {
        public string MessageID { get; set; }
        public List<string> Fields { get; set; }
    }

    public class ErrorMessageEventArgs : EventArgs
    {
        public string Message { get; set; }
    }
}