//
//  AdsStorage.swift
//  UnityFramework
//
//  Created by Pavel Yevtukhov on 29.07.2024.
//

final class AdsStorage {
    private var storage: [String: AdPresenter] = [:]

    func add(id: String, value: AdPresenter) {
        cleanUp()
        storage[id] = .init(value)
    }

    func exists(id: String) -> Bool {
        storage[id] != nil
    }

    func value(id: String) -> AdPresenter? {
        storage[id]
    }

    func remove(id: String) {
        cleanUp()
        storage[id] = nil
    }

    func cleanUp() {
        storage = storage.filter { $0.value.adView != nil }
    }
}
