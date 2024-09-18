using UnityEngine;
using System.Runtime.InteropServices;

namespace AdPlayer
{
    internal class AdPlayerJs : IAdPlayer
    {
        [DllImport("__Internal")]
        private static extern void Ada_Player_SetPlayerSelector(string selector);

        [DllImport("__Internal")]
        private static extern void Ada_Player_InitializePublisher(string pubId, string tagId);

        public void InitializePublisher(IAdPlayer.PublisherConfig config)
        {
            foreach (var tag in config.Tags)
            {
                Ada_Player_InitializePublisher(config.PublisherId, tag.TagId);
            }
        }
    }
}
