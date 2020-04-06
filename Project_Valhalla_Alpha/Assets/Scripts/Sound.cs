// script borrowed from Brackeys' "Introduction to AUDIO in Unity"
// https://www.youtube.com/watch?v=6OT43pvUyfY

using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
