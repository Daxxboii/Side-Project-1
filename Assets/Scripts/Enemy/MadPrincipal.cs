using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Scripts.Player;

namespace Scripts.Enemy
{
    namespace PrincipalMad
    {
        public class MadPrincipal : MonoBehaviour
        {

            [SerializeField] PlayerScript ps;
            float a;
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
            private bool cooldown, chasing;
            [SerializeField]
            private int damage;
            [SerializeField]
            private float attack_distance;
            public float Cooldown_period;
            private Vector3 newPos;

            private void Start()
            {
                agent = GetComponent<NavMeshAgent>();
                timer = wanderTimer;
                //Tracker because gameobject being tracked by navmesh should be grounded
                player = GameObject.FindWithTag("Tracker");
                cooldown = false;
            }
            private void Update()
            {
                //  Debug.Log(Vector3.Distance(transform.position, player.transform.position));
                if (!cooldown)
                {

                    if (isinFrontOFMe(player))
                    {
                        chasing = true;
                        Debug.Log("chaising");
                        agent.SetDestination(player.transform.position);
                       
                        Attack();
                    }
                    else if (!isinFrontOFMe(player))
                    {
                        if (chasing)
                        {
                            agent.SetDestination(player.transform.position);
                            a += Time.deltaTime;
                            if (a >= 5f)
                            {
                                chasing = false;
                                a = 0.0f;
                            }
                        }
                        else if (!chasing)
                        {
                            if (Vector3.Distance(transform.position, newPos) < 0.5)
                            {
                                
                            }
                            else
                            {
                                
                            }

                            {
                                timer += Time.deltaTime;
                                if (timer >= wanderTimer)
                                {
                                    newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                                    agent.SetDestination(newPos);
                                    timer = 0;
                                }
                            }
                        }
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
                        if (hit.transform.gameObject.tag == "Player")
                        {
                            return true;
                        }
                        else
                            return false;

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
            /* walk state -1 is idle 
             * walk state 2 is chasing
             * walk state 1 is walking 
             */

            private void Attack()
            {
                if (ps.isDead == false)
                {
                    if (Vector3.Distance(player.transform.position, transform.position) < attack_distance)
                    {
                        cooldown = true;
                        agent.isStopped = true;
                        ps.PlayerTakeDamage(damage);
                        
                        Invoke("ReActivate", Cooldown_period);
                    }
                }
                else if (ps.isDead)
                {
                    
                    //  Debug.Log("kill;");
                }
            }

            void ReActivate()
            {
                cooldown = false;
                agent.isStopped = false;

            }

        }
    }
}

