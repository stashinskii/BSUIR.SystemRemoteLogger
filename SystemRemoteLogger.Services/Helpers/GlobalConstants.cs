using System;
using System.Collections.Generic;
using System.Text;

namespace SystemRemoteLogger.Services.Helpers
{
    public static class GlobalConstants
    {
        public static string Signature = @"<p><strong><span style=""font-family: Arial, sans-serif; font-size: 11pt; color: #39c2d7; text-transform: uppercase;"" data-bind=""text: name"">|USERNAME_TOKEN|</span></strong> <br /> <strong><span style=""font-family: Arial, sans-serif; font-size: 10pt; color: #464547;"" data-bind=""text: jobTitle"">Computer Administrator</span></strong> <br /> <br /> <span style=""font-family: Arial, sans-serif; font-size: 8pt; color: #999999;""> CONFIDENTIALITY CAUTION AND DISCLAIMER<br /> This message was created and send automatically by System Remote Logger (SRL) API. Please, do not reply. </span></p>";
        public static string UsernameTokenPattern = "|USERNAME_TOKEN|";
    }
}
