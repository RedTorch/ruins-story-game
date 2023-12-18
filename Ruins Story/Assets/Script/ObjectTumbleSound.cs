using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTumbleSound : MonoBehaviour
{
    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        float coll = collision.relativeVelocity.magnitude;
        if (coll > 0.01f) {
            audio.volume = Mathf.Clamp(coll/25f,0f,1f);
            audio.Play();
        }
    }
}
