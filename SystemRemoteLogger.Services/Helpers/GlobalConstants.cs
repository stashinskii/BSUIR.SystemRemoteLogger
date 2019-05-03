using System;
using System.Collections.Generic;
using System.Text;

namespace SystemRemoteLogger.Services.Helpers
{
    /// <summary>
    /// Represents global constants for configuring SRL 
    /// </summary>
    public static class GlobalConstants
    { 
        public static string UsernameTokenPattern = "|USERNAME_TOKEN|";
        public static string MessageTokenPattern = "|MESSAGE|";
        public static string SignatureTokenPattern = "|SIGNATURE|";
    }
}
