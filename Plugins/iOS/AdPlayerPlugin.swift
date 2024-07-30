import Foundation
import UIKit
import AdPlayerSDK

@objc
public final class AdPlayerPlugin: NSObject {
    @objc
    public private(set) static var shared = AdPlayerPlugin()

    private let storage = AdsStorage()

    @objc
    public func _initializeSDK(appStoreURLString: String) {
        let url = URL(string: appStoreURLString)!
        AdPlayerSDK.AdPlayer.initSdk(storeURL: url)
    }

    @objc
    public func initializePublisher(publisherId: String, tagIds: [String]) {
        guard !tagIds.isEmpty else {
            print("initializePublisher Error: No tags provided")
            return
        }
        let tags = tagIds.map { AdPlayerTagConfiguration(tagId: $0) }

        let configuration = AdPlayerPublisherConfiguration(publisherId: publisherId, tagConfiguration: tags.first!, tags)
        AdPlayer.initializePublisher(configuration) { result in
            switch result {
            case .success:
                break
            case .failure(let error):
                print(error.localizedDescription)
            }
        }
    }

    @objc
    public func createPlacement(placementId: String) {
        guard !storage.exists(id: placementId) else { return }

        let presenter = AdPresenter()
        storage.add(id: placementId, value: presenter)
    }

    @objc
    public func disposePlacement(placementId: String) {
        storage.remove(id: placementId)
    }

    @objc
    public func attachTag(placementId: String, tagId: String) {
        let presenter = storage.value(id: placementId)

        presenter?.attach(tagId: tagId)
    }

    @objc
    public func updatePosition(placementId: String, left: Int, top: Int, width: Int) {
        guard let presenter = storage.value(id: placementId) else { return }

        let scale = UIScreen.main.scale
        presenter.updatePosition(
            left: CGFloat(left) / scale,
            top: CGFloat(top) / scale,
            width: CGFloat(width) / scale
        )
    }
}
