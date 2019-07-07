using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{

    private AudioSource audio;
    private int clipCount;
    public List<AudioClip> clips = new List<AudioClip>(3);

    // Use this for initialization
    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        //audio.Play();
        clipCount = 0;
        Debug.Log(clips.Capacity);
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(Input.touchCount);
        if (Input.touchCount > 0)
        {
            //Debug.Log(Input.GetTouch(0).position);
            //foreach (Touch touches in Input.touches)
            if(Input.touchCount > 0 && Input.touchCount < 2)
            {
                audio.Play();
                //Debug.Log(touches);
                if (clipCount < clips.Capacity)
                {
                    audio.clip = clips[clipCount];
                    audio.Play();
                    clipCount++;
                    Debug.Log("Cuenta de Lista: " + clipCount);
                }
                else
                {
                    clipCount = 0;
                }
            }
        }

    }
}
