mergeInto(LibraryManager.library, {
  ////////////////////////////////////////////////////////////////
  // Player API
  ////////////////////////////////////////////////////////////////
  Ada_Player_InitializePublisher: function (pubId, tagId) {
    pubId = UTF8ToString(pubId);
    tagId = UTF8ToString(tagId);
    return runAdPlayer((ada) => ada.player.initializePublisher(pubId, tagId));
  },

  ////////////////////////////////////////////////////////////////
  // Placement API
  ////////////////////////////////////////////////////////////////
  Ada_Placement_Alloc: function () {
    return runAdPlayer((ada) => ada.placement.alloc());
  },

  ////////////////////////////////////////////////////////////////
  Ada_Placement_Dispose: function (id) {
    id = UTF8ToString(id);
    return runAdPlayer((ada) => ada.placement.dispose(id));
  },

  ////////////////////////////////////////////////////////////////
  Ada_Placement_UpdatePosition: function (id, x, y, width, height) {
    id = UTF8ToString(id);
    return runAdPlayer((ada) => ada.placement.updatePosition(id, x, y, width, height));
  },

  ////////////////////////////////////////////////////////////////
  Ada_Placement_AttachTag: function (id, tagId) {
    id = UTF8ToString(id);
    tagId = UTF8ToString(tagId);
    return runAdPlayer((ada) => ada.placement.attachTag(id, tagId));
  },
});
