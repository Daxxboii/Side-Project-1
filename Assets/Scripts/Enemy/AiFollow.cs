using Scripts.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Enemy
{
    namespace Principal
    {
        public class AiFollow : MonoBehaviour
        {
            [Header("Audio Settings")] 
            [SerializeField] private AudioManager AudioM;
            [SerializeField] private float Time_between_growls;
          
            [Header("Principal States")]
            public bool angry;
            bool NotCooling = true;

            [Header("Principal Values")]
            [SerializeField]private Animator anim;
            [SerializeField]public NavMeshAgent _agent;
            [SerializeField]private float follow_distance;
            [SerializeField]private float attack_radius;
            [SerializeField] private float Roam_Radius;
            [SerializeField] private float cool_period;
            [SerializeField] private float giveup_time;
            private Vector3 newPos;
            private Vector3 RandomSpawnLocation;
            private float Distance_from_player;
            private float timer,timer2,GiveUpTimer;
            private float roam_timer;
            private NavMeshHit navHit;


            [Header("Player")]
            [SerializeField] private GameObject _player;
            [SerializeField] private PlayerScript playerScript;
            [SerializeField] private VisiBility visibility;
            void Start()
            {
                _agent.stoppingDistance = 0.5f;
            }
            void Update()
            {
                Movement();
                Attack();
            }
            void Movement()
            {
                //Principal is not Chilling
                if (NotCooling)
                {
                    //if principal is angry and in sight
                    if (visibility.visible == true && !angry)
                    {
                        _agent.enabled = false;
                        Animations(0, 0);
                        Growl();
                    }

                    //if principal is not in sight
                    else 
                    {
                        _agent.enabled = true;
                        Distance_from_player =DetermineDistanceFromPlayer();

                        //Player is not in Catching Range
                        if (Distance_from_player > follow_distance||GiveUpTimer>giveup_time)
                        {
                            roam_timer += Time.deltaTime;
                            //Change Position Every 10 Seconds
                            if (roam_timer >= 10)
                            {
                                newPos = GetRandomPointNearPlayer(_player.transform.position, Roam_Radius, -1);
                                Animations(0, 1);
                                _agent.SetDestination(newPos);
                                roam_timer = 0;
                                GiveUpTimer = 0f;
                            }
                        }
                        //Player is in catching Range
                        else if(Distance_from_player < follow_distance && GiveUpTimer<giveup_time)
                        {
                            GiveUpTimer += Time.deltaTime;
                            Animations(0, 2);
                            _agent.SetDestination(_player.transform.position);
                            Chase();
                        }
                    }
                }
                //Principal is Chilling
                else
                {
                    Animations(0, 0);
                }
            }
            void Attack()
            {
                //Direct Kill
				if (!angry||playerScript.isDead)
				{
                    if (DetermineDistanceFromPlayer() < attack_radius)
                    {
                        Animations(2, 2);
                        AudioM.Enemy_Princy_Int_Kill();
                    }
                }
                //Player Health is not critical
                else 
                {
                    //Player withing attack Range
                    if (DetermineDistanceFromPlayer() < attack_radius)
                    {
                        //Principal not on cooldown
                        if (NotCooling)
                        {
                            AudioM.Enemy_Princy_Atack();
                            Animations(1, 2);
                            NotCooling = false;
                        }
                        //Keep Principal Freezed
                        else
                        {
                            Animations(0, 0);
                        }
                        StartCoroutine("cooldown");
                    }
                }
            }

            
            float DetermineDistanceFromPlayer()
            {
                return Vector3.Distance(_player.transform.position, gameObject.transform.position);
            }
           



            //Get a point in Navmesh in this distance
            Vector3 GetRandomPointNearPlayer(Vector3 origin, float dist, int layermask)
            {
                RandomSpawnLocation = new Vector3(Random.insideUnitSphere.x * Roam_Radius, transform.position.y, Random.insideUnitSphere.z * Roam_Radius);
                RandomSpawnLocation += origin;
                NavMesh.SamplePosition(RandomSpawnLocation, out navHit, dist, layermask);
                RandomSpawnLocation = navHit.position;
                return RandomSpawnLocation;

            }



            private void Animations(int hit_state, int walk_state)
            {
                anim.SetInteger("Walk_state", walk_state);
                anim.SetInteger("Hit_state", hit_state);
            }

            IEnumerator cooldown()
            {
                yield return new WaitForSeconds(cool_period);
                NotCooling = true;
            }
            void Growl()
            {
                timer += Time.deltaTime;
                if(timer > Time_between_growls)
                {
                    AudioM.Enemy_Princy_Growl();
                    timer = 0;
                }
             //   Debug.Log("Growling;");
            }
            void Chase()
            {
                timer2 += Time.deltaTime;
                if (timer2 > Time_between_growls)
                {
                   // Debug.Log("Chasing");
                    AudioM.Enemy_Princy_chase();
                    timer2 = 0;
                }
            }


        }
    }
}
