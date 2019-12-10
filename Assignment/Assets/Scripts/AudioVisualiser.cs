using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualiser : MonoBehaviour
{
    private AudioSource audioSource;
    private float[] samplesArray;
    private float[] spectrumArray;
    private float sampleRate;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        samplesArray = new float[1024];
        spectrumArray = new float[1024];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
