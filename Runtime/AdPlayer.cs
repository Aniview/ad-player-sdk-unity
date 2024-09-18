using System;
using System.Collections.Generic;
using UnityEngine;

namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}

namespace AdPlayer
{
    /// <summary>
    /// Responsible for initialization and management of tag objects
    /// </summary>
    public interface IAdPlayer
    {
        /// <summary>
        /// Tag configuration used during initialization
        /// </summary>
        public record TagConfig(string TagId);

        /// <summary>
        /// Publisher configuration used during initialization
        /// </summary>
        public record PublisherConfig(string PublisherId, List<TagConfig> Tags);

        /// <summary>
        /// iOS only: Sets the AppStore URL of the App. Call once before any other SDK calls.
        /// </summary>
        /// <param name="AppStoreURL"></param>
        void SetIOsAppStoreUrl(string AppStoreURL) { }

        /// <summary>
        /// Initialize publisher and all of its tags.
        /// Must be called at most once per published/tag. All subsequent calls will be ignored.
        /// </summary>
        void InitializePublisher(PublisherConfig config);

        #region Singleton management

        /// <summary>
        /// Singleton object store
        /// </summary>
        private static readonly Lazy<IAdPlayer> instance = new(() =>
        {
            return Application.platform switch
            {
#if UNITY_ANDROID
                RuntimePlatform.Android => new AdPlayerAndroid(),
#endif
#if UNITY_IOS
                RuntimePlatform.IPhonePlayer => new AdPlayerIOS(),
#endif
#if UNITY_WEBGL
                RuntimePlatform.WebGLPlayer => new AdPlayerJs(),
#endif
                _ => throw new Exception("unsupported platform"),
            };
        });

        /// <summary>
        /// Provides global object instance
        /// </summary>
        public static IAdPlayer Instance
        {
            get { return instance.Value; }
        }

        #endregion
    }
}
