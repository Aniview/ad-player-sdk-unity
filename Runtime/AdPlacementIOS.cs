using System;
using System.Threading;
using System.Runtime.InteropServices;
using UnityEngine;

namespace AdPlayer
{
    internal class AdPlacementIOS : IAdPlacement
    {
        [DllImport("__Internal")]
        private static extern void _createAdPlacement(string placementId);
        [DllImport("__Internal")]
        private static extern void _dispose(string placementId);
        [DllImport("__Internal")]
        private static extern void _attachTag(string placementId, string tagId);
        [DllImport("__Internal")]
        private static extern void _updatePosition(string placementId, int x, int y, int width);

        private readonly string placementId = Guid.NewGuid().ToString();

        internal AdPlacementIOS() {
            _createAdPlacement(placementId);
        }

        ~AdPlacementIOS()
        {
            Dispose();
        }

        public void Dispose()
        {
            _dispose(placementId);
        }

        public void AttachTag(string tagId)
        {
           _attachTag(placementId, tagId);
        }

        public void UpdatePosition(int x, int y, int width, int height)
        {
            _updatePosition(placementId, x, y, width);
        }
    }
}
