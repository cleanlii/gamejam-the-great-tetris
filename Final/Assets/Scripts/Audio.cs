using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource music;
    public AudioClip fuck;

    void Start()
    {
        music = gameObject.AddComponent<AudioSource>();
        music.playOnAwake = false;
        //fuck = Resources.Load<AudioClip>("Audio/wasd");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            music.clip = fuck;
            music.Play();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            music.clip = fuck;
            music.Play();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            music.clip = fuck;
            music.Play();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            music.clip = fuck;
            music.Play();
        }
    }
}
