using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemType : ScriptableObject
{
    public string itemName;
    public string itemText;
    public Sprite itemImage;
    public AudioClip itemAudio;
    public AudioClip cutSceneAudio;
}
