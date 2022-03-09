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
            public AudioManager audioM;
            [SerializeField] PlayerScript ps;
            float chase_timer;
            public float wanderRadius;
            public bool angry;
            public float wanderTimer, fieldOfView = 110f, chase_range,agentstoppingdistance;
            public GameObject volume;
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
           public bool ded = false;

            private Vector3 newPos;

            private void Awake()
            {
                agent = GetComponent<NavMeshAgent>();
                agent.stoppingDistance = agentstoppingdistance;

                //Tracker because gameobject being tracked by navmesh should be grounded
                agent.destination = player.transform.position;
                cooldown = false;
            }

            private void OnEnable()
            {
                agent.destination = player.transform.position;
                cooldown = false;
                Animations(0, 0);
                
            }
            private void Update()
            {
                //if agent is active
                if (agent.enabled)
                {
                    //if girl is in chase mode
                    if (!angry)
                    {
                        //if girl hasnt just attacked
                        if (!cooldown)
                        {
                            //if player is in front of girl
                            if (isinFrontOFMe(player))
                            {
                                chasing = true;
                                agent.SetDestination(player.transform.position);
                                //Trigger Chase Animation
                                Animations(2, 0);
                                Attack();
                               
                            }

                            //if player is not in front of girl
                            else if (!isinFrontOFMe(player))
                            {
                                //ig girl is still chasing
                                if (chasing)
                                {
                                    Animations(2, 0);
                                    audioM.Enemy_Girl_chase();
                                    //Black and white
                                    volume.SetActive(true);
                                    agent.SetDestination(player.transform.position);
                                    chase_timer += Time.deltaTime;
                                    if (chase_timer >= 5f)
                                    {
                                        chasing = false;
                                        chase_timer = 0.0f;
                                    }
                                }
                                else if (!chasing)
                                {
                                  audioM.Girl_Stop();
                                    volume.SetActive(false);
                                    if (Vector3.Distance(transform.position, newPos) < 0.5)
                                    {
                                        Animations(0, 0);
                                    }
                                    else
                                    {
                                        Animations(1, 0);
                                    }
                                    agent.SetDestination(newPos);

                                    timer += Time.deltaTime;
                                    if (timer >= wanderTimer)
                                    {
                                        newPos = RandomNavSphere(player.transform.position, wanderRadius, -1);
                                        timer = 0;
                                    }

                                }
                            }
                        }
                    }
                    if (angry)
                    {
                        agent.SetDestination(player.transform.position);
                        agent.stoppingDistance = 15;
                        if (agent.isStopped || Vector3.Distance(player.transform.position,transform.position)<16f)
                        {
                            Animations(0, 0);
                        }
                        else
                        {
                            Animations(1, 0);
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
            /* walk state 0 is idle 
             * walk state 2 is chasing
             * walk state 1 is walking 
             */
            public void Animations(int walk_state, int hit_state)
            {
                Girl_animator.SetInteger("Hit_State", hit_state);
                Girl_animator.SetInteger("Walk_State", walk_state);
            }

            private void Attack()
            {
                //if player is too close
                if (Vector3.Distance(player.transform.position, transform.position) < attack_distance)
                {
                    if (ps.isDead == false && ps.Health > 25)
                    {
                        if (hit)
                        {
                            Animations(2, 1);
                            cooldown = true;
                            agent.isStopped = true;
                            ps.PlayerTakeDamage(damage);
                            hit = false;
                           
                        
                            Invoke("ReActivate", Cooldown_period);
                        }

                    }

                    //If player has low health
                    else if (ps.isDead||ps.Health<=25)
                    {
                        ps.PlayerTakeDamage(damage);
                      
                        if (!ded)
                        {
                            audioM.Enemy_Girl_kill();
                            ded = true;
                        }
                      //Trigger kill animation
                        Animations(2, 2);
                     
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