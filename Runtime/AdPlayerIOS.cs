using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

namespace AdPlayer
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct TagConfigMarshalled
    {
        [MarshalAs(UnmanagedType.LPStr)]
        internal string tagId;
    }


    internal class AdPlayerIOS : IAdPlayer
    {

        [DllImport("__Internal")]
        private static extern void _initializeSDK(string appStoreURL);

        [DllImport("__Internal")]
        private static extern void _initializePublisher(string publisherId, TagConfigMarshalled[] tags, int count);

        public void SetIOsAppStoreUrl(string AppStoreURL) {
            _initializeSDK(AppStoreURL);
        }

        public void InitializePublisher(IAdPlayer.PublisherConfig config)
        {
            var records = config.Tags;
            var marshalledTags = new TagConfigMarshalled[records.Count];

            for (int i = 0; i < records.Count; i++)
            {
                marshalledTags[i] = new TagConfigMarshalled
                {
                    tagId = config.Tags[i].TagId
                };
            }


            _initializePublisher(config.PublisherId, marshalledTags, config.Tags.Count);
        }
    }
}
