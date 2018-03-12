using System;
using System.Threading.Tasks;

namespace Cumulocity.MQTT.Interfaces
{
    internal interface IClient
    {
        bool IsConnected { get; }

        Task<bool> ConnectAsync();

        Task<bool> DisconnectAsync();

        //Restart (510)
        event EventHandler<RestartEventArgs> RestartEvt;

        //Command (511)
        event EventHandler<CommandEventArgs> CommandEvt;

        //Configuration (513)
        event EventHandler<ConfigurationEventArgs> ConfigurationEvt;

        //Firmware (515)
        event EventHandler<FirmwareEventArgs> FirmwareEvt;

        //Software list (516)
        event EventHandler<SoftwareListEventArgs> SoftwareListEvt;

        //Measurement request operation (517)
        event EventHandler<MeasurementRequestOperationEventArgs> MeasurementRequestOperationEvt;

        //Relay (518)
        event EventHandler<RelayEventArgs> RelayEvt;

        //RelayArray (519)
        event EventHandler<RelayArrayEventArgs> RelayArrayEvt;

        //Upload configuration file (520)
        event EventHandler<UploadConfigurationFileEventArgs> UploadConfigurationFileEvt;

        //Download configuration file (521)
        event EventHandler<DownloadConfigurationFileEventArgs> DownloadConfigurationFileEvt;

        //Logfile request (522)
        event EventHandler<LogfileRequestEventArgs> LogfileRequestEvt;

        //Communication mode (523)
        event EventHandler<CommunicationModeEventArgs> CommunicationModeEvt;

        //Get children of device (106)
        event EventHandler<ChildrenOfDeviceEventArgs> ChildrenOfDeviceEvt;
    }
}