import UIKit

extension UIWindow {
    static func getSuitableHostVC() -> UIViewController? {
        guard let rootVC = UIWindow.findKeyWindow()?.rootViewController else {
            return nil
        }
        guard let navigationViewController = rootVC as? UINavigationController else {
            return rootVC
        }
        return navigationViewController.children.first
    }

    static func findKeyWindow() -> UIWindow? {
        if #available(iOS 13.0, *) {
            return UIApplication
                .shared
                .connectedScenes
                .flatMap { ($0 as? UIWindowScene)?.windows ?? [] }
                .last { $0.isKeyWindow }
        } else {
            return UIApplication.shared.keyWindow
        }
    }
}