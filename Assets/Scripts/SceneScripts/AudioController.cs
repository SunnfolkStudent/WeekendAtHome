using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider sfx;

    public Slider music;

    public Slider dialogue;

    public void SetSFXVolume()
    {
        audioMixer.SetFloat("SFXVolume", sfx.value);
    }
    public void SetMusicVolume()
    {
        audioMixer.SetFloat("MusicVolume", music.value);
    }
    public void SetDialogueVolume()
    {
        audioMixer.SetFloat("DialogueVolume", dialogue.value);
    }
}
