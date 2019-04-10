using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
    [PackageName("c8y_Command")]
    public class Command 
    {

        private const long serialVersionUID = -6443811928706492241L;

        private string text;
        private string syntax;
        private string result;

        public Command()
        {
        }

        public Command(string text)
        {
            this.text = text;
        }
        [JsonProperty(PropertyName = "text")]
        public virtual string Text
        {
            get
            {
                return text;
            }
            set
            {
                this.text = value;
            }
        }

        [JsonProperty(PropertyName = "syntax")]
        public virtual string Syntax
        {
            get
            {
                return syntax;
            }
            set
            {
                this.syntax = value;
            }
        }


        [JsonProperty(PropertyName = "result")]
        public virtual string Result
        {
            get
            {
                return result;
            }
            set
            {
                this.result = value;
            }
        }


        public override string ToString()
        {
            return string.Format("Command [text={0}]", text);
        }

    }
}
