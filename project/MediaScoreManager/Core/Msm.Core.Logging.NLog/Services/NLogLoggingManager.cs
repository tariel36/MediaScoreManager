using Msm.Core.Maintenance.Logging.Contract;
using NLog;

namespace Msm.Core.Logging.NLog.Services
{
    /// <summary>
    /// Provides logging management functionality using NLog.
    /// </summary>
    internal class NLogLoggingManager
        : ILoggingManager
    {
        /// <inheritdoc />
        public void Shutdown()
        {
            LogManager.Shutdown();
        }
    }
}
