using Msm.Core.Extensions;
using Msm.Core.Stability.Contract;

namespace Msm.Core.Stability.Providers
{
    /// <summary>
    /// Provides a singleton implementation of the <see cref="INullObjectProvider"/> interface for managing null object instances.
    /// </summary>
    internal class NullObjectProvider
        : INullObjectProvider
    {
        /// <summary>
        /// Synchronization object for thread-safe singleton initialization.
        /// </summary>
        private static readonly object SyncRoot = new();

        /// <summary>
        /// The singleton instance of the <see cref="NullObjectProvider"/>.
        /// </summary>
        private static NullObjectProvider? _instance;

        /// <summary>
        /// A dictionary to store registered null object instances by their type.
        /// </summary>
        private readonly Dictionary<Type, object> _nullObjects = new();

        /// <summary>
        /// Prevents instantiation from outside the class.
        /// </summary>
        private NullObjectProvider()
        {
        }

        /// <inheritdoc />
        public static INullObjectProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new();
                        }
                    }
                }

                return _instance;
            }
        }

        /// <inheritdoc />
        public void Register<TValue>(TValue nullObjectInstance)
            where TValue : class
        {
            _nullObjects[typeof(TValue)] = nullObjectInstance;
        }

        /// <inheritdoc />
        public TValue Resolve<TValue>()
        {
            Type type = typeof(TValue);

            if (_nullObjects.GetValueOrDefault(type) is TValue result)
            {
                return result;
            }

            type.IsClass.AssertIsTrue<KeyNotFoundException>(type.Name);

            return type.Create<TValue>();
        }
    }
}
