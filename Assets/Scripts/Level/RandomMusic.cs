using UnityEngine;
using System.Collections;

public class RandomMusic : MonoBehaviour
{
    public AudioSource music;
    public AudioClip[] soundtrack;

    // Use this for initialization
    /*void Awake()
    {
        if (!music.playOnAwake)
        {
            music.clip = soundtrack[Random.Range(0, soundtrack.Length)];
            music.Play();
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        if (!music.isPlaying)
        {
            music.clip = soundtrack[Random.Range(0, soundtrack.Length)];
            music.Play();
        }
    }
}
