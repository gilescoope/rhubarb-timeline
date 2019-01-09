# Rhubarb Timeline
Unity Timeline tracks for Rhubarb Lip Sync

https://youtu.be/n_LAp6tTjv0

## Rhubarb
https://github.com/DanielSWolf/rhubarb-lip-sync
Rhubarb Lip Sync is a command-line tool that automatically creates 2D mouth animation from voice recordings. This Unity extension allows an animator to add a Rhubarb Sprite component to a Game Object with a Sprite Renderer, then animate the mouth shapes in Timeline.

Using Rhubarb a Timeline track can be automatically generated from an audio file.

## Rhubarb Sprite Set
To create a Rhubarb Sprite Set go to Assets > Create > Rhubarb > Sprite Set. A sprite should be referenced for each possible mouth shape. A premade Sprite Set "Lisa" has been included.

## Rhubarb Sprite Component
To add a Rhubarb Sprite Component to a Game Object click Add Component > Rhubarb Sprite. This will automatically add a Sprite Renderer to the Game Object if none is present. The desired Rhubarb Sprite Set can be set in the component.

## Rhubarb Playable Track / Clip
To add a Rhubarb Playable Track to a Timeline click Add > Rhubarb Playable Track. To add clips (individual mouth shapes in the track) Right Click > Add Rhubarb Playable Clip. The Mouth Shape used can be set in the inspector.

## Rhubarb Timeline Window
To automatically create the Rhubarb Playable Track and Audio Track from an Audio Clip go to Window > Sequencing > Rhubarb Timeline.

You need to locate the Rhubarb application (downloaded from https://github.com/DanielSWolf/rhubarb-lip-sync/releases) on your hard drive. This does not have to be located in your Unity project folder.

The Game Objects for the Timeline (Playable Director), Rhubarb Sprite and Audio Source must be assigned before you can generate your tracks.

The AudioClip to be used for generation can be assigned, if you try and use a file without the extension ".wav" or ".ogg" you will get a warning and not be able to generate the tracks.

The other settings correspond to the command-line options for Rhubarb which can be found at https://github.com/DanielSWolf/rhubarb-lip-sync.

## Limitations
Rhubarb only supports WAVE (.wav) and Ogg Vorbis (.ogg) files.
This plugin has only been tested on Windows and OSX.
