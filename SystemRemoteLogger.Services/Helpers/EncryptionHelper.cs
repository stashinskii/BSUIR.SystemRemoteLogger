using System;
using System.Collections.Generic;
using System.Text;

namespace SystemRemoteLogger.Services.Helpers
{
    public static class EncryptionHelper
    {
        public static byte[] Encode(string data) => Encoding.UTF8.GetBytes(data);
        public static string Decode(byte[] data) => Encoding.UTF8.GetString(data);
    }
}
