using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Scripts.Player;

namespace Scripts.Enemy

{
    namespace girlHostile
    {
        public class GirlAiGhost : MonoBehaviour
        {
            [SerializeField] PlayerScript ps;
            float a;
            public float wanderRadius;
		
            public float wanderTimer, fieldOfView = 110f, chase_range,stoppingdistance;
            Vector3 playerLastInSight;
         
            [SerializeField]
            public NavMeshAgent agent;
            // [SerializeField]
            private float timer;
            public GameObject player;
            [SerializeField]
            private bool cooldown, chasing;

            [SerializeField]
            private Animator Girl_animator;
            [SerializeField]
            private int damage;
            [SerializeField]
            private float attack_distance;
            private bool hit = true;
            public float Cooldown_period;

            private Vector3 newPos;

            private void Awake()
            {
                agent = GetComponent<NavMeshAgent>();
                agent.stoppingDistance = stoppingdistance;
                //Tracker because gameobject being tracked by navmesh should be grounded
              
                agent.destination = player.transform.position;
                cooldown = false;
            }
            private void FixedUpdate()
            {
                //   Debug.Log(Vector3.Distance(transform.position, player.transform.position));
            //    Debug.Log(agent.destination);
                if (!cooldown)
                {

                    if (isinFrontOFMe(player))
                    {
                        chasing = true;
                      //  Debug.Log("chaising");
                        agent.SetDestination(player.transform.position);
                        Animations(2, 0);
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
                                Animations(-1, 0);
                            }
                            else
                            {
                                Animations(1, 0);
                            }

                            {
                                timer += Time.deltaTime;
                                if (timer >= wanderTimer)
                                {
                                        newPos = RandomNavSphere(player.transform.position, wanderRadius, -1);
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
                    if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, chase_range))
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

            public  Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
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
            private void Animations(int walk_state, int hit_state)
            {
                Girl_animator.SetInteger("Hit_State", hit_state);
                Girl_animator.SetInteger("Walk_State", walk_state);
            }

            private void Attack()
            {

                if (Vector3.Distance(player.transform.position, transform.position) < attack_distance)
                {
                    if (ps.isDead == false && ps.Health > 25)
                    {
                        if (hit)
                        {
                            Animations(-1, 1);
                            cooldown = true;
                            agent.isStopped = true;
                            ps.PlayerTakeDamage(damage);
                            hit = false;
                          //  newPos = RandomNavSphere(player.transform.position, wanderRadius, -1);

                            Invoke("ReActivate", Cooldown_period);
                        }

                    }
                    else if (ps.isDead||ps.Health<=25)
                    {
                        ps.PlayerTakeDamage(damage);
                        Animations(2, 2);
                        //  Debug.Log("kill;");
                    }
                }
               
            
            }

            void ReActivate()
            {
              //  agent.Warp(newPos);
                cooldown = false;
                agent.isStopped = false;
                hit = true;
            }
        }
    }
}