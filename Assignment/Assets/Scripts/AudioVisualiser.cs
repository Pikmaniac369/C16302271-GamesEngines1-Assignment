using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualiser : MonoBehaviour
{
    // Declare constants
    private const int SAMPLE_SIZE = 1024;

    // Declare variables for storing audio information
    public float rmsVal; // The audio's average power output
    public float dbVal; // The decibel value
    public float pitchVal; // The value of the pitch

    //Declare variables for controlling the background
    public float backgroundIntensity;
    public Material backMat;
    public Color minColor;
    public Color maxColor;

    // Declare variables for controling cube scaling and movement
    public float maxScale = 25.0f; // Restricts the maximum height of the cubes
    public float modifier = 50.0f;
    public float smoothingSpeed = 10.0f; // Stops "snapping" on the way down
    public float displayAmount = 0.5f; // Displays only a certain amount of the samples. Between 0 and 1.

    // Declare variables for storing audio data
    private AudioSource audioSource; // Store the audio source
    private float[] samplesArray; // Store the audio samples
    private float[] spectrumArray;
    private float sampleRate; // Store the sample rate of the audio

    // Declare variables for storing cubes
    private Transform[] visualList;
    private float[] visualScale;
    public int amountOfVisuals = 64; // The amount of cubes to create


    // Start is called before the first frame update
    void Start()
    {
        // Initialise the variables
        audioSource = GetComponent<AudioSource>();
        samplesArray = new float[SAMPLE_SIZE];
        spectrumArray = new float[SAMPLE_SIZE];
        sampleRate = AudioSettings.outputSampleRate;

        // Create a line of responsive cubes
        //CreateLineOfCubes();
        // Create a circle of responsive cubes
        CreateCircleOfCubes();
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

    private void CreateCircleOfCubes()
    {
        visualScale = new float[amountOfVisuals];
        visualList = new Transform[amountOfVisuals];
        Vector3 center = Vector3.zero; // Set the center of the circle to be the center of the scene
        float radius = 10.0f; // The radius of the circle

        // Create the circle of cubes
        for (int i = 0; i < amountOfVisuals; i++)
        {
            // Get the angle at which the cube will be created
            float angle = i * 1.0f / amountOfVisuals;
            angle = angle * Mathf.PI * 2;

            // Set the x and y coordinates of the cube when it is created
            float x = center.x + Mathf.Cos(angle) * radius;
            float y = center.y + Mathf.Sin(angle) * radius;

            // Get the cube's position when it is created
            Vector3 cubePosition = center + new Vector3(x, y, 0);

            // Create the cube
            GameObject cubeObject = GameObject.CreatePrimitive(PrimitiveType.Cube) as GameObject;

            // Set the cube's position and rotation when it is created
            cubeObject.transform.position = cubePosition;
            cubeObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, cubePosition);

            // Add the cube to the list of cubes to display
            visualList[i] = cubeObject.transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        AnalyseSound();
        UpdateCubes();
        UpdateBackground();
    }

    private void UpdateCubes()
    {
        int visualIndex = 0;
        int spectrumIndex = 0;
        int averageSize = (int)( (SAMPLE_SIZE * displayAmount)/ amountOfVisuals);

        while(visualIndex < amountOfVisuals)
        {
            float sum = 0;

            for(int j = 0; j < averageSize; j++)
            {
                sum = sum + spectrumArray[spectrumIndex];
                spectrumIndex++;
            }

            float scaleY = sum / averageSize * modifier;
            visualScale[visualIndex] -= Time.deltaTime * smoothingSpeed;

            if(visualScale[visualIndex] < scaleY)
            {
                visualScale[visualIndex] = scaleY;
            }

            // Restrict the maximum height of the cubes
            if(visualScale[visualIndex] > maxScale)
            {
                visualScale[visualIndex] = maxScale;
            }

            // Set the height of the cubes
            visualList[visualIndex].localScale = Vector3.one + Vector3.up * visualScale[visualIndex];
            visualIndex++;
        }

    }

    private void UpdateBackground()
    {
        // Reduce the background intensity
        backgroundIntensity -= Time.deltaTime * smoothingSpeed;

        if(backgroundIntensity < dbVal)
        {
            backgroundIntensity = dbVal;
        }

        backMat.color = Color.Lerp(minColor, maxColor, backgroundIntensity);
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
