using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadNextMorningDay3 : MonoBehaviour
{
    public LevelLoader levelLoader;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            levelLoader.loadSceneByName("Day 3 - Morning 2");
    }
}
