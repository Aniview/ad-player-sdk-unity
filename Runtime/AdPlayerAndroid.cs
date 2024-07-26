using System.Linq;
using UnityEngine;

namespace AdPlayer
{
    internal class AdPlayerAndroid : IAdPlayer
    {
        private readonly AndroidJavaObject jAdPlayer = new("com.adservrs.adplayer.unity.Player");

        public void SetIOsAppStoreUrl(string AppStoreURL) { /* empty */ }

        public void InitializePublisher(IAdPlayer.PublisherConfig config)
        {
            var tags = config.Tags.Select(config =>
            {
                var tag = new AndroidJavaObject("com.adservrs.adplayer.unity.Player$InitTagConfig");
                tag.Set("tagId", config.TagId);
                return tag;
            }).ToArray();

            using (var pub = new AndroidJavaObject("com.adservrs.adplayer.unity.Player$InitPubConfig"))
            {
                pub.Set("pubId", config.PublisherId);
                pub.Set("tags", tags);

                jAdPlayer.Call("initializePublisher", pub);
            }

            foreach (var tag in tags)
            {
                tag.Dispose();
            }
        }
    }
}
