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
            public bool angry;
            [Header("Variable depending on player")]
            [SerializeField]
             public bool _isVisible;
            [SerializeField]
            public float daimage,follow_distance;
            [SerializeField]
            private GameObject _player;
            [SerializeField]
            PlayerScript ps;
            [SerializeField]
            Animator anim;
            [Header("Variable depending on NavMesh and agent")]
            [SerializeField, Tooltip("set threshold of how big area to sample to check if player is on nav mesh or not.")]
            private float _agentThreshHold;
            [SerializeField, Tooltip("Radius of Random.insideUnitSpher")]
            private float _radius;
            private NavMeshAgent _agent;
            public GameObject volume;

            private bool isAgentOnNavMesh;
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
                    _agent.speed = 3.5f;
                }
                if (disatnce>follow_distance)
                {
                    volume.SetActive(false);
                }
                else if(disatnce < follow_distance)
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
                    if (inAttackRange() < 2f)
                    {
                  
                        if (hit)
                        {
                        
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
                    }
                    
                }
            }
            float inAttackRange()
            {
                return Vector3.Distance(_player.transform.position, gameObject.transform.position);
            }
            void Movement()
            {
                if (_isVisible == true && !angry)
                {
                  _agent.enabled = false;
                  Animations(0, 0);
                }



                if (_isVisible == false || angry)
                {
                    _agent.enabled = true;
                   
                      disatnce = Vector3.Distance(transform.position, _player.transform.position);

                        if (disatnce > follow_distance)
                        {
                        Debug.Log("not chasing");
                            newPos = GetRandomPointNearPlayer(_player.transform.position, _radius, -1);
                            isAgentOnNavMesh = IsAgentOnNavMesh(newPos);
                            if (isAgentOnNavMesh == true)
                            {
                            _agent.SetDestination(newPos);
                            Animations(0, 1);
                            }
                            else
                            {
                                newPos = GetRandomPointNearPlayer(_player.transform.position, _radius, -1);
                            }
                        }
                    else
                    {

                        _agent.SetDestination(_player.transform.position);
                        Animations(0, 2);

                    }

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

            private bool IsAgentOnNavMesh(Vector3 agentPos)
            {

                NavMeshHit hit;

                //check for nearest point in navmesh to agent, within thrwshold
                if (NavMesh.SamplePosition(agentPos, out hit, _agentThreshHold, NavMesh.AllAreas))
                {
                    return true;
                }

                return false;
            }

            private void Animations(int hit_state , int walk_state)
            {
                anim.SetInteger("Walk_state", walk_state);
                anim.SetInteger("Hit_state", hit_state);
            }
            IEnumerator cooldown()
            {
                yield return new WaitForSeconds(cool_period);
                hit = true;
            }

         
        }
    }
}
