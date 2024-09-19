using System.Threading;
using System.Runtime.InteropServices;
using UnityEngine;

namespace AdPlayer
{
    internal class AdPlacementJs : IAdPlacement
    {
        [DllImport("__Internal")]
        private static extern string Ada_Placement_Alloc();

        [DllImport("__Internal")]
        private static extern void Ada_Placement_Dispose(string id);

        [DllImport("__Internal")]
        private static extern void Ada_Placement_AttachTag(string id, string tagId);

        [DllImport("__Internal")]
        private static extern void Ada_Placement_UpdatePosition(string id, int x, int y, int width, int height);

        private string id = Ada_Placement_Alloc();
        private int disposed = 0;

        ~AdPlacementJs()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (Interlocked.Exchange(ref disposed, 1) == 0)
            {
                Ada_Placement_Dispose(id);
            }
        }

        public void AttachTag(string tagId)
        {
            Ada_Placement_AttachTag(id, tagId);
        }

        public void UpdatePosition(int x, int y, int width, int height)
        {
            Ada_Placement_UpdatePosition(id, x, y, width, height);
        }
    }
}
