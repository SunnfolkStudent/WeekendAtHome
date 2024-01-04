using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Music_Scripts
{
    public class AudioController : MonoBehaviour
    {
        public AudioMixer audioMixer;

        public void SetSfxVolume(Slider volume)
        {
            audioMixer.SetFloat("SFXVolume", volume.value);
        }
        public void SetMusicVolume(Slider volume)
        {
            audioMixer.SetFloat("MusicVolume", volume.value);
        }
        public void SetDialogueVolume(Slider volume)
        {
            audioMixer.SetFloat("DialogueVolume", volume.value);
        }
    }
}
