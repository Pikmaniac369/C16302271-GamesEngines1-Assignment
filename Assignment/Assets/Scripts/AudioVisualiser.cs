using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualiser : MonoBehaviour
{
    // Declare variables for storing audio data
    public float rmsVal; // The audio's average power output
    public float dbVal; // The decibel value
    public float pitchVal;

    private AudioSource audioSource; // Store the audio source
    private float[] samplesArray; // Store the audio samples
    private float[] spectrumArray;
    private float sampleRate;

    // Start is called before the first frame update
    void Start()
    {
        // Initialise the variables
        audioSource = GetComponent<AudioSource>();
        samplesArray = new float[1024];
        spectrumArray = new float[1024];
        sampleRate = AudioSettings.outputSampleRate;
    }

    // Update is called once per frame
    void Update()
    {
        AnalyseSound();
    }

    private void AnalyseSound()
    {
        audioSource.GetOutputData(samplesArray, 0); // Listen for samples on channel 0

    }
}
