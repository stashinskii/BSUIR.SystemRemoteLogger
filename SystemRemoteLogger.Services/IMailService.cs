using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SystemRemoteLogger.Services
{
    /// <summary>
    /// Represends interface for mailing service
    /// </summary>
    public interface IMailService
    {
        Task Start();
    }
}
