# A Unity-based app for bedroom guitar heroes!

This is the source code of ProShredder, one of my hobby projects, that allows players to record themselves while shredding!

## Motivation

Traditional DAW softwares rarely have the ability to record the video while recording sounds from the guitar, also, after watching the setups of some guitarists 
(myself included), I feel like to do a cover of a song would take so much time for preparation.
I hope this app can take some of the jobs and let guitarists focus on shredding!

## Current Features (will be updated frequently...)

- A RockSmith-like interface for recording the guitar and video simultaneously.
- A mini audio player that can load the audio file from a destinated folder. (clearly I don't want Eagles to sue me over the copyright issue so pls find your own source lol) The functions of raising/lowering the pitch of the audio is added so you don't have to drop your tune when playing 80's hard rock.
- A count-in system.
- A tablature making system that contains basic note and guitar techniques (bending, harmonics) notation, timeline synchronization with the source audio, save/load, etc..
- A built-in simple pedal system, currently it only contains distortion effect and delay effect.

## Upcoming Plans

- Hook up AI model for instruments separation. Currently I am thinking about using existing APIs, but clearly it is not a long-term solution. With that you can play with the separated backing track of your desire.
- Video/audio recording, multi-channel editing, basically a poor man's version of any DAW and Video Editor combined.
- Post-processing effector chains.

## Further Plans

- AI-assisted tab-making.
- Blues improvisation mode.
- Multi-audio channel input for people who want to sing while playing.

# How to use

Notice that this is far from the final version, but you're welcome to test it out!
<br/>
**Don't use the built-in microphone from your computer if you're not using headphone, unless you want your speaker to blow up**

## What you need

- A PC with a good sound card/driver. (This is important, when I use my Windows laptop to test it out, the lag is so egregious that I cannot play. A MacBook may be a good choice, at least my MacBook works well)
- Unity 2020.3.48f software.
- An audio interface. I am using Scarlett 2i2.
- A pedal board if you want more dynamic pedal effects.
- A guitar/bass

## Tutorial

- Clone this repo to your local computer.
- Install Unity Hub and Unity 2020.3.48 if you haven't.
- Go to the source file, in Assets/Scene, open the SampleScene
- Explore the GUI. But please remember don't select computer microphone if your computer is not plugged in with a headphone.
