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

            [Header("Variable depending on player")]
            [SerializeField]
             public bool _isVisible;
            [SerializeField]
            float daimage,follow_distance;
            [SerializeField]
            private bool _isPlayerHiding;
            [SerializeField]
            private bool _isPlayerIdle;
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


            private bool isAgentOnNavMesh;
            private Vector3 newPos;
            VisiBility vis;


            private Vector3 _randomSpawanLocation;

            [Header("Timer")]
            [SerializeField]
            private float _idleWaitTime;
            [SerializeField]
            private float _timer;

            void Start()
            {
                _agent = GetComponent<NavMeshAgent>();
                vis = _player.transform.GetComponent<VisiBility>();
            }
            void Update()
            {
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
                        ps.PlayerTakeDamage(daimage);
                        transform.position = newPos;
                    }
                }
            }
            float inAttackRange()
            {
                return Vector3.Distance(_player.transform.position, gameObject.transform.position);
            }
            void Movement()
            {
                ///<summary>
                ///if player can see 
                /// Stop!!
                /// 
                /// if player can't see and isIdle
                ///     start timer
                ///     if timer crossed
                ///         check distance b/w them
                ///         if distance is greater than desired disatnce
                ///             choose random point
                ///             check if random point is on nav mesh
                ///             if ramdom point is on nav mesh
                ///                 teleport to random point
                ///             else
                ///                 again check random point
                ///     set Nav Mesh destination to player
                ///</summary>

                if (_isVisible == true)
                {
                    _agent.enabled = false;
                    _timer = 0f;
                }



                if (_isVisible == false)
                {
                    _agent.enabled = true;

                   
                        float disatnce = Vector3.Distance(transform.position, _player.transform.position);

                        if (disatnce > follow_distance)
                        {
                            newPos = GetRandomPointNearPlayer();

                            isAgentOnNavMesh = IsAgentOnNavMesh(newPos);

                            if (isAgentOnNavMesh == true)
                            {
                            _agent.SetDestination(newPos);
                        }
                            else
                            {
                                newPos = GetRandomPointNearPlayer();
                            }


                        }
                    else
                    {

                        _agent.SetDestination(_player.transform.position);

                    }

                }

            }
        
            
            private void OnEnable()
            {
                PlayerScript.OnPlayerIdle += PlayerIdle;
            }

            void PlayerIdle(bool idle)
            {
                _isPlayerIdle = idle;

            }

            Vector3 GetRandomPointNearPlayer()
            {
                _randomSpawanLocation = new Vector3(Random.insideUnitSphere.x * _radius, transform.position.y, Random.insideUnitSphere.z * _radius);

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


            

            private void OnDisable()
            {
                PlayerScript.OnPlayerIdle -= PlayerIdle;
            }
        }
    }
}
