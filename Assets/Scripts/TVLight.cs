using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class TVLight : MonoBehaviour
{

    private Light2D light2D;
    // Start is called before the first frame update
    void Start()
    {
        light2D = GetComponent<Light2D>();
    }

    private void Update()
    {
        StartCoroutine(ChangeLight());
    }

    IEnumerator ChangeLight()
    {
        
        light2D.color = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255), 0.1f);
        yield return new WaitForSeconds(0.2f);
    }
}
