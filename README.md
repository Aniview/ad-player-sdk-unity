# AdPlayer Unity package

This repository contains `AdPlayer` Unity package.

# Preparations

## Install EDM4U dependency

`AdPlayer` package has a single dependency - [External Dependency Manager for Unity](https://github.com/googlesamples/unity-jar-resolver).

To install it follow these steps:
- Download EDM4U package from [here](https://developers.google.com/unity/archive#external_dependency_manager_for_unity)
- Open Package Manager (`Window > Package Manager`)
- Add EDM4U package previously downloaded (`+ > Add package from tarball`)

## Install AdPlayer package

Now we need to add AdPlayer package to the project.

To install it follow these steps:
- Open Package Manager (`Window > Package Manager`)
- Add EDM4U package previously downloaded (`+ > Add package from git URL`)
- Enter [this](git@github.com:Aniview/ad-player-sdk-unity.git) repository url and click `Add`

## Player configuration

Player needs to be properly configured before building project for Android/iOS:
- Go to `File > Build Settings > Player Settings`
- Open `Other Settings` group
- Change `Script Backend` to `IL2CPP`
- Enable `ARM64` Target Architecture

## Selecting Android/iOS build

AdPLayer plugin works only for Android and iOS devices. Required platform should be selected before building the project:
- Go to `File -> Build settings`
- Select `Android`/`iOS` from the list
- Click `Switch platform`

# Usage

`AdPlayer` package API is very simple to use.

## Initializing SDK

SDK must be initialized before anything with it:
```cs
private static readonly string PUB_ID = "<published id>";
private static readonly string TAG_ID = "<tag id>";

IAdPlayer.Instance.InitializePublisher(new(
    PublisherId: PUB_ID,
    Tags: new List<IAdPlayer.TagConfig> { new(TagId: TAG_ID) }
));
```

## Creating a placement

To show any AD a placement must be created:
```cs
placement = IAdPlacement.Alloc();
placement.AttachTag(TAG_ID);
placement.UpdatePosition(x, y, width, height);
```

Please note that placement coordinates are in screen space pixels.

## Releasing a placement

When placement is no longer needed id must be disposed:
```cs
placement.Dispose();
placement = null;
```

Please note that forgetting to release a placement will cause a memory leak.

## Running appliation

To run application simply select `File -> Build And Run` from the menu.
