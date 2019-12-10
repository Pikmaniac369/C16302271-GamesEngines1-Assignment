﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualiser : MonoBehaviour
{
    // Declare constants
    private const int SAMPLE_SIZE = 1024;

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
        samplesArray = new float[SAMPLE_SIZE];
        spectrumArray = new float[SAMPLE_SIZE];
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

        // Get the value for rms
        int i = 0; // Counter for the for loop
        float sum = 0;

        for(; i < SAMPLE_SIZE; i++)
        {
            sum = sum + (samplesArray[i] * samplesArray[i]);
        }

        rmsVal = Mathf.Sqrt(sum / SAMPLE_SIZE);

        // Get the decibel value
        dbVal = 20 * Mathf.Log10(rmsVal / 0.1f);



    }
}
