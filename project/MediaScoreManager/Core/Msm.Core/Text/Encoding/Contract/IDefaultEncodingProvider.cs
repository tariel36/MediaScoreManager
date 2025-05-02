namespace Msm.Core.Text.Encoding.Contract
{
    /// <summary>
    /// Defines a contract for providing the default text encoding.
    /// </summary>
    public interface IDefaultEncodingProvider
    {
        /// <summary>
        /// Provides the default text encoding.
        /// </summary>
        /// <returns>The instance of <see cref="System.Text.Encoding"/>.</returns>
        System.Text.Encoding Provide();
    }
}
