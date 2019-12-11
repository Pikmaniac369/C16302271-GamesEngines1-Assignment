# C16302271-GamesEngines1-Assignment
The repository for my Games Engines 1 2019-2020 assignment.

## Week 13
For this assignment, I created an audio visualiser. Unfortunately though, due to a very busy schedule, I was unable to start work on this assignment until the week it was due. As such, I made my audio visualiser while referring to the following tutorial:

[![YouTube](http://img.youtube.com/vi/wtXirrO-iNA/0.jpg)](https://www.youtube.com/watch?v=wtXirrO-iNA)

### Description
My audio visualiser creates a ring of cubes that respond to music in front of a coloured background. The colour of the background and the colour of the ring of cubes change along with the intensity of the music. The more intense the music, the brighter the colour. The less intense the music, the darker the colour.

The audio visualiser loads in music from an audio source component in the main camera. The AudioVisualiser.cs script then gets a collection of samples from the music and divides these samples across the ring of cubes. As the power of the audio changes, the cubes scale on their local y-axes accordingly, creating the desired effect.

The part of the assignment that I am the most proud of is the colour changing of the ring of cubes, as I managed to figure out how to do that without using a tutorial.

### Instructions for Building and Running
There are no special requirements for building and loading. You simply have to press the play button in the Unity Editor. If you want to change the song that is playing, the audio source component is attached to the main camera. Simply drag and drop a different audio file from the Audio folder onto the audio source component. A few music tracks have been provided.

## Week 5
I will attempt to create a sound visualiser. I will use tutorials like the one below:

[![YouTube](http://img.youtube.com/vi/GHc9RF258VA/0.jpg)](https://www.youtube.com/watch?v=GHc9RF258VA)