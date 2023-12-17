using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFoodFull : MonoBehaviour
{
    public static bool catBowlFull;
    private SpriteRenderer _sprite;

    private void Start()
    {
        _sprite = gameObject.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (catBowlFull)
        {
            _sprite.enabled = true;
        }
        else
        {
            _sprite.enabled = false;
        }
    }
}
