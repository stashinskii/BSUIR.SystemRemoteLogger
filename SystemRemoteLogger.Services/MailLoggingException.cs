using System;
using System.Collections.Generic;
using System.Text;

namespace SystemRemoteLogger.Services
{
    /// <summary>
    /// Represents custom mail logging exception for handeling specific errors
    /// </summary>
    public class MailLoggingException: Exception
    {
        public MailLoggingException(string message) : base (message)
        {

        }
    }
}
