using Msm.Core.Text.Encoding.Contract;

namespace Msm.Core.Text.Encoding.Providers
{
    /// <summary>
    ///     Provides the default implementation of the <see cref="IDefaultEncodingProvider" /> interface.
    /// </summary>
    public class DefaultEncodingProvider
        : IDefaultEncodingProvider
    {
        /// <inheritdoc />
        public System.Text.Encoding Provide()
        {
            return System.Text.Encoding.UTF8;
        }
    }
}
