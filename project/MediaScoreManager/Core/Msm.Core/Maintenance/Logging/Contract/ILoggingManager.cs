namespace Msm.Core.Maintenance.Logging.Contract
{
    /// <summary>
    /// Represents a logging manager responsible for managing logging operations.
    /// </summary>
    public interface ILoggingManager
    {
        /// <summary>
        /// Shuts down the logging system and releases any resources held by it.
        /// </summary>
        void Shutdown();
    }
}
