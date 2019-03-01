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