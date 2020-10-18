using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;
   

    public enum SoundName
    {
        Ambient,
        Shock,
        Chase
    }

    [SerializeField] private Sound[] sounds;
    [SerializeField] private AudioSource audioSrc;
    [SerializeField] private AudioSource secondarySrc;
    [SerializeField] private Queue<Sound> soundQueue;
    private Sound currentSound;
    private bool queueBegan = false;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        soundQueue = new Queue<Sound>();
        PlaySound(SoundName.Ambient);
    }

    public void PlaySound(SoundName name)
    {
       for(int i = 0; i < sounds.Length; ++i)
        {
            if(sounds[i].GetSoundName() == name)
            {
                if (audioSrc)
                {
                    audioSrc.clip = sounds[i].GetAudioClip();
                    audioSrc.loop = sounds[i].IsLooping();
                    currentSound = sounds[i];
                    audioSrc.Play();
                }
            }
        }
    }

    public bool IsPlayingSound(SoundName name)
    {
        if (audioSrc.isPlaying)
        {
            if(currentSound.GetSoundName() == name)
            {
                return true;
            }
        }

        return false;
    }

    public void BeginQueue()
    {
        queueBegan = true;
        StartCoroutine(PlayQueue());
    }

    IEnumerator PlayQueue()
    {
        while(soundQueue.Count != 0)
        {
            Sound soundToPlay = soundQueue.Dequeue();
            audioSrc.clip = soundToPlay.GetAudioClip();
            audioSrc.loop = soundToPlay.IsLooping();
            currentSound = soundToPlay;
            audioSrc.Play();


            if(soundToPlay.GetAudioClip().length > 15)
            {
                yield return new WaitForSeconds(15);
            }
            else
            {
                yield return new WaitForSeconds(soundToPlay.GetAudioClip().length);
            }
            
        }

        queueBegan = false;
        yield return null;
    }


    public bool HasQueueBegan()
    {
        return queueBegan;
    }

    public void AddToSoundQueue(SoundName name)
    {
        for(int i = 0; i < sounds.Length; ++i)
        {
            if(sounds[i].GetSoundName() == name)
            {
                soundQueue.Enqueue(sounds[i]);
            }
        }
        
    }


    public void PlaySecondary(SoundName name)
    {
        for (int i = 0; i < sounds.Length; ++i)
        {
            if (sounds[i].GetSoundName() == name)
            {
                if (secondarySrc)
                {
                    secondarySrc.clip = sounds[i].GetAudioClip();
                    secondarySrc.loop = sounds[i].IsLooping();
                    currentSound = sounds[i];
                    secondarySrc.Play();
                }
            }
        }
    }
    void ReturnToAmbient()
    {

    }

    void QueietScene()
    {

    }
}
