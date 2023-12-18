using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public float timer = 5f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("DeathScreen",timer);
        }
    }

    void DeathScreen()
    {
        SceneManager.LoadScene("DeathCredits");
    }
}
