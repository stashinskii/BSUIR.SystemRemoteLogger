﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SystemRemoteLogger.Services
{
    /// <summary>
    /// Represends interface for entitites which provides configuration for mailing service
    /// </summary>
    public interface IConfigurationProvider
    {
        string MailFrom { get; }
        string Password { get; }
        string MailTo { get; }
        string Directory { get; }
    }
}