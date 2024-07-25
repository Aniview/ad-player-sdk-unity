using System.Threading;
using UnityEngine;

namespace AdPlayer
{
    internal class AdPlacementAndroid : IAdPlacement
    {
        private int disposed = 0;
        private readonly AndroidJavaObject jAdPlacement = new("com.adservrs.adplayer.unity.Placement");

        ~AdPlacementAndroid()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (Interlocked.Exchange(ref disposed, 1) == 0)
            {
                jAdPlacement.Call("dispose");
                jAdPlacement.Dispose();
            }
        }

        public void AttachTag(string tagId)
        {
            jAdPlacement.Call("attachTag", tagId);
        }

        public void UpdatePosition(int x, int y, int width, int height)
        {
            jAdPlacement.Call("updatePosition", x, y, width, height);
        }
    }
}
