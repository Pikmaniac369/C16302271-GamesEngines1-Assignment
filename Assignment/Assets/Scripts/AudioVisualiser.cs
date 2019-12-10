using System.Collections;
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

    private Transform[] visualList;
    private float[] visualScale;
    private int amountOfVisuals = 10; // The amount of cubes to create


    // Start is called before the first frame update
    void Start()
    {
        // Initialise the variables
        audioSource = GetComponent<AudioSource>();
        samplesArray = new float[SAMPLE_SIZE];
        spectrumArray = new float[SAMPLE_SIZE];
        sampleRate = AudioSettings.outputSampleRate;

        // Create a line of responsive cubes
        CreateLineOfCubes();
    }

    private void CreateLineOfCubes()
    {
        // Initialise variables
        visualScale = new float[amountOfVisuals];
        visualList = new Transform[amountOfVisuals];

        // Create the cubes
        for(int i = 0; i < amountOfVisuals; i++)
        {
            GameObject cubeGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube) as GameObject;
            visualList[i] = cubeGameObject.transform; // Adds a cube to the list of visuals at index i
            visualList[i].position = Vector3.right * i; // Set the spawn position of the cube to 1 unit to the right of the last one

        }
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
        float sum = 0;

        for(int i = 0; i < SAMPLE_SIZE; i++)
        {
            sum = sum + (samplesArray[i] * samplesArray[i]);
        }

        rmsVal = Mathf.Sqrt(sum / SAMPLE_SIZE);

        // Get the decibel value
        dbVal = 20 * Mathf.Log10(rmsVal / 0.1f);

        // Get the spectrum data from the audio source
        audioSource.GetSpectrumData(spectrumArray, 0, FFTWindow.BlackmanHarris);

    }
}
