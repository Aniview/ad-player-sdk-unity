//
//  AdPresenter.swift
//  UnityFramework
//
//  Created by Pavel Yevtukhov on 29.07.2024.
//

import UIKit
import AdPlayerSDK

final class AdPresenter {
    private(set) weak var adView: AdPlayerPlacementView?

    private var leftContraint: NSLayoutConstraint?
    private var topConstraint: NSLayoutConstraint?
    private var widthContraint: NSLayoutConstraint?

    func attach(tagId: String) {
        guard let superview = UIWindow.getSuitableHostVC()?.view else { return }

        let adView = AdPlayerPlacementView(tagId: tagId)
        adView.translatesAutoresizingMaskIntoConstraints = false
        superview.addSubview(adView)

        let leftContraint = adView.leadingAnchor.constraint(equalTo: superview.leadingAnchor)
        let topConstraint = adView.topAnchor.constraint(equalTo: superview.topAnchor)
        let widthContraint = adView.widthAnchor.constraint(equalToConstant: 1)
        NSLayoutConstraint.activate([leftContraint, topConstraint, widthContraint])

        self.adView = adView
        self.leftContraint = leftContraint
        self.widthContraint = widthContraint
        self.topConstraint = topConstraint
    }

    func updatePosition(left: CGFloat, top: CGFloat, width: CGFloat) {
        leftContraint?.constant = left
        topConstraint?.constant = top
        widthContraint?.constant = width
    }

    deinit {
        adView?.removeFromSuperview()
    }
}
