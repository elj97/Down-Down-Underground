using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip digSound, fruitPickup, soundTrack;
    private static AudioSource audioSource;
    AudioSource loopingSoundTrack;

    void Start()
    {
        digSound = Resources.Load<AudioClip>("digSound");
        fruitPickup = Resources.Load<AudioClip>("fruitPickup");
        soundTrack = Resources.Load<AudioClip>("soundTrack");

        audioSource = GetComponent<AudioSource>();

        loopingSoundTrack = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "digSound":
                audioSource.PlayOneShot(digSound);
                break;
            case "fruitPickup":
                audioSource.PlayOneShot(fruitPickup);
                break;
            case "soundTrack":
                audioSource.PlayOneShot(soundTrack);
                break;
        }
    }

}
