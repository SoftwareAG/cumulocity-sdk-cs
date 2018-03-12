using Cumulocity.MQTT.Enums;
using System;

namespace Cumulocity.MQTT.Model
{
    public abstract class Request
    {
        protected readonly string _messageId;
        protected readonly ValidApis _api;
        protected readonly HttpMethods _httpMethods;

        protected Request(string messageId, ValidApis api, HttpMethods httpMethods)
        {
            _messageId = messageId;
            _api = api;
            _httpMethods = httpMethods;
        }

        public virtual string RequestTemplate()
        {
            return String.Empty;
        }
    }
}