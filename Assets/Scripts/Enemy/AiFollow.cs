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
            [SerializeField]
            private float Time_between_growls,timer;
            public AudioManager AudioM;
            public bool angry;
            [Header("Variable depending on player")]
            [SerializeField]
            public bool _isVisible;
            [SerializeField]
            public float daimage, follow_distance;
            [SerializeField]
            private GameObject _player;
            [SerializeField]
            PlayerScript ps;
            [SerializeField]
            Animator anim;
            [Header("Variable depending on NavMesh and agent")]
            [SerializeField, Tooltip("set threshold of how big area to sample to check if player is on nav mesh or not.")]
            private float _agentThreshHold, roam_timer;
            [SerializeField, Tooltip("Radius of Random.insideUnitSpher")]
            private float _radius;
            private NavMeshAgent _agent;
            public GameObject volume;
            public float attack_radius;

            private Vector3 newPos;
            VisiBility vis;
            bool hit = true;
            public float cool_period;

            private Vector3 _randomSpawanLocation;

            float disatnce;




            void Start()
            {
                _agent = GetComponent<NavMeshAgent>();
                _agent.stoppingDistance = 1.5f;
                vis = _player.transform.GetComponent<VisiBility>();
            }
            void Update()
            {
                if (angry)
                {
                    daimage = 25;
                    _agent.speed = 3.3f;
                }
                if (disatnce > follow_distance)
                {
                    volume.SetActive(false);
                }
                else if (disatnce < follow_distance)
                {
                    volume.SetActive(true);
                }
                _isVisible = vis.visible;
                Movement();
                Attak();
            }
            void Attak()
            {
                if (ps.isDead == false)
                {
                    if (inAttackRange() < attack_radius)
                    {
                        if (hit)
                        {
                            AudioM.Enemy_Princy_Atack();
                            if (ps.Health - daimage > 0)
                            {
                                Animations(1, 2);
                            }
                            ps.PlayerTakeDamage(daimage);
                            hit = false;
                        }
                        else
                        {
                            Animations(0, 0);
                        }
                        StartCoroutine("cooldown");
                    }
                }

                else if (ps.isDead)
                {
                    if (inAttackRange() <= 2f)
                    {
                        Animations(2, 2);
                        AudioM.Enemy_Princy_Int_Kill();
                    }

                }
            }
            float inAttackRange()
            {
                return Vector3.Distance(_player.transform.position, gameObject.transform.position);
            }
            void Movement()
            {
                if (hit)
                {
                    if (_isVisible == true && !angry)
                    {
                        _agent.enabled = false;
                        Animations(0, 0);
                        Growl();
                    }
                    else if (_isVisible == false)
                    {

                        _agent.enabled = true;

                        disatnce = Vector3.Distance(transform.position, _player.transform.position);

                        if (disatnce > follow_distance)
                        {
                            roam_timer += Time.deltaTime;
                            if (roam_timer >= 10)
                            {
                                newPos = GetRandomPointNearPlayer(_player.transform.position, _radius, -1);
                                Animations(0, 1);
                                _agent.SetDestination(newPos);
                                roam_timer = 0;
                            }
                        }
                        else
                        {
                            Animations(0, 2);
                            _agent.SetDestination(_player.transform.position);
                            Chase();
                        }
                    }
                }
                else
                {
                    Animations(0, 0);
                }

               
            }




            Vector3 GetRandomPointNearPlayer(Vector3 origin, float dist, int layermask)
            {
                _randomSpawanLocation = new Vector3(Random.insideUnitSphere.x * _radius, transform.position.y, Random.insideUnitSphere.z * _radius);
                _randomSpawanLocation += origin;

                NavMeshHit navHit;

                NavMesh.SamplePosition(_randomSpawanLocation, out navHit, dist, layermask);

                return _randomSpawanLocation;

            }



            private void Animations(int hit_state, int walk_state)
            {
                anim.SetInteger("Walk_state", walk_state);
                anim.SetInteger("Hit_state", hit_state);
            }

            IEnumerator cooldown()
            {
                yield return new WaitForSeconds(cool_period);
                hit = true;
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
                timer += Time.deltaTime;
                if (timer > Time_between_growls)
                {
                    Debug.Log("Chasing");
                    AudioM.Enemy_Princy_chase();
                    timer = 0;
                }
            }


        }
    }
}
