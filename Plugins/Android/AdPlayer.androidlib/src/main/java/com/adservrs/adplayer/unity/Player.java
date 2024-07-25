package com.adservrs.adplayer.unity;

import androidx.annotation.Keep;

import com.adservrs.adplayer.AdPlayer;
import com.adservrs.adplayer.tags.AdPlayerPublisherConfiguration;
import com.adservrs.adplayer.tags.AdPlayerTagConfiguration;
import com.unity3d.player.UnityPlayer;

@Keep
public class Player {
    public static class InitPubConfig {
        public String pubId;
        public InitTagConfig[] tags;
    }

    public static class InitTagConfig {
        public String tagId;
    }

    @Keep
    public void initializePublisher(InitPubConfig config) {
        if (config.tags.length == 0) {
            return;
        }

        AdPlayerTagConfiguration[] tags = new AdPlayerTagConfiguration[config.tags.length - 1];
        for (int index = 1; index < config.tags.length; index++) {
            tags[index - 1] = new AdPlayerTagConfiguration(config.tags[index].tagId);
        }

        AdPlayerPublisherConfiguration pub = new AdPlayerPublisherConfiguration(
                config.pubId,
                new AdPlayerTagConfiguration(config.tags[0].tagId),
                tags
        );

        AdPlayer.initialize(UnityPlayer.currentContext);
        AdPlayer.initializePublisher(pub);
    }
}
