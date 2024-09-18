const AdPlayer = new function () {
  var tag2pub = {};
  var placementCounter = 0;
  var placementsById = {};

  ////////////////////////////////////////////////////////////////
  // Player API
  ////////////////////////////////////////////////////////////////
  this.player = new function () {
    ////////////////////////////////////////////////////////////////
    this.initializePublisher = function (pubId, tagId) {
      console.log("player.initializePublisher: pubId = " + pubId + ", tagId = " + tagId);
      tag2pub[tagId] = pubId;
    };
  };

  ////////////////////////////////////////////////////////////////
  // Placement API
  ////////////////////////////////////////////////////////////////
  this.placement = new function () {
    ////////////////////////////////////////////////////////////////
    this.alloc = function () {
      console.log("placement.alloc");

      const unityContainer = Module.canvas.parentElement;
      if (!unityContainer) {
        return console.error("unity container not found");
      }

      const playerContainer = document.createElement("div");
      playerContainer.id = "av_player_" + (++placementCounter) + "_container";
      playerContainer.style = "background-color: red;";
      playerContainer.style.position = "absolute";
      playerContainer.style.top = "0px";
      playerContainer.style.left = "0px";
      playerContainer.style.width = "0px";
      playerContainer.style.height = "0px";

      placementsById[playerContainer.id] = playerContainer;
      unityContainer.appendChild(playerContainer);

      return playerContainer.id;
    };

    ////////////////////////////////////////////////////////////////
    this.dispose = function (id) {
      console.log("placement.dispose: id = " + id);

      const playerContainer = placementsById[id];
      if (!playerContainer) {
        return;
      }

      playerContainer.remove();
      // TODO dispose player instance
    };

    ////////////////////////////////////////////////////////////////
    this.updatePosition = function (id, x, y, width, height) {
      console.log("placement.updatePosition: id = " + id + ", x = " + x + ", y = " + y + ", w = " + width + ", h = " + height);

      const playerContainer = placementsById[id];
      if (!playerContainer) {
        return;
      }

      const bounds = Module.canvas.getBoundingClientRect();
      if (bounds) {
        const clientWidth = bounds.width;
        const clientHeight = bounds.height;
        const viewportWidth = Module.canvas.width;
        const viewportHeight = Module.canvas.height;

        if (clientWidth > 0 && clientHeight > 0 && viewportWidth > 0 && viewportHeight > 0) {
          const scaleX = clientWidth / viewportWidth;
          const scaleY = clientHeight / viewportHeight;

          x = x * scaleX;
          y = y * scaleY;
          width = width * scaleX;
          height = height * scaleY;
        }
      }

      const player = playerContainer.avPlayer;
      if (player) {
        player.resize(width, height);
      }

      playerContainer.style.top = y + "px";
      playerContainer.style.left = x + "px";
      playerContainer.style.width = width + "px";
      playerContainer.style.height = height + "px";
    };

    ////////////////////////////////////////////////////////////////
    this.attachTag = function (id, tagId) {
      console.log("placement.attachTag: id = " + id + ", tagId = " + tagId);
      
      const pubId = tag2pub[tagId];
      if (!pubId) {
        return console.error("tag " + tagId + " was not initialized");
      }

      const playerContainer = placementsById[id];
      if (!playerContainer) {
        return console.error("player " + id + " was not found");
      }

      const loader = async () => {
        const url = "https://tg1.aniview.com/api/adserver/sdk/spt?AV_PUBLISHERID=" + pubId + "&AV_TAGID=" + tagId;
        const response = await fetch(url);
        const json = await response.json();
        const channelId = json.config.channelId;

        if (!channelId) {
          return console.error("failed to fetch channel id");
        }

        console.log("placement.attachTag: id = " + id + ", channelId = " + channelId);

        const playerConfig = {
          publisherId: pubId,
          channelId: channelId,
          tagId: tagId,
          width: playerContainer.clientWidth,
          height: playerContainer.clientHeight,
          position: playerContainer.id
        };

        const playerScript = document.createElement("script");
        playerScript.src = "https://player.aniview.com/script/6.1/player.js?v=1&type=s&pid=" + pubId;
        playerScript.crossOrigin = "anonymous"; // TOOD remove when fixed
        playerScript.onload = function () {
          if (!playerContainer.parentElement) {
            return;
          }
          const player = new avPlayer(playerConfig);
          player.play(playerConfig);
          playerContainer.avPlayer = player;
        };
        playerScript.onerror = function (error) {
          console.error("placement.attachTag: id = " + id + ", error = " + error);
          // TODO handle error
        };
        playerContainer.appendChild(playerScript);
      };

      loader().catch((error) => {
        console.error(error); // TODO properly handle error
      });
    };
  };
};

function runAdPlayer(closure) {
  try {
    var result = closure(AdPlayer);
    if (typeof result == "string") {
      result = stringToNewUTF8(result);
    }
    return result;
  } catch (e) {
    console.error(e);
    return null;
  }
}
