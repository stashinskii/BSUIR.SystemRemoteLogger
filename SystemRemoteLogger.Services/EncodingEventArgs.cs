using System;
using System.Collections.Generic;
using System.Text;

namespace SystemRemoteLogger.Services
{
    public class EncodingEventArgs : EventArgs
    {
        public string data;
        public EncodingEventArgs() : base()
        {
        }
    }
}
