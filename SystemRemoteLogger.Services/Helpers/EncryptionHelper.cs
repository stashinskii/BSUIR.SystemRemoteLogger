using System;
using System.Collections.Generic;
using System.Text;

namespace SystemRemoteLogger.Services.Helpers
{
    internal static class EncryptionHelper
    {
        internal static byte[] Encode(string data) => Encoding.UTF8.GetBytes(data);
        internal static string Decode(byte[] data) => Encoding.UTF8.GetString(data);
    }
}
