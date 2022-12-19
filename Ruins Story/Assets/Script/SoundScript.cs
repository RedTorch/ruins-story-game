using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    [SerializeField] private float speedAudioCutoff = 5f;
    [SerializeField] private AnimationCurve SoundFeedbackCurve;

    private AudioSource m_MyAudioSource;
    private float speedNormalized;

    //Play the music
    
    void Start()
    {
        //Fetch the AudioSource from the GameObject
        m_MyAudioSource = GetComponent<AudioSource>();
        //Ensure the toggle is set to true for the music to play at start-up
    }

    void Update()
    {
        // m_MyAudioSource.Play(); or .Stop();

        // if(rigidbody.velocity.magnitude >= 0.001f) {
        //     if(m_myAudioSource.isPlaying)
        // }
        print(GetComponent<Rigidbody>().velocity.magnitude);
        speedNormalized = Mathf.Clamp(GetComponent<Rigidbody>().velocity.magnitude/speedAudioCutoff,0f,1f);
        if(speedNormalized>0.0000001f) {
            m_MyAudioSource.volume = SoundFeedbackCurve.Evaluate(speedNormalized);
        }
        else {
            m_MyAudioSource.volume = 0f;
        }
        // m_MyAudioSource.volume = Mathf.Clamp(GetComponent<Rigidbody>().velocity.magnitude/speedAudioCutoff,0f,1f);
    }
}
