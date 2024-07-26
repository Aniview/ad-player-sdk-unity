//
//  UnityPluginBridge.m
//  Unity-iPhone
//
//  Created by Pavel Yevtukhov on 26.07.2024.
//

#import "UnityFramework/UnityFramework-Swift.h"
#import <Foundation/Foundation.h>

extern "C" {
    typedef struct {
        const char *tagId;
    } _TagConfigMarshalled;

    void _initializeSDK(const char* appStoreURL) {
        NSString *sAppStoreURL = [NSString stringWithUTF8String:appStoreURL];
        [AdPlayerPlugin.shared _initializeSDKWithAppStoreURLString:sAppStoreURL];
    }

    void _initializePublisher(const char* publisherId, _TagConfigMarshalled* tagsList, int count) {
        NSMutableArray *tagIds = [NSMutableArray arrayWithCapacity:count];
        NSString *sPublisherId = [NSString stringWithUTF8String:publisherId];
        for (int i = 0; i < count; i++) {
            _TagConfigMarshalled marshaled = tagsList[i];
            NSString *tagId = [NSString stringWithUTF8String:marshaled.tagId];
            [tagIds addObject:tagId];
        }
        [AdPlayerPlugin.shared initializePublisherWithPublisherId:sPublisherId tagIds:[tagIds copy]];
    }

    void _createAdPlacement(const char* placementId) {
        NSString *sPlacementId = [NSString stringWithUTF8String: placementId];
        [AdPlayerPlugin.shared createPlacementWithPlacementId:sPlacementId];
    }

    void _dispose(const char* placementId) {
        NSString *sPlacementId = [NSString stringWithUTF8String: placementId];
        [AdPlayerPlugin.shared disposePlacementWithPlacementId:sPlacementId];
    }

    void _attachTag(const char* placementId, const char* tagId) {
        NSString *sPlacementId = [NSString stringWithUTF8String: placementId];
        NSString *sTagId = [NSString stringWithUTF8String: tagId];
        [AdPlayerPlugin.shared attachTagWithPlacementId:sPlacementId tagId:sTagId];
    }

    void _updatePosition(const char* placementId, int left, int top, int width) {
        NSString *sPlacementId = [NSString stringWithUTF8String: placementId];
        [AdPlayerPlugin.shared updatePositionWithPlacementId:sPlacementId
                                                        left:(NSInteger)left
                                                         top:(NSInteger)top
                                                       width:(NSInteger)width
        ];
    }
}
