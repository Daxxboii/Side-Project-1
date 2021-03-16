using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace LoneWolfStudios.Control
{
    public class GirlAiGhost : MonoBehaviour
    {

        public float wanderRadius;
        public float wanderTimer, fieldOfView = 110f, range;
        Vector3 playerLastInSight;
        [SerializeField]
        private Transform target;
        [SerializeField]
        private NavMeshAgent agent;
       // [SerializeField]
        private float timer;
        private GameObject player;


        [SerializeField]
        public Animator Girl_animator;




        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            timer = wanderTimer;
           //Tracker because gameobject being tracked by navmesh should be grounded

            player = GameObject.FindWithTag("Tracker");
        }
        private void Update()
        {
            
             
            if (isinFrontOFMe(player))
            {
               // Debug.Log("chaising");
                agent.SetDestination(player.transform.position);
                Animations(2);
            }
            else if (!isinFrontOFMe(player))
            {
                //   Debug.Log("patoling");
                Animations(1);
                timer += Time.deltaTime;

                if (timer >= wanderTimer)
                {

                    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                    agent.SetDestination(newPos);
                    timer = 0;
                }
            }
        }
        bool isinFrontOFMe(GameObject player)
        {

            Vector3 direction = player.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            if (angle < fieldOfView * 0.5f)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, range))
                {
                    Debug.DrawRay(transform.position, direction, Color.black);
                    return true;

                }
                else
                    return false;
            }
            else
                return false;
        }

        public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
        {
            Vector3 randDirection = Random.insideUnitSphere * dist;

            randDirection += origin;

            NavMeshHit navHit;

            NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

            return navHit.position;
        }

        private void Animations(int walk_state)
        {
            Girl_animator.SetInteger("Walk_State", walk_state);
        }
    }
}