using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound : MonoBehaviour
{
    [SerializeField] private AudioManager.SoundName soundType;
    [SerializeField] private AudioClip soundClip;
    [SerializeField] private bool loops;

    public AudioManager.SoundName GetSoundName()
    {
        return soundType;
    }

    public AudioClip GetAudioClip()
    {
        return soundClip;
    }

    public bool IsLooping()
    {
        return loops;   
    }
}
