using System;
using System.Collections.Generic;
using System.Text;

namespace SystemRemoteLogger.Services
{
    /// <summary>
    /// Represents custom event arguments for encoding while processing UDP datagramm data
    /// </summary>
    public class EncodingEventArgs : EventArgs
    {
        /// <summary>
        /// Represents string field containing message
        /// </summary>
        public string data;
        public EncodingEventArgs() : base()
        {
        }
    }
}
