using System;
using UnityEngine;

namespace AdPlayer
{
    /// <summary>
    /// Placement responsible for diplaying an Ad.
    /// </summary>
    public interface IAdPlacement : IDisposable
    {
        #region Allocation

        /// <summary>
        /// Create new placement instance.
        /// Placement must be disposed when no longer in use.
        /// </summary>
        public static IAdPlacement Alloc()
        {
            return Application.platform switch
            {
                RuntimePlatform.Android => new AdPlacementAndroid(),
                RuntimePlatform.IPhonePlayer => new AdPlacementIOS(),
                _ => throw new Exception("unsupported platform"),
            };
        }

        #endregion

        /// <summary>
        /// Attach a tag to the placement.
        /// </summary>
        void AttachTag(string tagId);

        /// <summary>
        /// Update placement position in screen space coordinates.
        /// </summary>
        void UpdatePosition(int x, int y, int width, int height);
    }
}
