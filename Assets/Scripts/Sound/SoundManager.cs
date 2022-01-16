using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    private static float volume;
    public enum Sound
    {
        PlayerJump,
        JumperSound,
        SwordSlice,
        ApplePickUp,
        keyPickUp,
        
        level_1_Music,
        scoreCounterMusic,
        level_2_Music,

    }

    //private static Dictionary<Sound, float> soundTimerDictionary;
    public static void PlaySound(Sound sound, Vector2 position)
    {
        GameObject soundGameObject = new GameObject("Sound");
        soundGameObject.transform.position = position;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = GetAudioClip(sound);
        audioSource.volume = 0.2f;
        audioSource.Play();

        Object.Destroy(soundGameObject, audioSource.clip.length);
    }

    private static bool CanPlaySound(Sound sound)
    {
        switch(sound)
        {
            default:
                return true;
        }
    }
    public static void PlayMainSound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("MainSound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = GetAudioClip(sound);
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.Play();
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach(GameAsset.SoundAudioClip soundAudioClip in GameAsset.i.SoundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                volume = soundAudioClip.volume;
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound" + sound + "not found");
        return null;
    }
}
