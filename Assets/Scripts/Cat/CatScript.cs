using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

public class CatAnimation : MonoBehaviour
{
    public AIPath aiPath;
    public Animator animator;
    public GameObject goal;
    public Vector2[] goalSpots;
    public bool[] staysForRandom;
    public float[] howLongToStay;
    public string[] whichAnimationToPlay;
    public AudioClip[] whatSoundToPlay;
    private int _randomGoal;
    private float _timeBetweenMeows = 0;
    
    private bool onTheMove = true;
    private void FixedUpdate()
    {
        if (onTheMove)
        {
            if (aiPath.desiredVelocity.x >= 0.1)
            {
                Debug.Log("Moving Right");
            }
        
            if (aiPath.desiredVelocity.x <= -0.1)
            {
                Debug.Log("Moving Left");
            }
            if (aiPath.desiredVelocity.y >= 0.1)
            {
                Debug.Log("Moving Up");
            }
        
            if (aiPath.desiredVelocity.y <= -0.1)
            {
                Debug.Log("Moving Down");
            }

            if (aiPath.reachedDestination)
            {
                onTheMove = false;
                Debug.Log("Reached Destination");
                OnGoalEnter();
            }
            
            //Counter For Random Accasional Meows
            if (_timeBetweenMeows > 0)
            {
                _timeBetweenMeows -= Time.deltaTime;
            }
            else
            {
                //Play Meow Sound
                Debug.Log("Meow");
                _timeBetweenMeows = Random.Range(5, 60);
            }
            
        }
    }

    private void OnGoalEnter()
    {
        _randomGoal = Random.Range(0, goalSpots.Length);
        //Play Animation
        //Play OneShot Sound
        StartCoroutine(WaitForNewGoal());
    }

    IEnumerator WaitForNewGoal()
    {
        yield return new WaitForSeconds(howLongToStay[_randomGoal]);
        if (staysForRandom[_randomGoal])
        {
            Debug.Log("Random Time Added");
            yield return new WaitForSeconds(Random.Range(5,30));
        }
        goal.transform.position = goalSpots[Random.Range(0,goalSpots.Length)];
        onTheMove = true;
    }
}