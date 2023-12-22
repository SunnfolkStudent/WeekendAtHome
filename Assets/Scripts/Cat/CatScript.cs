using System.Collections;
using Pathfinding;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Cat
{
    public class CatAnimation : MonoBehaviour
    {
        public AIPath aiPath;
        [FormerlySerializedAs("animator")] public Animator anim;
        public GameObject goal;
        public Vector2[] goalSpots;
        public bool[] staysForRandom;
        public float[] howLongToStay;
        public string[] whichAnimationToPlay;
        public AudioClip[] whatSoundToPlay;
        private int _randomGoal;
        private float _timeBetweenMeows = 0;
        private bool _destinationReached;
        private bool _goingTowardsCatFood;
    

        public AudioSource meows;
    
        private bool onTheMove = true;
        private void FixedUpdate()
        {
            //Finds Which Direction The Cat is Generally Moving
            if (onTheMove)
            {
                if (aiPath.desiredVelocity == Vector3.zero)
                {
                    //anim.Play("Player_Idle_Normal");
                }
                else
                {
                    anim.Play("Movement");
                    anim.SetFloat("Horizontal", aiPath.desiredVelocity.x);
                    anim.SetFloat("Vertical", aiPath.desiredVelocity.y);
                }
            
                if (aiPath.desiredVelocity.x != 0 && aiPath.desiredVelocity.y == 0)
                {
                    anim.transform.localScale = new Vector3(-aiPath.desiredVelocity.x, 1f, 1f);
                }
                else
                {
                    anim.transform.localScale = new Vector3(1f, 1f, 1f);
                }

                //If At Destination. Play Goal Function
                if (aiPath.reachedDestination && !_destinationReached)
                {
                    _destinationReached = true;
                    /* Debug.Log(_randomGoal);*/
                    anim.Play("Player_Idle_Normal");
                    onTheMove = false;
                    //Debug.Log("Reached Destination");
                    OnGoalEnter();
                }
            
                //Counter For Random Occasional Meows
                if (_timeBetweenMeows > 0)
                {
                    _timeBetweenMeows -= Time.deltaTime;
                }
                else
                {
                    //meows.Play();
                    Debug.Log("Meow");
                    _timeBetweenMeows = Random.Range(5, 20);
                }
            
            }

            if (CatFoodFull.catBowlFull)
            {
                _goingTowardsCatFood = true;
                goal.transform.position = new Vector3(1,0,0);
            }
        }

        //Plays When At Goal, Sets A New Goal, Then Starts Coroutine
        private void OnGoalEnter()
        {
            StartCoroutine(WaitForNewGoal());
        }

        //Waits For Seconds, Then Starts New Goal
        IEnumerator WaitForNewGoal()
        {
            yield return new WaitForSeconds(howLongToStay[_randomGoal]);
            if (staysForRandom[_randomGoal])
            {
                Debug.Log("Random Time Added");
                yield return new WaitForSeconds(Random.Range(1,15));
            }

            if (_goingTowardsCatFood)
            {
                yield return new WaitForSeconds(13);
                CatFoodFull.catBowlFull = false;
                _goingTowardsCatFood = false;
            }
            _randomGoal = Random.Range(0, goalSpots.Length);
            /*Debug.Log(goalSpots[_randomGoal]);*/
            goal.transform.position = goalSpots[_randomGoal];
            /*Debug.Log(goal.transform.position);*/
            onTheMove = true;
            _destinationReached = false;
        }
    }
}