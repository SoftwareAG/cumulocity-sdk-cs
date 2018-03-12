using System;
using System.Collections.Generic;
using static Cumulocity.MQTT.Client;

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