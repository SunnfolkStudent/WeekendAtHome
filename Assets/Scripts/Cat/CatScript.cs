using System;
using System.Collections;
using Pathfinding;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

namespace Cat
{
    public class CatScript : MonoBehaviour
    {
        public AIPath aiPath;
        public Animator anim;
        public GameObject goal;
        public AudioSource meowAudioSource;
        public AudioClip[] meows;
        public Vector2[] goalSpots; 
        
        public bool[] staysForRandom;
        public float[] howLongToStay;
        
        private string _direction;
        private Vector2 _directionValue;
        private Vector2 _lastPosition; 
        
        [SerializeField] private GameObject cat;
        [SerializeField] private SortingGroup catSortingGroup;
        
        private int _randomGoal;
        private float _timeBetweenMeows;
        private bool _destinationReached;
        private bool _goingTowardsCatFood;
        private bool _onTheMove = true;

        private void Start()
        {
            cat = GameObject.FindWithTag("Cat");
            catSortingGroup = cat.GetComponent<SortingGroup>();
            
            if (DataTransfer.CatOutside)
            {
                catSortingGroup.sortingOrder = DataTransfer.CatSortingOrderOutside;
                Debug.Log("Cat Is Outside");
            }
            else
            {
                catSortingGroup.sortingOrder = DataTransfer.CatSortingOrderInside;
                Debug.Log("Cat Is Inside");
            }
            
        }

        private void Update()
        {
            // TODO: Fix bug that happens when player is outside and walks inside, causing the cat to appear above the inside walls if outside as well.
            
            if (DataTransfer.CatOutside)
            {
                catSortingGroup.sortingOrder = DataTransfer.CatSortingOrderOutside;
            }
            else if (DataTransfer.CatOutside && DataTransfer.PlayerInside)
            {
                catSortingGroup.sortingOrder = DataTransfer.CatSortingOrderOutside;
            }
            else if (!DataTransfer.CatOutside)
            {
                catSortingGroup.sortingOrder = DataTransfer.CatSortingOrderInside;
            }
            
            // Consider using Math.Sign to return a value of either -1, 0 or 1, and use that for animations.
            // Calculates direction from last-pos to current-pos
            var directionValue = ((Vector2)transform.position - _lastPosition).normalized;
       
            // Updates last-pos for use in next Update() cycle
            _lastPosition = transform.position;
            
            if (directionValue.y > Mathf.Sqrt(0.5f))
            {
                _direction = "_Up";
            }
            else if (directionValue.y < -Mathf.Sqrt(0.5f))
            {
                _direction = "_Down";
            } 
            else if (directionValue.x > Mathf.Sqrt(0.5f))
            {
                _direction = "_Right";
            }
            else if (directionValue.x < -Mathf.Sqrt(0.5f))
            {
                _direction = "_Left";
            }
            Animate(directionValue == Vector2.zero ? "Toes_Idle" : "Toes_Walk");
        }
        private void FixedUpdate()
        {
            //Finds Which Direction The Cat is Generally Moving
            if (_onTheMove)
            {
                //If At Destination. Play Goal Function
                if (aiPath.reachedDestination && !_destinationReached)
                {
                    _destinationReached = true;
                    /* Debug.Log(_randomGoal);*/
                    _onTheMove = false;
                    //Debug.Log("Reached Destination");
                    OnGoalEnter();
                }
            }
            //Counter For Random Occasional Meows
            if (_timeBetweenMeows > 0)
            {
                _timeBetweenMeows -= Time.deltaTime;
            }
            else
            {
                meowAudioSource.clip = meows[Random.Range(0, meows.Length)];
                meowAudioSource.PlayOneShot(meowAudioSource.clip);
                Debug.Log("Meow");
                _timeBetweenMeows = Random.Range(10, 30);
            }
            if (!CatFoodFull.CatBowlFull) return;
            
            _goingTowardsCatFood = true;
            goal.transform.position = new Vector3(1,0,0);
        }
        
        private void Animate(string unitAnimation)
        {
            anim.Play(unitAnimation + _direction);
        }
        
        //Plays When At Goal, Sets A New Goal, Then Starts Coroutine
        private void OnGoalEnter()
        {
            StartCoroutine(WaitForNewGoal());
        }

        //Waits For Seconds, Then Starts New Goal
        private IEnumerator WaitForNewGoal()
        {
            yield return new WaitForSeconds(howLongToStay[_randomGoal]);
            if (staysForRandom[_randomGoal])
            {
                Debug.Log("Random Time Added");
                yield return new WaitForSeconds(Random.Range(0, 10));
            }

            if (_goingTowardsCatFood)
            {
                yield return new WaitForSeconds(13);
                CatFoodFull.CatBowlFull = false;
                _goingTowardsCatFood = false;
            }
            _randomGoal = Random.Range(0, goalSpots.Length);
            Debug.Log(goalSpots[_randomGoal]);
            goal.transform.position = goalSpots[_randomGoal];
            /*Debug.Log(goal.transform.position);*/
            _onTheMove = true;
            _destinationReached = false;
        }
    }
}