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

    public AudioSource meows;
    
    private bool onTheMove = true;
    private void FixedUpdate()
    {
        //Finds Which Direction The Cat is Generally Moving
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

            //If At Destination. Play Goal Function
            if (aiPath.reachedDestination)
            {
                onTheMove = false;
                Debug.Log("Reached Destination");
                OnGoalEnter();
            }
            
            //Counter For Random Occasional Meows
            if (_timeBetweenMeows > 0)
            {
                _timeBetweenMeows -= Time.deltaTime;
            }
            else
            {
                meows.Play();
                Debug.Log("Meow");
                _timeBetweenMeows = Random.Range(5, 30);
            }
            
        }
    }

    //Plays When At Goal, Sets A New Goal, Then Starts Coroutine
    private void OnGoalEnter()
    {
        _randomGoal = Random.Range(0, goalSpots.Length);
        //Play Animation
        //Play OneShot Sound
        StartCoroutine(WaitForNewGoal());
    }

    //Waits For Seconds, Then Starts New Goal
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