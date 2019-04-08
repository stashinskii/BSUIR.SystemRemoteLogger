using System;
using System.Collections.Generic;
using System.Text;

namespace SystemRemoteLogger.Services
{
    public class MailLoggingException: Exception
    {
        public MailLoggingException(string message) : base (message)
        {

        }
    }
}
