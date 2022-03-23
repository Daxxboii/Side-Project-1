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
            [Header("Scripts")]
            [SerializeField] private AudioManager audioM;
            [SerializeField] private PlayerScript ps;

            [Header("Variables")]
            [SerializeField]private float wanderRadius;
            [SerializeField]private float wanderTimer, fieldOfView = 110f, chase_range,agentstoppingdistance;
            [SerializeField]private int damage;
            [SerializeField]private float attack_distance;
            [SerializeField]public float Cooldown_period;
            private float chase_timer;
            private float LaughTimer;

            [Header("Audio")]
            [SerializeField,Range(0,10)] private float Time_Between_ChaseSound;

            [Header("Components")]
            [SerializeField]public GameObject player; 
            [SerializeField] public NavMeshAgent agent;
            [SerializeField]private Animator Girl_animator;

            [Header("Booleans")]
            [SerializeField] private bool cooldown;
          //  [SerializeField] private bool chasing;
            [SerializeField] private bool hit = true;
            [SerializeField] private bool ded = false;
            [SerializeField] public bool angry;
           
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
                newPos = RandomNavSphere(player.transform.position, wanderRadius, -1);
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
                            if (isinFrontOFMe())
                            {
                                
                                 
                                    agent.SetDestination(player.transform.position);
                                    //Trigger Chase Animation
                                    Animations(2, 0);
                                    LaughTimer +=Time.deltaTime;
                                    if (LaughTimer > Time_Between_ChaseSound)
                                    {
                                        audioM.Enemy_Girl_chase();
                                        LaughTimer = 0f;
                                    }
                                    Attack();
                                
								
								
                            }

                            //if player is not in front of girl
                            else 
                            {
                                ChangePos();
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
            bool isinFrontOFMe()
            {
				if (Vector3.Distance(player.transform.position, transform.position) > chase_range)
				{
                    return true;
				}
				else
				{
                    return false;
				}
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

            void ChangePos()
			{
                audioM.Girl_Stop();
                if (Vector3.Distance(transform.position, newPos) < 2)
                {
                    Animations(0, 0);
                    newPos = RandomNavSphere(player.transform.position, wanderRadius, -1);
                }

                else
                {
                    Animations(1, 0);
                }
                agent.SetDestination(newPos);
            }
        }
    }
}