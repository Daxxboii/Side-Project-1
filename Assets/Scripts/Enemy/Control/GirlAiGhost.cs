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
            private Animator Girl_animator;
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
            private void Animations(int walk_state, int hit_state)
            {
                Girl_animator.SetInteger("Hit_State", hit_state);
                Girl_animator.SetInteger("Walk_State", walk_state);
            }

            private void Attack()
            {
                if (ps.isDead == false)
                {
                    if (Vector3.Distance(player.transform.position, transform.position) < attack_distance)
                    {
                        cooldown = true;
                        agent.isStopped = true;
                        ps.PlayerTakeDamage(damage);
                        Animations(-1, 1);
                        Invoke("ReActivate", Cooldown_period);
                    }
                }
                else if (ps.isDead)
                {
                    Animations(2, 2);
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